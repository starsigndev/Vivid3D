using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Vivid.Scene;
using Vivid.Meshes;

namespace Vivid.CSG
{
    public class CSGGen
    {

        public enum CSGOpType
        {
            Union,
            Intersection,
            Subtraction
        }
        public Entity Perform(Entity Input,Entity Receiver,CSGOpType type)
        {
            switch (type)
            {
                case CSGOpType.Union:
                    Receiver = Union(Receiver, Input);
                    break;
                case CSGOpType.Intersection:
                    Receiver = Intersection(Receiver, Input);
                    break;
                case CSGOpType.Subtraction:
                    Receiver = Subtraction(Receiver, Input);
                    break;
            }
            return Receiver;
        }

        public Entity Union(Entity recevier,Entity input)
        {

            var tris1 = recevier.Meshes[0].Triangles;
            var tris2 = input.Meshes[0].Triangles;
            var verts1 = recevier.Meshes[0].Vertices;
            var verts2 = input.Meshes[0].Vertices;
            //

            int vert_base = recevier.Meshes[0].Vertices.Count;

            var tris3 = new List<Triangle>();

            foreach(var t in tris1)
            {
                tris3.Add(t);
            }

            foreach(var t in tris2.ToArray())
            {
                var new_tri = new Triangle();
                new_tri.V0 = vert_base + t.V0;
                new_tri.V1 = vert_base + t.V1;
                new_tri.V2 = vert_base + t.V2;
                tris3.Add(new_tri);

            }

            List<Vertex> vertices = new List<Vertex>();

            foreach(var v in verts1)
            {

                var vpos = v.Position;
                var vnorm = v.Position;

                vpos = recevier.TransformPosition(vpos);
                vnorm = recevier.TransformVector(vnorm);

                Vertex new_vert = new Vertex();

                new_vert.Position = vpos;
                new_vert.Normal = vnorm;
                new_vert.Color = v.Color;
                new_vert.TexCoord = v.TexCoord;
                new_vert.BiNormal = v.BiNormal;
                new_vert.Tangent = v.Tangent;

                vertices.Add(new_vert);
            }

            foreach (var v in verts2)
            {

                var vpos = v.Position;
                var vnorm = v.Position;

                vpos = input.TransformPosition(vpos);
                vnorm = input.TransformVector(vnorm);

                Vertex new_vert = new Vertex();

                new_vert.Position = vpos;
                new_vert.Normal = vnorm;
                new_vert.Color = v.Color;
                new_vert.TexCoord = v.TexCoord;
                new_vert.BiNormal = v.BiNormal;
                new_vert.Tangent = v.Tangent;

                vertices.Add(new_vert);
                //vertices.Add(v);

            }

            Entity res = new Entity();
            res.Position = Vector3.Zero;
            res.Rotation = Matrix4.Identity;

            Meshes.Mesh mesh = new Meshes.Mesh(res);

            mesh.Vertices = vertices;
            mesh.Triangles = tris3;

            mesh.CreateBuffers();

            res.AddMesh(mesh);

            mesh.Material = recevier.Meshes[0].Material;

            return res;
        }

        public Entity Intersection(Entity receiver,Entity input)
        {
            List<Triangle> receiverTriangles = receiver.Meshes[0].Triangles;
            List<Triangle> operatorTriangles = input.Meshes[0].Triangles;

            // Retrieve the list of vertices from the receiver model
            List<Vertex> receiverVertices = receiver.Meshes[0].Vertices;

            // Perform intersection between receiver model and operator shape
            List<Vertex> intersectionVertices = new List<Vertex>();
            List<Triangle> intersectedTriangles = new List<Triangle>();

            foreach (Triangle receiverTriangle in receiverTriangles)
            {
                foreach (Triangle operatorTriangle in operatorTriangles)
                {
                    // Get the vertices of the receiver triangle
                    Vertex v1 = receiverVertices[receiverTriangle.V0].Clone();
                    Vertex v2 = receiverVertices[receiverTriangle.V1].Clone();
                    Vertex v3 = receiverVertices[receiverTriangle.V2].Clone();

                    // Get the vertices of the operator triangle
                    Vertex u1 = input.Meshes[0].Vertices[operatorTriangle.V0].Clone();
                    Vertex u2 = input.Meshes[0].Vertices[operatorTriangle.V1].Clone();
                    Vertex u3 = input.Meshes[0].Vertices[operatorTriangle.V2].Clone();

                    //v1.Position = receiver.TransformPosition(v1.Position);
                   // v2.Position = receiver.TransformPosition(v2.Position);
                   // v3.Position = receiver.TransformPosition(v3.Position);

                   // u1.Position = input.TransformPosition(u1.Position);
                   // u2.Position = input.TransformPosition(u2.Position);
                   // u3.Position = input.TransformPosition(u3.Position);


                    // Check for intersection between receiver and operator triangles
                    if (IntersectsTriangle(v1, v2, v3, u1, u2, u3, out Vertex[] newIntersectionVertices))
                    {
                        // Add the intersection vertices to the intersected vertices
                        intersectionVertices.AddRange(newIntersectionVertices);

                        // Create a new triangle for the intersection area using the updated vertex indices
                        int startIndex = intersectionVertices.Count - 3;
                        Triangle intersectedTriangle = new Triangle(startIndex, startIndex + 1, startIndex + 2);
                        intersectedTriangles.Add(intersectedTriangle);
                    }
                }
            }

            // Create a new model with the intersected vertices and triangles
            //Model newModel = new Model();
            // newModel.SetVertices(intersectionVertices);
            // newModel.SetTriangles(intersectedTriangles);
            Entity res = new Entity();

            Meshes.Mesh mesh = new Meshes.Mesh(res);
            mesh.Vertices = intersectionVertices;
            mesh.Triangles = intersectedTriangles;

            mesh.Material = receiver.Meshes[0].Material;

            mesh.CreateBuffers();
            res.AddMesh(mesh);




            return res;
        }

        public Entity Subtraction(Entity receiver,Entity input)
        {
            List<Triangle> receiverTriangles = receiver.Meshes[0].Triangles;
            List<Triangle> inputTriangles = input.Meshes[0].Triangles;

            // Retrieve the list of vertices from the receiver and input models
            List<Vertex> receiverVertices = receiver.Meshes[0].Vertices;
            List<Vertex> inputVertices = input.Meshes[0].Vertices;

            // Perform subtraction between receiver model and input model
            List<Vertex> subtractionVertices = new List<Vertex>();
            List<Triangle> subtractedTriangles = new List<Triangle>();

            foreach (Triangle receiverTriangle in receiverTriangles)
            {
                bool intersected = false;

                foreach (Triangle inputTriangle in inputTriangles)
                {
                    if (IntersectsTriangle(receiverVertices, receiverTriangle, inputVertices, inputTriangle, out Vertex[] intersectionVertices,receiver,input))
                    {
                        subtractedTriangles.AddRange(inputTriangles.Except(new[] { inputTriangle }));

                        subtractedTriangles.AddRange(SubtractTriangle(receiverVertices, receiverTriangle, intersectionVertices));

                        intersected = true;
                        break;
                    }
                }

                if (!intersected)
                {
                    subtractedTriangles.Add(receiverTriangle);
                }
            }

            // Create a new model with the subtracted vertices and triangles
            Entity result = new Entity();

            Meshes.Mesh mesh = new Meshes.Mesh(result);
            mesh.Vertices = receiverVertices;
            mesh.Triangles = subtractedTriangles;

            mesh.Material = receiver.Meshes[0].Material;

            mesh.CreateBuffers();
            result.AddMesh(mesh);

            return result;
        }

        private bool IntersectsTriangle(List<Vertex> receiverVertices, Triangle receiverTriangle, List<Vertex> inputVertices, Triangle inputTriangle, out Vertex[] intersectionVertices,Entity receiver,Entity input)
        {
            Vertex v1 = receiverVertices[receiverTriangle.V0].Clone();
            Vertex v2 = receiverVertices[receiverTriangle.V1].Clone();
            Vertex v3 = receiverVertices[receiverTriangle.V2].Clone();

            Vertex u1 = inputVertices[inputTriangle.V0].Clone();
            Vertex u2 = inputVertices[inputTriangle.V1].Clone();
            Vertex u3 = inputVertices[inputTriangle.V2].Clone();

            // Transform the vertices based on their respective transformation matrices
            v1.Position = receiver.TransformPosition(v1.Position);
            v2.Position = receiver.TransformPosition(v2.Position);
            v3.Position = receiver.TransformPosition(v3.Position);

            u1.Position = input.TransformPosition(u1.Position);
            u2.Position = input.TransformPosition(u2.Position);
            u3.Position = input.TransformPosition(u3.Position);

            // Check for intersection between receiver and input triangles
            if (IntersectTriangleWithLine(v1, v2, v3, u1.Position, u2.Position - u1.Position, out Vector3 intersectionPoint))
            {
                // Calculate the interpolation weights for normals and texture coordinates
                float weight1 = Vector3.Dot(intersectionPoint - v1.Position, Vector3.Cross(v2.Position - v1.Position, v3.Position - v1.Position)) /
                                Vector3.Dot(u2.Position - u1.Position, Vector3.Cross(v2.Position - v1.Position, v3.Position - v1.Position));
                float weight2 = Vector3.Dot(intersectionPoint - v1.Position, Vector3.Cross(v3.Position - v1.Position, v1.Position - v2.Position)) /
                                Vector3.Dot(u2.Position - u1.Position, Vector3.Cross(v3.Position - v1.Position, v1.Position - v2.Position));
                float weight3 = 1.0f - weight1 - weight2;

                // Interpolate the normals and texture coordinates at the intersection point
                Vertex intersectionVertex = new Vertex(intersectionPoint,
                                                       (v1.Normal * weight1) + (v2.Normal * weight2) + (v3.Normal * weight3),
                                                       (v1.TexCoord * weight1) + (v2.TexCoord * weight2) + (v3.TexCoord * weight3));

                intersectionVertices = new[] { intersectionVertex };
                return true;
            }

            intersectionVertices = null;
            return false;
        }


        private List<Triangle> SubtractTriangle(List<Vertex> receiverVertices, Triangle receiverTriangle, Vertex[] intersectionVertices)
        {
            int startIndex = receiverVertices.Count;
            receiverVertices.AddRange(intersectionVertices);

            int v0 = receiverTriangle.V0;
            int v1 = receiverTriangle.V1;
            int v2 = receiverTriangle.V2;

            return new List<Triangle>
    {
        new Triangle(v0, v1, startIndex),
        new Triangle(v1, v2, startIndex),
        new Triangle(v2, v0, startIndex)
    };
        }


        private bool IntersectsTriangle(Vertex v1, Vertex v2, Vertex v3, Vertex u1, Vertex u2, Vertex u3, out Vertex[] intersectionVertices)
        {

            intersectionVertices = null;

            // Calculate the edge vectors of the first triangle
            Vector3 edge1 = v2.Position - v1.Position;
            Vector3 edge2 = v3.Position - v1.Position;

            // Calculate the normal of the first triangle
            Vector3 triangleNormal1 = Vector3.Cross(edge1, edge2);

            // Calculate the edge vectors of the second triangle
            Vector3 edge3 = u2.Position - u1.Position;
            Vector3 edge4 = u3.Position - u1.Position;

            // Calculate the normal of the second triangle
            Vector3 triangleNormal2 = Vector3.Cross(edge3, edge4);

            // Check if the triangles are parallel (dot product of normals close to 1)
            float dot = Vector3.Dot(triangleNormal1, triangleNormal2);
            if (Math.Abs(dot) > 0.999f)
                return false; // Triangles are parallel, no intersection

            // Calculate the intersection line direction
            Vector3 intersectionLine = Vector3.Cross(triangleNormal1, triangleNormal2);

            // Find a point on the intersection line (using the first vertex of the first triangle)
            Vector3 pointOnLine = v1.Position;

            // Calculate the intersection points of the two triangles with the intersection line
            bool intersects1 = IntersectTriangleWithLine(v1, v2, v3, pointOnLine, intersectionLine, out Vector3 intersectionPoint1);
            bool intersects2 = IntersectTriangleWithLine(u1, u2, u3, pointOnLine, intersectionLine, out Vector3 intersectionPoint2);

            if (!intersects1 || !intersects2)
                return false; // Triangles do not intersect

            // Calculate the interpolation weights for normals and texture coordinates of the first triangle
            float weight1 = Vector3.Dot(intersectionPoint1 - v1.Position, triangleNormal1) / Vector3.Dot(edge1, triangleNormal1);
            float weight2 = Vector3.Dot(intersectionPoint1 - v1.Position, Vector3.Cross(edge2, triangleNormal1)) / Vector3.Dot(edge1, Vector3.Cross(edge2, triangleNormal1));
            float weight3 = 1.0f - weight1 - weight2;

            // Interpolate the normals and texture coordinates at the intersection point of the first triangle
            Vector3 interpolatedNormal1 = (v1.Normal * weight1) + (v2.Normal * weight2) + (v3.Normal * weight3);
            Vector3 interpolatedTexCoord1 = (v1.TexCoord * weight1) + (v2.TexCoord * weight2) + (v3.TexCoord * weight3);

            // Calculate the interpolation weights for normals and texture coordinates of the second triangle
            weight1 = Vector3.Dot(intersectionPoint2 - u1.Position, triangleNormal2) / Vector3.Dot(edge3, triangleNormal2);
            weight2 = Vector3.Dot(intersectionPoint2 - u1.Position, Vector3.Cross(edge4, triangleNormal2)) / Vector3.Dot(edge3, Vector3.Cross(edge4, triangleNormal2));
            weight3 = 1.0f - weight1 - weight2;

            // Interpolate the normals and texture coordinates at the intersection point of the second triangle
            Vector3 interpolatedNormal2 = (u1.Normal * weight1) + (u2.Normal * weight2) + (u3.Normal * weight3);
            Vector3 interpolatedTexCoord2 = (u1.TexCoord * weight1) + (u2.TexCoord * weight2) + (u3.TexCoord * weight3);

            // Create the intersection vertices
            Vertex vertex1 = new Vertex(intersectionPoint1, interpolatedNormal1, interpolatedTexCoord1);
            Vertex vertex2 = new Vertex(intersectionPoint2, interpolatedNormal2, interpolatedTexCoord2);


            intersectionVertices = new Vertex[] { vertex1, vertex2 };

            return true; // Triangles intersect
        }

        private bool IntersectTriangleWithLine(Vertex v1, Vertex v2, Vertex v3, Vector3 pointOnLine, Vector3 lineDirection, out Vector3 intersectionPoint)
        {
            // Möller-Trumbore intersection algorithm

            const float epsilon = 1e-5f;

            // Calculate the edge vectors of the triangle
            Vector3 edge1 = v2.Position - v1.Position;
            Vector3 edge2 = v3.Position - v1.Position;

            // Calculate the normal of the triangle
            Vector3 triangleNormal = Vector3.Cross(edge1, edge2);

            // Calculate the determinant
            float determinant = Vector3.Dot(lineDirection, Vector3.Cross(edge2, triangleNormal));

            // Check if the line is parallel to the triangle's plane
            if (Math.Abs(determinant) < epsilon)
            {
                intersectionPoint = Vector3.Zero;
                return false; // Line is parallel to the plane, no intersection
            }

            float invDeterminant = 1.0f / determinant;

            // Calculate the barycentric coordinates
            Vector3 distance = pointOnLine - v1.Position;
            float u = Vector3.Dot(distance, Vector3.Cross(lineDirection, edge2)) * invDeterminant;
            float v = Vector3.Dot(distance, Vector3.Cross(edge1, lineDirection)) * invDeterminant;

            // Check if the intersection point is within the valid line segment and inside the triangle
            if (u >= 0.0f && v >= 0.0f && u + v <= 1.0f)
            {
                // Calculate the intersection point
                intersectionPoint = v1.Position + u * edge1 + v * edge2;
                return true; // Intersection point is inside the triangle
            }

            intersectionPoint = Vector3.Zero;
            return false; // Intersection point is outside the triangle
        }
    }
}
