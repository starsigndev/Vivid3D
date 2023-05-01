using OpenTK.Mathematics;
using System.Runtime.CompilerServices;
using Vivid.Engine;
using Vivid.Meshes;
using Vivid.Physx;
using Vivid.Renderers;
using Vivid.State;

namespace Vivid.Scene
{
    public enum RenderMode
    {
        PBR, Fullbright
    }

    public enum EntityType
    {
        Static, Dynamic, Skeletal
    }

    public class Entity : Node
    {
        public EntityType EntityType
        {
            get;
            set;
        }

        public BodyType BodyKind
        {
            get;
            set;
        }

        public RenderMode RenderMode
        {
            get;
            set;
        }

        public List<Vivid.Meshes.Mesh> Meshes
        {
            get;
            set;
        }

        public PXBody Body
        {
            get;
            set;
        }

        public bool ClearZBeforeRender
        {
            get;
            set;
        }

        public bool WriteDepth
        {
            get;
            set;
        }

        public bool DepthTest
        {
            get;
            set;
        }

        public Entity()
        {
            Meshes = new List<Vivid.Meshes.Mesh>();
            BodyKind = BodyType.None;
            Body = null;
            RenderMode = RenderMode.PBR;
            ClearZBeforeRender = false;
            EntityType = EntityType.Static;
            WriteDepth = true;
            DepthTest = true;
        }

        public void AddMesh(Vivid.Meshes.Mesh mesh)
        {
            Meshes.Add(mesh);
        }

        public override void BeginPhysics()
        {
            if (!Enabled) return;
         

            switch (BodyKind)
            {
                case BodyType.Box:

                    var bb = BoundsNoTransform;

                    Body = new PXBox(bb.HalfSize.X, bb.HalfSize.Y, bb.HalfSize.Z, this);
                    Body.SetPose(Position, Rotation);


                    break;
                case BodyType.Sphere:

                    Body = new PXSphere(Bounds.HalfSize.X);
                    Body.SetPose(Position, Rotation);

                    break;

                case BodyType.ConvexHull:

                    Body = new PXConvexHull(Meshes[0]);
                    Body.SetPose(Position, Rotation);

                    break;

                case BodyType.TriMesh:

                    Body = new PXTriMesh(Meshes, 0);
                   
                    break;

                case BodyType.FPS:
                   
                    break;
            }

            foreach (var node in Nodes)
            {
                node.BeginPhysics();
            }

        }

        public override void MoveBody(float x, float y, float z)
        {
            if (Body != null)
            {
                Vector3 rm = TransformVector(new Vector3(x, y, z));

                //    Body.DynamicBody.AddForce(new System.Numerics.Vector3(rm.x, rm.y, rm.z));
            }
        }

        public override void TurnBody(float p, float y, float r)
        {
            //base.PXTurn(p, y, r);
            if (Body != null)
            {
                
            }
        }

        public override bool BodyOnGround()
        {
            //return base.BodyOnGround();

            //   if(QPhysics.Raycast(Position + new Vec3(0,-0.65f,0), Position + new Vec3(0, -1.3f, 0),Body)  ){
            return true;

            //            }

            return false;
        }

        public override void UpdatePX()
        {
            if (Body != null)
            {
                //    Body.SetPose(Position, Rotation);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void UpdatePhysics()
        {
            if (!Enabled) return;
            if (Body != null)
            {
                if (Body is PXTriMesh)
                {
                }
                else
                {
                    Position = Body.GetPos();
                    Rotation = Body.GetRot();
                    //Body.Body.Velocity = new BepuPhysics.BodyVelocity(new System.Numerics.Vector3(55, 55, 55));
                  

                   // Rotation = Body.GetRot();
                }
            }

            foreach (var node in Nodes)
            {
                node.UpdatePhysics();
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void RenderSimple()
        {
            GLHelper.PreRenderStandard(WriteDepth, DepthTest);

            foreach (var mesh in Meshes)
            {
                var material = mesh.FullBrightMaterial;

                material.Shader.Camera = RenderGlobals.CurrentCamera;
                material.Shader.Entity = this;
                material.Shader.Light = null;

                material.Shader.Bind();

                mesh.Material.ColorMap.Bind(0);

                mesh.RenderMesh();

                material.Shader.Unbind();
                mesh.Material.ColorMap.Unbind(0);
            }
            foreach (var node in Nodes)
            {
                node.RenderSimple();
            }

            //base.RenderSimple();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Render(Light l, Camera c, bool firstPass)
        {
            if (!Enabled) return;

            if (firstPass)
            {
                Vivid.State.GLState.State = State.CurrentGLState.LightFirstPass;
                // GLHelper.PreRenderStandard(WriteDepth,DepthTest);
            }
            else
            {
                Vivid.State.GLState.State = State.CurrentGLState.LightSecondPass;
                //   GLHelper.PreRenderStandardSecondPass();
            }

            foreach (var mesh in Meshes)
            {
                var material = mesh.LightMaterial;

                material.Shader.Camera = c;
                material.Shader.Entity = this;
                material.Shader.Light = l;

                material.Shader.Bind();

                mesh.Material.ColorMap.Bind(0);
                mesh.Material.NormalMap.Bind(1);
                mesh.Material.SpecularMap.Bind(2);
                l.RTC.Cube.Bind(3);

                mesh.RenderMesh();

                material.Shader.Unbind();
                mesh.Material.ColorMap.Unbind(0);
                mesh.Material.NormalMap.Unbind(1);
                mesh.Material.SpecularMap.Unbind(2);
                l.RTC.Cube.Release(3);

                //Vec4 eLightDir2;
                //Vec3 eLightDir;

                //eLightDir2 = l.Rotation * new Vec4(0, 0, 1,1);
                //eLightDir = eLightDir2.xyz;

                //IntPtr rtc = l.RTC.rtc;

                //GemBridge.gem_MeshRenderer_RenderLit(RenderGlobals.MeshRenderer, mesh.MeshBuffer,c.Position.handle, c.TransformVector(new Vec3(0, 0, 1)).handle, c.Projection.handle, c.WorldMatrix.handle, WorldMatrix.handle,(int)l.Type,eLightDir.handle,l.InnerCone,l.OuterCone,l.Position.handle ,l.Diffuse.handle,l.Specular.handle ,l.Range, mesh.Material.ColorMap.CObj,mesh.Material.NormalMap.CObj,mesh.Material.SpecularMap.CObj,mesh.Material.DisplaceMap.CObj, rtc,firstPass);
                //Marshal.FreeHGlobal(np);
            }

            foreach (var node in Nodes)
            {
                node.Render(l, c, firstPass);
            }
        }

        public override void RenderPositions(Camera c)
        {
            //base.RenderPositions(c);
            foreach (var mesh in Meshes)
            {
                //  GemBridge.gem_MeshRenderer_RenderPositions(RenderGlobals.MeshRenderer, mesh.MeshBuffer, c.Position.handle, c.Projection.handle, c.WorldMatrix.handle, WorldMatrix.handle);
            }
        }

        public override void RenderNormals(Camera c)
        {
            foreach (var mesh in Meshes)
            {
                //  GemBridge.gem_MeshRenderer_RenderNormals(RenderGlobals.MeshRenderer, mesh.MeshBuffer, c.Position.handle, c.Projection.handle, c.WorldMatrix.handle, WorldMatrix.handle);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void RenderDepth(Camera c, bool ignore_child = false)
        {
            if (!Enabled) return;

            GLHelper.PreRenderStandard(WriteDepth, DepthTest);

            foreach (var mesh in Meshes)
            {
                var material = mesh.DepthMaterial;

                material.Shader.Camera = c;
                material.Shader.Entity = this;
                material.Shader.Light = null;

                material.Shader.Bind();

                // mesh.Material.ColorMap.Bind(0);

                mesh.RenderMesh();

                material.Shader.Unbind();
                // mesh.Material.ColorMap.Unbind(0);

                //   GemBridge.gem_MeshRenderer_RenderDepth(RenderGlobals.MeshRenderer, mesh.MeshBuffer, c.Projection.handle, c.WorldMatrix.handle, WorldMatrix.handle,c.Position.handle,c.DepthStart,c.DepthEnd);
            }
            if (!ignore_child)
            {
                foreach (var node in Nodes)
                {
                    node.RenderDepth(c);
                }
            }

            //  base.RenderSimple();
        }

        public override Node Clone()
        {
            var clone = new Entity();
            clone.Rotation = Rotation;
            clone.Position = Position;
            clone.Root = Root;
            clone.Nodes = Nodes;
            clone.Name = Name + "_Clone";
            clone.Meshes = Meshes;
            return clone;
            //return base.Clone();
        }
        public BoundingBox BoundsNoTransform
        {
            get
            {
                return ComputeOverallBoundingBox(Meshes, true);
            }
        }
        public override BoundingBox Bounds
        {
            get
            {
                //if (_MBB == null)
                //{
                _MBB = ComputeOverallBoundingBox(Meshes);
                // }
                return _MBB;
            }
        }

        private BoundingBox _MBB;

        public Vivid.Scene.BoundingBox ComputeOverallBoundingBox(List<Vivid.Meshes.Mesh> meshes,bool noTransform = false)
        {
            if (meshes.Count == 0)
            {
                // Empty list, return an invalid bounding box
                return new Vivid.Scene.BoundingBox(Vector3.Zero, Vector3.Zero);
            }
            else if (meshes.Count == 1)
            {
                // Only one mesh, return its bounding box
                return ComputeMeshBoundingBox(meshes[0],noTransform);
            }
            else
            {
                // Multiple meshes, compute the overall bounding box
                Vector3 min = ComputeMeshBoundingBox(meshes[0],noTransform).Min;
                Vector3 max = ComputeMeshBoundingBox(meshes[0],noTransform).Max;

                for (int i = 1; i < meshes.Count; i++)
                {
                    Vivid.Scene.BoundingBox meshBounds = ComputeMeshBoundingBox(meshes[i],noTransform);
                    min = Vector3.ComponentMin(min, meshBounds.Min);
                    max = Vector3.ComponentMax(max, meshBounds.Max);
                }

                return new Vivid.Scene.BoundingBox(min, max);
            }
        }

        private BoundingBox _BB = null;

        public Vivid.Scene.BoundingBox ComputeMeshBoundingBox(Vivid.Meshes.Mesh mesh,bool noTransform)
        {
            //   if (_BB != null) return _BB;
            // Get the vertices of the mesh
            List<Vertex> vertices = mesh.Vertices;

            var world = mesh.Owner.WorldMatrix;

            Vector3 min, max;

            if (noTransform)
            {
                min = vertices[0].Position;

                max = vertices[0].Position;
            }
            else
            {
                // Compute the minimum and maximum extents of the mesh
                min = Vector3.TransformPosition(vertices[0].Position, world);

                max = Vector3.TransformPosition(vertices[0].Position, world);
            }

            foreach (var v in vertices)
            {
                Vector3 pos;
                //  verts[vv] = new Vector3(vertices[vv].Position.X, vertices[vv].Position.Y, vertices[vv].Position.Z);
                if (noTransform)
                {
                    pos = v.Position;
                }
                else
                {
                     pos = Vector3.TransformPosition(v.Position, world);
                }
                min = Vector3.ComponentMin(min, pos);
                max = Vector3.ComponentMax(max, pos);
            }

            // Create and return a bounding box with the minimum and maximum extents
            _BB = new Vivid.Scene.BoundingBox(min, max);
            return _BB;
        }
    }
}