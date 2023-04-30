using OpenTK.Mathematics;

namespace Vivid.Scene
{
    public class BoundingBox
    {
        public Vector3 Min { get; set; }
        public Vector3 Max { get; set; }
        public Vector3 Center
        { get { return (this.Min + this.Max) / 2; } }
        public Vector3 HalfSize
        { get { return (this.Max - this.Min) / 2; } }
        public Vector3 Size
        {
            get
            {
                return Max - Min;
            }
        }

        public BoundingBox(Vector3 min, Vector3 max)
        {
            this.Min = min;
            this.Max = max;
        }

        public BoundingBox(Vivid.Meshes.Mesh mesh)
        {
            Matrix4 world = mesh.Owner.WorldMatrix;

            if (mesh == null || mesh.Vertices.Count == 0)
            {
                this.Min = Vector3.Zero;
                this.Max = Vector3.Zero;
                return;
            }

            // Get the world space vertices of the mesh
            Vector3[] worldVertices = new Vector3[mesh.Vertices.Count];
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                var pos = new Vector3(mesh.Vertices[i].Position.X, mesh.Vertices[i].Position.Y, mesh.Vertices[i].Position.Z);
                worldVertices[i] = Vector3.TransformVector(pos, world);
            }

            // Calculate the bounding box based on the mesh vertices
            Vector3 min = worldVertices[0];
            Vector3 max = worldVertices[0];

            for (int i = 1; i < worldVertices.Length; i++)
            {
                min = Vector3.ComponentMin(min, worldVertices[i]);
                max = Vector3.ComponentMax(max, worldVertices[i]);
            }

            this.Min = min;
            this.Max = max;
        }

        public bool Intersects(BoundingBox other)
        {
            if (this.Max.X < other.Min.X || this.Min.X > other.Max.X) return false;
            if (this.Max.Y < other.Min.Y || this.Min.Y > other.Max.Y) return false;
            if (this.Max.Z < other.Min.Z || this.Min.Z > other.Max.Z) return false;

            return true;
        }

        public bool Contains(Vector3 point)
        {
            if (point.X < this.Min.X || point.X > this.Max.X) return false;
            if (point.Y < this.Min.Y || point.Y > this.Max.Y) return false;
            if (point.Z < this.Min.Z || point.Z > this.Max.Z) return false;

            return true;
        }

        public int CountVerticesInBoundingBox(List<Entity> entities)
        {
            BoundingBox boundingBox = this;
            int vertexCount = 0;

            foreach (Entity entity in entities)
            {
                var world = entity.WorldMatrix;
                foreach (Vivid.Meshes.Mesh mesh in entity.Meshes)
                {
                    BoundingBox meshBoundingBox = mesh.Owner.ComputeMeshBoundingBox(mesh,false);

                    if (Intersects(meshBoundingBox))
                    {
                        foreach (Vector3 vertex in mesh.Positions)
                        {
                            //Vector3 vertexWorldSpace = Vector3.Transform(vertex, entity.WorldMatrix);
                            Vector3 tv = Vector3.TransformVector(vertex, world);

                            if (Contains(tv))
                            {
                                vertexCount++;
                            }
                        }
                    }
                }
            }

            return vertexCount;
        }

        public Vector3[] GetCorners()
        {
            Vector3[] corners = new Vector3[8];

            corners[0] = new Vector3(Min.X, Min.Y, Min.Z);
            corners[1] = new Vector3(Max.X, Min.Y, Min.Z);
            corners[2] = new Vector3(Max.X, Max.Y, Min.Z);
            corners[3] = new Vector3(Min.X, Max.Y, Min.Z);
            corners[4] = new Vector3(Min.X, Min.Y, Max.Z);
            corners[5] = new Vector3(Max.X, Min.Y, Max.Z);
            corners[6] = new Vector3(Max.X, Max.Y, Max.Z);
            corners[7] = new Vector3(Min.X, Max.Y, Max.Z);

            return corners;
        }

        public Vector3 GetCorner(int id)
        {
            return GetCorners()[id];
        }

        public bool IntersectsBoundingBox(Vector3 a, Vector3 b, Vector3 c)
        {
            BoundingBox boundingBox = this;
            // Calculate the minimum and maximum x, y, and z values of the bounding box
            float minX = boundingBox.Min.X;
            float minY = boundingBox.Min.Y;
            float minZ = boundingBox.Min.Z;
            float maxX = boundingBox.Max.X;
            float maxY = boundingBox.Max.Y;
            float maxZ = boundingBox.Max.Z;

            // Check if any of the vertices of the triangle are inside the bounding box
            if (boundingBox.Contains(a) || boundingBox.Contains(b) || boundingBox.Contains(c))
            {
                return true;
            }

            // Calculate the minimum and maximum x, y, and z values of the triangle
            float triMinX = Math.Min(a.X, Math.Min(b.X, c.X));
            float triMinY = Math.Min(a.Y, Math.Min(b.Y, c.Y));
            float triMinZ = Math.Min(a.Z, Math.Min(b.Z, c.Z));
            float triMaxX = Math.Max(a.X, Math.Max(b.X, c.X));
            float triMaxY = Math.Max(a.Y, Math.Max(b.Y, c.Y));
            float triMaxZ = Math.Max(a.Z, Math.Max(b.Z, c.Z));

            // Check if the bounding box and the triangle intersect on any axis
            if (triMaxX < minX || triMinX > maxX) return false; // No intersection on x-axis
            if (triMaxY < minY || triMinY > maxY) return false; // No intersection on y-axis
            if (triMaxZ < minZ || triMinZ > maxZ) return false; // No intersection on z-axis

            // If we made it here, there is an intersection
            return true;
        }

        public List<BoundingBox> SubdivideBoundingBox()
        {
            var boundingBox = this;
            Vector3 center = (boundingBox.Max + boundingBox.Min) / 2f;

            List<BoundingBox> subNodes = new List<BoundingBox>();
            subNodes.Add(new BoundingBox(boundingBox.Min, center));  // Bottom Left Front
            subNodes.Add(new BoundingBox(new Vector3(center.X, boundingBox.Min.Y, boundingBox.Min.Z), new Vector3(boundingBox.Max.X, center.Y, center.Z)));  // Bottom Right Back
            subNodes.Add(new BoundingBox(new Vector3(center.X, boundingBox.Min.Y, center.Z), new Vector3(boundingBox.Max.X, center.Y, boundingBox.Max.Z)));  // Bottom Right Front
            subNodes.Add(new BoundingBox(new Vector3(boundingBox.Min.X, boundingBox.Min.Y, center.Z), new Vector3(center.X, center.Y, boundingBox.Max.Z)));  // Bottom Left Back
            subNodes.Add(new BoundingBox(new Vector3(boundingBox.Min.X, center.Y, boundingBox.Min.Z), new Vector3(center.X, boundingBox.Max.Y, center.Z)));  // Top Left Front
            subNodes.Add(new BoundingBox(new Vector3(center.X, center.Y, boundingBox.Min.Z), new Vector3(boundingBox.Max.X, boundingBox.Max.Y, center.Z)));  // Top Right Front
            subNodes.Add(new BoundingBox(center, boundingBox.Max));  // Top Right Back
            subNodes.Add(new BoundingBox(new Vector3(boundingBox.Min.X, center.Y, center.Z), new Vector3(center.X, boundingBox.Max.Y, boundingBox.Max.Z)));  // Top Left Back

            return subNodes;
        }
    }
}