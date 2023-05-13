using OpenTK.Mathematics;
using System.Runtime.CompilerServices;
using Vivid.App;
using Vivid.Materials;
using Vivid.Mesh;
using Vivid.Meshes;
using Vivid.Physx;
using Vivid.Renderers;

namespace Vivid.Scene
{
    public class Ray
    {
        public Vector3 Pos;
        public Vector3 Dir;
    }

    public class RaycastResult
    {
        public Vector3 Point
        {
            get;
            set;
        }

        public Vector3 Normal
        {
            get;
            set;
        }

        public Vector3 UV
        {
            get;
            set;
        }

        public float Distance
        {
            get;
            set;
        }

        public Node Node
        {
            get;
            set;
        }

        public Entity Entity
        {
            get;
            set;
        }

        public Vivid.Meshes.Mesh Mesh
        {
            get;
            set;
        }

        public bool Hit
        {
            get;
            set;
        }

        public RaycastResult()
        {
            Point = Vector3.Zero;
            Normal = Vector3.Zero;
            Node = null;
            Entity = null;
            Hit = false;
            Distance = 9999;
        }
    }

    /// <summary>
    /// A scene contains nodes, entities, lights and cameras.
    /// It is used to render a whole game's 3d output, including levels and characters.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// This is the top level node. it should not be replaced, it should be added to. I.e, Scene.Root.AddNode(myChar)
        /// </summary>
        public Node Root
        {
            get;
            set;
        }

        /// <summary>
        /// The list of spawn points, which can be defined in the scene editor and imported by loading the scene.
        /// </summary>
        public List<SpawnPoint> Spawns
        {
            get;
            set;
        }

        /// <summary>
        /// The current viewpoint the scene will be rendered from.
        /// </summary>
        public Camera MainCamera
        {
            get;
            set;
        }

        /// <summary>
        /// The lights that light the scene. There should be no limit to how many.
        /// At least 1 is needed for lighting and shadows to work.
        /// </summary>

        public List<Light> Lights
        {
            get;
            set;
        }

        public List<SceneEvent> PossibleEvents
        {
            get;
            set;
        }

        public List<SceneEvent> CurrentEvents
        {
            get;
            set;
        }

        public CubeRenderer ShadowRender
        {
            get;
            set;
        }

        public bool PhysicsBegan
        {
            get;
            set;
        }

        public PropertyList Properties
        {
            get;
            set;
        }

        /// <summary>
        /// A List of mesh lines to render. A MeshLines is a list of 3D lines, rendered on the gpu using
        /// any given color.
        /// </summary>
        public List<MeshLines> MeshLines
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a full list of every 'Entity' in the scene.
        /// </summary>
        public List<Entity> EntityList
        {
            get
            {
                if (_EntList != null)
                {
                    //   return _EntList;
                }
                List<Entity> list = new List<Entity>();
                AddEntities(list, Root);
                _EntList = list;
                return list;
            }
        }

        private List<Entity> _EntList = null;

        private void AddEntities(List<Entity> list, Node node)
        {
            if (node is Entity)
            {
                list.Add(node as Entity);
            }

            foreach (var sub in node.Nodes)
            {
                AddEntities(list, sub);
            }
        }

        /// <summary>
        /// The bounds of the overall scene, tightly fit around every internal node/entity.
        /// </summary>
        public BoundingBox Bounds
        {
            get
            {
                return ComputeOverallBoundingBox(EntityList);
                //BoundingBox bb = new BoundingBox()
            }

            set
            {
            }
        }

        /// <summary>
        /// Returns how many nodes are in the whole scene.
        /// </summary>
        public int NodeCount
        {
            get
            {
                return EntityList.Count;
            }
        }

        /// <summary>
        /// Returns the total number of tris being rendered in the whole scene.
        /// </summary>
        public int TriCount
        {
            get
            {
                int tc = 0;
                foreach (var ent in EntityList)
                {
                    foreach (var mesh in ent.Meshes)
                    {
                        tc += mesh.Triangles.Count;
                    }
                }
                return tc;
            }
        }

        /// <summary>
        /// Returns the total number of verticies in the whole scene.
        /// </summary>
        public int VertexCount
        {
            get
            {
                int vc = 0;
                foreach (var ent in EntityList)
                {
                    foreach (var mesh in ent.Meshes)
                    {
                        vc += mesh.Vertices.Count;
                    }
                }
                return vc;
            }
        }

        public BoundingBox ComputeOverallBoundingBox(List<Entity> entities)
        {
            if (entities.Count == 0)
            {
                // Empty list, return an invalid bounding box
                return new BoundingBox(Vector3.Zero, Vector3.Zero);
            }
            else if (entities.Count == 1)
            {
                // Only one entity, return its bounding box
                return entities[0].Bounds;
            }
            else
            {
                // Multiple entities, compute the overall bounding box
                Vector3 min = entities[0].Bounds.Min;
                Vector3 max = entities[0].Bounds.Max;

                for (int i = 1; i < entities.Count; i++)
                {
                    min = Vector3.ComponentMin(min, entities[i].Bounds.Min);
                    max = Vector3.ComponentMax(max, entities[i].Bounds.Max);
                }

                return new BoundingBox(min, max);
            }
        }

        public Stack<SceneState> States
        {
            get;
            set;
        }

        public void PushState(SceneState state)
        {

            States.Push(state);
            state.Start();

        }

        public void PopState()
        {

            if (States.Count > 0)
            {
                States.Peek().Stop();
                States.Pop();
            }

        }
        
        public void AddPossibleEvent(SceneEvent sceneEvent){
            
            PossibleEvents.Add(sceneEvent);
            
        }

        public void StartEvent(string name)
        {

            foreach (var ev in PossibleEvents)
            {
                if (ev.EventName == name)
                {
                    CurrentEvents.Add(ev);
                    ev.Start();
                }
            }
            
        }

        public void StopEvent(string name)
        {
            foreach(var ev in CurrentEvents)
            {
                if(ev.EventName == name)
                {
                    ev.Stop();
                    CurrentEvents.Remove(ev);
                    break;
                }
            }
        }

        /// <summary>
        /// Constructs a empty usable scene. You need to add nodes, lights and entities for it to render anything.
        /// </summary>
        public Scene()
        {
            Root = new Node();
            Lights = new List<Light>();
            MainCamera = new Camera();
            ShadowRender = new CubeRenderer(this, null);
            VividApp.CurrentScene = this;
            PhysicsBegan = false;
            Properties = new PropertyList();
            Spawns = new List<SpawnPoint>();
            MeshLines = new List<MeshLines>();
            //Bounds = new BoundingBox();
            sc++;
            States = new Stack<SceneState>();
            PossibleEvents = new List<SceneEvent>();
            CurrentEvents = new List<SceneEvent>();
        }

        public int sc = 0;

        public void AddBoundsLine()
        {
        }

        private void ReadScene(BinaryReader r)
        {
            Root = ReadNode(r);
        }

        private void ReadNodeData(Node node, BinaryReader r)
        {
            node.Position = ReadVec3(r);
            node.Rotation = ReadMat4(r);
            node.Scale = ReadVec3(r);
            node.Name = r.ReadString();
            node.Enabled = r.ReadBoolean();
            node.NodeType = r.ReadString();
        }

        private Materials.MaterialBase ReadMaterial(BinaryReader r)
        {
            MaterialBase mat = new Materials.Materials.Entity.MaterialStandardLight();

            string col = r.ReadString();
            string norm = r.ReadString();
            string spec = r.ReadString();
            string col1 = Path.GetFileName(col);
            string norm1 = Path.GetFileName(norm);
            string spec1 = Path.GetFileName(spec);

            var cc = Content.Content.GlobalFindItem(col);
            var nc = Content.Content.GlobalFindItem(norm);
            var sc = Content.Content.GlobalFindItem(spec);

            if (cc != null)
            {
                mat.ColorMap = new Texture.Texture2D(cc.GetStream(), cc.Width, cc.Height);
            }
            else
            {
                mat.ColorMap = new Texture.Texture2D(col);
            }
            if (nc != null)
            {
                mat.NormalMap = new Texture.Texture2D(nc.GetStream(), nc.Width, nc.Height);
            }
            else
            {
                mat.NormalMap = new Texture.Texture2D(norm);
            }
            if (sc != null)
            {
                mat.SpecularMap = new Texture.Texture2D(sc.GetStream(), sc.Width, sc.Height);
            }
            else
            {
                mat.SpecularMap = new Texture.Texture2D(spec);
            }




            //int b = 5;


            return mat;
        }

        public void ReadMeshData(Entity ent, BinaryReader r)
        {
            Vector3 Readdpos3()
            {
                float x, y, z;
                x = r.ReadSingle();
                y = r.ReadSingle();
                z = r.ReadSingle();
                return new Vector3(x, y, z);
            }

            Vector4 Readdpos4()
            {
                float x, y, z, w;
                x = r.ReadSingle();
                y = r.ReadSingle();
                z = r.ReadSingle();
                w = r.ReadSingle();
                return new Vector4(x, y, z, w);
            }

            int mesh_c = r.ReadInt32();
            for (int i = 0; i < mesh_c; i++)
            {
                Vivid.Meshes.Mesh mesh = new Vivid.Meshes.Mesh(ent);

                int vert_c = r.ReadInt32();
                for (int v = 0; v < vert_c; v++)
                {
                    Vertex vert = new Vertex();
                    vert.Position = Readdpos3();
                    vert.Normal = Readdpos3();
                    vert.Tangent = Readdpos3();
                    vert.BiNormal = Readdpos3();
                    vert.Color = Readdpos4();
                    vert.TexCoord = Readdpos3();
                    vert.BoneIDS = Readdpos4();
                    vert.Weights = Readdpos4();
                    mesh.AddVertex(vert, false);
                }

                int tri_c = r.ReadInt32();
                for (int t = 0; t < tri_c; t++)
                {
                    Triangle tri = new Triangle();

                    tri.V0 = r.ReadInt32();
                    tri.V1 = r.ReadInt32();
                    tri.V2 = r.ReadInt32();

                    mesh.AddTriangle(tri);
                }

                mesh.Material = ReadMaterial(r);
                mesh.CreateBuffers();
                ent.AddMesh(mesh);
            }
        }

        public void ReadPhysicsData(Entity ent, BinaryReader r)
        {
            ent.BodyKind = (BodyType)r.ReadInt32();
        }

        public void ReadEntityData(Entity ent, BinaryReader r)
        {
            ReadNodeData(ent as Node, r);
            ReadMeshData(ent, r);
            ReadPhysicsData(ent, r);

            ent.EntityType = (EntityType)r.ReadInt32();
        }

        private void ReadLightData(Light light, BinaryReader r)
        {
            ReadNodeData(light as Node, r);
            light.Diffuse = ReadVec3(r);
            light.Specular = ReadVec3(r);
            light.Range = r.ReadSingle();
            light.CastShadows = r.ReadBoolean();
            light.InnerCone = r.ReadSingle();
            light.OuterCone = r.ReadSingle();
            light.VolumetricShafts = r.ReadBoolean();
        }

        private void ReadSpawnData(SpawnPoint spawn, BinaryReader r)
        {
            ReadNodeData(spawn as Node, r);
            spawn.Index = r.ReadInt32();
            spawn.Type = r.ReadString();
        }

        private Node ReadNode(BinaryReader r)
        {
            Node res = null;
            int type = r.ReadInt32();
            switch (type)
            {
                case 0:

                    res = new Node();
                    ReadNodeData(res, r);

                    break;

                case 1:

                    res = new Entity() as Node;
                    ReadEntityData(res as Entity, r);

                    break;

                case 2:

                    res = new Light() as Node;
                    ReadLightData(res as Light, r);
                    Lights.Add(res as Light);

                    break;

                case 3:

                    res = new SpawnPoint() as Node;
                    ReadSpawnData(res as SpawnPoint, r);
                    Spawns.Add(res as SpawnPoint);

                    break;
            }

            int node_c = r.ReadInt32();

            for (int i = 0; i < node_c; i++)
            {
                var sub = ReadNode(r);
                res.AddNode(sub);
            }

            return res;
        }

        /// <summary>
        /// Load a whole scene from a memory stream. Usually used with the content system.
        /// </summary>
        /// <param name="stream"></param>
        public void Load(MemoryStream stream)
        {
            stream.Position = 0;
            BinaryReader r = new BinaryReader(stream);
            ReadScene(r);
            r.Close();
        }

        /// <summary>
        /// Loads a whole scene from file.
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path)
        {
            Root = null;
            Lights.Clear();

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

            ReadScene(r);

            r.Close();
            fs.Close();
        }

        /// <summary>
        /// Saves a whole scene to file.
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            if (path is null || path == string.Empty)
            {
                return;
            }
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryWriter w = new BinaryWriter(fs);

            WriteNode(w, Root);

            w.Flush();
            fs.Flush();
            fs.Close();
        }

        private void WriteVec3(BinaryWriter w, Vector3 v)
        {
            w.Write(v.X);
            w.Write(v.Y);
            w.Write(v.Z);
        }

        private Vector3 ReadVec3(BinaryReader r)
        {
            Vector3 res = new Vector3();

            res.X = r.ReadSingle();
            res.Y = r.ReadSingle();
            res.Z = r.ReadSingle();

            return res;
        }

        private Matrix4 ReadMat4(BinaryReader r)
        {
            Matrix4 res = new Matrix4();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    res[i, j] = r.ReadSingle();
                }
            }

            return res;
        }

        private void WriteMat4(BinaryWriter w, Matrix4 m)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    w.Write(m[i, j]);
                }
            }
        }

        private void WriteNodeData(BinaryWriter w, Node node)
        {
            WriteVec3(w, node.Position);
            WriteMat4(w, node.Rotation);
            WriteVec3(w, node.Scale);
            w.Write(node.Name);
            w.Write(node.Enabled);
            w.Write(node.NodeType);
        }

        private void WriteMaterial(BinaryWriter w, Vivid.Materials.MaterialBase mat)
        {
            w.Write(mat.ColorMap.Path);
            w.Write(mat.NormalMap.Path);
            w.Write(mat.SpecularMap.Path);
        }

        private void WriteMeshData(BinaryWriter w, Entity ent)
        {
            void Writedpos3(Vector3 p)
            {
                w.Write(p.X);
                w.Write(p.Y);
                w.Write(p.Z);
            }
            void Writedpos4(Vector4 p)
            {
                w.Write(p.X);
                w.Write(p.Y);
                w.Write(p.Z);
                w.Write(p.W);
            }
            w.Write(ent.Meshes.Count);
            foreach (var mesh in ent.Meshes)
            {
                w.Write(mesh.Vertices.Count);
                foreach (var v in mesh.Vertices)
                {
                    Writedpos3(v.Position);
                    Writedpos3(v.Normal);
                    Writedpos3(v.Tangent);
                    Writedpos3(v.BiNormal);
                    Writedpos4(v.Color);
                    Writedpos3(v.TexCoord);
                    Writedpos4(v.BoneIDS);
                    Writedpos4(v.Weights);
                }

                w.Write(mesh.Triangles.Count);
                foreach (var t in mesh.Triangles)
                {
                    w.Write(t.V0);
                    w.Write(t.V1);
                    w.Write(t.V2);
                }

                WriteMaterial(w, mesh.Material);
            }
        }

        private void WritePhysicsData(BinaryWriter w, Entity ent)
        {
            w.Write((int)ent.BodyKind);
        }

        private void WriteEntityData(BinaryWriter w, Entity ent)
        {
            WriteNodeData(w, ent);
            WriteMeshData(w, ent);
            WritePhysicsData(w, ent);
            w.Write((int)ent.EntityType);
        }

        //private void WriteSkeletalData(BinaryWriter w,SkeletalEntity actor)
        //{
        //     WriteEntityData(w, actor);
        //   }
        private void WriteLightData(BinaryWriter w, Light light)
        {
            WriteNodeData(w, light);
            WriteVec3(w, light.Diffuse);
            WriteVec3(w, light.Specular);
            w.Write(light.Range);
            w.Write(light.CastShadows);
            w.Write(light.InnerCone);
            w.Write(light.OuterCone);
            w.Write(light.VolumetricShafts);
        }

        private void WriteSpawnData(BinaryWriter w, SpawnPoint spawn)
        {
            WriteNodeData(w, (Node)spawn);
            w.Write((int)spawn.Index);
            w.Write(spawn.Type);
        }

        private void WriteNode(BinaryWriter w, Node node)
        {
            if (node is SpawnPoint)
            {
                w.Write((int)3);
                WriteSpawnData(w, (SpawnPoint)node);
            }
            else
            if (node is Light)
            {
                w.Write((int)2);
                //WriteNodeData(w, node);
                WriteLightData(w, (Light)node);
            }
            else if (node is Entity)
            {
                w.Write((int)1);
                WriteEntityData(w, node as Entity);
            }
            else if (node is Node)
            {
                w.Write(0);
                WriteNodeData(w, node);
            }

            w.Write((int)node.Nodes.Count);

            foreach (var sub in node.Nodes)
            {
                WriteNode(w, sub);
            }
        }

        public void AddNode(Node node)
        {
            Root.AddNode(node);
            if (node is SpawnPoint)
            {
                Spawns.Add(node as SpawnPoint);
            }
        }

        /// <summary>
        /// Begins the physics simulation, including all nodes contained.
        /// </summary>
        public void BeginPhysics()
        {
            if (!PhysicsBegan)
            {
                Root.BeginPhysics();
                PhysicsBegan = true;
            }
        }

        /// <summary>
        /// Updates the node's position based on the physics scene.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdatePhysics()
        {
            Root.UpdatePhysics();
        }

        /// <summary>
        /// Updates the scene. call once per frame.
        /// </summary>
        ///  [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update()
        {
            if (States.Count > 0)
            {
                States.Peek().Update();
            }
            foreach(var ev in CurrentEvents)
            {
                ev.Update();
            }
            UpdatePhysics();
            MainCamera.Update();
            Root.Update();
            Root.UpdateStates();
        }

        private void NodeKeyDown(Node node, KeyID key)
        {
            node.OnKeyDown(key);
            foreach (var sub in node.Nodes)
            {
                NodeKeyDown(sub, key);
            }
        }

        private void NodeKeyUp(Node node, KeyID key)
        {
            node.OnKeyUp(key);
            foreach (var sub in node.Nodes)
            {
                NodeKeyUp(sub, key);
            }
        }

        private void NodeKeyPressed(Node node, KeyID key)
        {
            node.OnKeyPressed(key);
            foreach (var sub in node.Nodes)
            {
                NodeKeyPressed(sub, key);
            }
        }

        private void NodeMouseMove(Node node, float x, float y)
        {
            node.OnMuseMove(x, y);
            foreach (var sub in node.Nodes)
            {
                NodeMouseMove(sub, x, y);
            }
        }

        public virtual void OnKeyPressed(KeyID key)
        {
            NodeKeyPressed(Root, key);
        }

        public virtual void OnKeyDown(KeyID key)
        {
            NodeKeyDown(Root, key);
        }

        public virtual void OnKeyUp(KeyID key)
        {
            NodeKeyUp(Root, key);
        }

        public virtual void OnMouseMove(float x, float y)
        {
            NodeMouseMove(Root, x, y);
        }

        /// <summary>
        /// Returns a full list of every mesh in the scene.
        /// </summary>
        /// <returns></returns>
        public List<Vivid.Meshes.Mesh> GetMeshes()
        {
            List<Vivid.Meshes.Mesh> meshes = new List<Vivid.Meshes.Mesh>();
            AddMeshes(meshes, Root);
            return meshes;
        }

        public void AddMeshes(List<Vivid.Meshes.Mesh> list, Node node)
        {
            if (node is Entity)
            {
                var ent = node as Entity;
                foreach (var mesh in ent.Meshes)
                {
                    list.Add(mesh);
                }
            }

            foreach (var sub_node in node.Nodes)
            {
                AddMeshes(list, sub_node);
            }
        }

        /// <summary>
        /// Performs a raycast against a single entity and returns the result.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public RaycastResult MousePickNode(int x, int y, Entity entity)
        {
            Ray ray = GetMousePickRay(x, y);

            RaycastResult res = new RaycastResult();
            res = Raycast(ray, entity.Meshes);
            if (res == null)
            {
                return new RaycastResult();
            }
            return res;
        }

        /// <summary>
        /// Casts a ray against the whole scene from the mouse position provided.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public RaycastResult MousePick(int x, int y)
        {
            Ray ray = GetMousePickRay(x, y);

            RaycastResult res = new RaycastResult();

            var meshes = GetMeshes();

            res = Raycast(ray, meshes);

            return res;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Ray GetMousePickRay(int x, int y)
        {
            float w = App.VividApp.FrameWidth;
            float h = App.VividApp.FrameHeight;

            //     y =(int)h - y;

            float mx = -1 + (float)(x) / (float)(w) * 2;
            float my = 1 - (float)(y) / (float)(h) * 2;

            Vector3 origin = new Vector3(mx, my, 0);
            Vector3 dest = new Vector3(mx, my, 1.0f);

            Matrix4 viewProj = RenderGlobals.CurrentCamera.WorldMatrix * RenderGlobals.CurrentCamera.Projection;

            Matrix4 inverseProj = viewProj.Inverted();

            Vector3 ray_origin = Vector3.TransformPerspective(origin, inverseProj);
            Vector3 ray_end = Vector3.TransformPerspective(dest, inverseProj);

            Vector3 ray_dir = ray_end - ray_origin;
            ray_dir = ray_dir.Normalized();

            Ray ray = new Ray();

            ray.Pos = RenderGlobals.CurrentCamera.Position;
            ray.Dir = ray_dir;
            return ray;
        }

        private const float EPSILON = 0.0000001f;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RaycastResult RayToTri(Ray ray, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            Vector3 edge1, edge2, h, s, q;
            float a, f, u, v;
            RaycastResult res = new RaycastResult();

            edge1 = v1 - v0;// vertex1 - vertex0;
            edge2 = v2 - v0;
            h = Vector3.Cross(ray.Dir, edge2);


            a = Vector3.Dot(edge1, h);
            if (a > -EPSILON && a < EPSILON)
                return res;    // This ray is parallel to this triangle.
            f = 1.0f / a;
            s = ray.Pos - v0;
            u = f * Vector3.Dot(s, h);
            if (u < 0.0f || u > 1.0f)
                return res;
            q = Vector3.Cross(s, edge1);
            v = f * Vector3.Dot(ray.Dir, q);
            if (v < 0.0f || u + v > 1.0f)
                return res;
            // At this stage we can compute t to find out where the intersection point is on the line.
            float t = f * Vector3.Dot(edge2, q);

            if (t > EPSILON) // ray intersection
            {
                //outIntersectionPoint = rayOrigin + rayVector * t;
                res.Hit = true;
                res.Point = ray.Pos + ray.Dir * t;

                return res;
            }
            else // This means that there is a line intersection but not a ray intersection.
                return res;

            res.Hit = false;
            return res;

            return res;
        }

        public static bool RayToBounds(Ray ray, BoundingBox box)
        {
            float tmin = (box.Min.X - ray.Pos.X) / ray.Dir.X;
            float tmax = (box.Max.X - ray.Pos.X) / ray.Dir.X;

            if (tmin > tmax)
            {
                float temp = tmin;
                tmin = tmax;
                tmax = temp;
            }

            float tymin = (box.Min.Y - ray.Pos.Y) / ray.Dir.Y;
            float tymax = (box.Max.Y - ray.Pos.Y) / ray.Dir.Y;

            if (tymin > tymax)
            {
                float temp = tymin;
                tymin = tymax;
                tymax = temp;
            }

            if ((tmin > tymax) || (tymin > tmax))
                return false;

            if (tymin > tmin)
                tmin = tymin;

            if (tymax < tmax)
                tmax = tymax;

            float tzmin = (box.Min.Z - ray.Pos.Z) / ray.Dir.Z;
            float tzmax = (box.Max.Z - ray.Pos.Z) / ray.Dir.Z;

            if (tzmin > tzmax)
            {
                float temp = tzmin;
                tzmin = tzmax;
                tzmax = temp;
            }

            if ((tmin > tzmax) || (tzmin > tmax))
                return false;

            if (tzmin > tmin)
                tmin = tzmin;

            if (tzmax < tmax)
                tmax = tzmax;

            return true;
        }

        static object triLock = new object();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RaycastResult Raycast(Ray ray, Vivid.Meshes.Mesh mesh)
        {
            if (!RayToBounds(ray,mesh.BB))
            {
                return new RaycastResult();
            }

            RaycastResult res = new RaycastResult();
            float close = 999999;
            RaycastResult closeres = null;

            if (mesh.Owner == null) return res;
            var mat = mesh.Owner.WorldMatrix;

            if (mesh.TFPositions == null)
            {
                mesh.TFPositions = new Vector3[mesh.Triangles.Count * 3];
                int vc = 0;
                foreach(var tri in mesh.Triangles)
                {
                    mesh.TFPositions[vc++] = Vector3.TransformPosition(mesh.Vertices[tri.V0].Position, mat);
                    mesh.TFPositions[vc++] = Vector3.TransformPosition(mesh.Vertices[tri.V2].Position, mat);
                    mesh.TFPositions[vc++] = Vector3.TransformPosition(mesh.Vertices[tri.V1].Position, mat);
                }
            }

            int tc = mesh.Triangles.Count;
            //for (int i = 0; i < mesh.Triangles.Count; i++)
            Parallel.For(0,tc, i =>
            {

              

                RaycastResult r1 = RayToTri(ray, mesh.TFPositions[i * 3], mesh.TFPositions[i * 3 + 1], mesh.TFPositions[i*3+2]);
               
                if (r1.Hit)
                {
                    r1.Node = mesh.Owner;
                    r1.Entity = mesh.Owner as Entity;
                    lock (triLock)
                    {
                        if (closeres == null)
                        {
                            closeres = r1;
                            close = (ray.Pos - r1.Point).LengthSquared;
                        }
                        else
                        {
                            float dist = (ray.Pos - r1.Point).LengthSquared;
                            if (dist < close)
                            {
                                close = dist;
                                closeres = r1;
                            }
                        }
                    }

                    //int a = 5;
                }
            });

            return closeres;
        }


        public RaycastResult Raycast(Ray ray, Node ignore = null)
        {

            var meshes = GetMeshes();



            if (ignore != null)
            {
                List<Vivid.Meshes.Mesh> ml = new List<Meshes.Mesh>();
                foreach (var mesh in meshes)
                {
                    if(mesh.Owner != ignore)
                    {
                        ml.Add(mesh);
                    }
                }
                meshes = ml;
            }

            return Raycast(ray, meshes);

          

        }

        public void Start()
        {
            StartNode(Root);
        }

        public void StartNode(Node node)
        {
            node.Start();
            foreach(var sub in node.Nodes)
            {
                sub.Start();
            }
        }

        static object lockClose = new object();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RaycastResult Raycast(Ray ray, List<Vivid.Meshes.Mesh> meshes)
        {
            //var meshes = GetMeshes();

            RaycastResult close = null;
            float close_dist = 9999;
            RaycastResult res = new RaycastResult();

            Parallel.ForEach(meshes, mesh=>{

                RaycastResult r1 = Raycast(ray, mesh);
                if (r1 != null)
                {
                 
                    if (r1.Hit)
                    {
                        r1.Mesh = mesh;
                        lock (lockClose)
                        {
                            if (close == null)
                            {
                                close = r1;
                                close_dist = (ray.Pos - r1.Point).LengthSquared;
                            }
                            else
                            {
                                float dist = (ray.Pos - r1.Point).LengthSquared;
                                if (dist < close_dist)
                                {
                                    close = r1;
                                    close_dist = dist;
                                }
                            }
                        }
                    }
                }
                //  return close;
            });

            return close;
        }

        /// <summary>
        /// Renders the whole scene, including lights and shadows.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Render()
        {
            bool firstPass = true;
            Console.WriteLine("Lights:" + Lights.Count);
            foreach (var light in Lights)
            {
                Root.Render(light, MainCamera, firstPass);
                firstPass = false;
                //RenderGlobals.FirstPass = false;
            }
            Root.RenderStates();
            if (States.Count > 0)
            {
                States.Peek().Render();
            }
           foreach(var ev in CurrentEvents)
            {
                ev.Render();
            }

        }

        public void RemoveNode(Node node)
        {
            RemoveNodeIfFound(Root,node);

        }

        public void RemoveNodeIfFound(Node from,Node rem)
        {

            if(from == rem)
            {
                rem.Root.Nodes.Remove(rem);
                rem.Root = null;
                return;
            }
            foreach(var sub in from.Nodes)
            {
                RemoveNodeIfFound(sub, rem);
            }

        }

        public void RenderPositions()
        {
            Root.RenderPositions(MainCamera);
        }

        public void RenderNormals()
        {
            Root.RenderNormals(MainCamera);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RenderDepth()
        {
            //RenderGlobals.CurrentCamera = MainCamera;
            Root.RenderDepth(MainCamera);
        }

        /// <summary>
        /// Renders the shadow maps for each light. call this without ANY rendertargets's bound.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RenderShadows()
        {
            foreach (var light in Lights)
            {
                ShadowRender.mRT = light.RTC;
                ShadowRender.RenderDepth(light.Position, light.Range);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RenderSimple()
        {
            RenderGlobals.CurrentCamera = MainCamera;
            Root.RenderSimple();
        }

        /// <summary>
        /// Finds a specific spawn within the scene.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public SpawnPoint FindSpawn(string name, bool caseSensitive = false)
        {
            if (caseSensitive)
            {
                foreach (var spawn in Spawns)
                {
                    if (spawn.Name == name) return spawn;
                }
            }
            else
            {
                foreach (var spawn in Spawns)
                {
                    if (spawn.Name.ToLower() == name.ToLower())
                    {
                        return spawn;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds a specific spawn.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public SpawnPoint FindSpawn(string name, int index, bool caseSensitive = false)
        {
            if (caseSensitive)
            {
                foreach (var spawn in Spawns)
                {
                    if (spawn.Name == name && index == spawn.Index) return spawn;
                }
            }
            else
            {
                foreach (var spawn in Spawns)
                {
                    if (spawn.Name.ToLower() == name.ToLower() && index == spawn.Index)
                    {
                        return spawn;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds a specific spawn by type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public SpawnPoint FindSpawnByType(string type, int index = -1)
        {
            foreach (var spawn in Spawns)
            {
                if (spawn.Type == type)
                {
                    if (index >= 0)
                    {
                        if (spawn.Index == index) return spawn;
                    }
                    else
                    {
                        return spawn;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Renders the list of MeshLines attached to the scene.
        /// </summary>
        public void RenderLines()
        {
            RenderGlobals.CurrentCamera = MainCamera;
            RenderGlobals.CurrentNode = null;
            foreach (var ml in MeshLines)
            {
                Matrix4 proj = RenderGlobals.CurrentCamera.Projection;
                Matrix4 view = RenderGlobals.CurrentCamera.WorldMatrix;
                Matrix4 model = Matrix4.Identity;

                ShaderModules.Shaders.MeshFX.Camera = MainCamera;
                ShaderModules.Shaders.MeshFX.Bind();

                ml.Render();

                //GemBridge.gem_MeshRenderer_RenderMeshLines(RenderGlobals.MeshRenderer, ml.MeshBuffer, proj.handle, view.handle, model.handle);
            }
        }

        /// <summary>
        /// Finds all nodes of a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetNodesOfType<T>() where T : class
        {
            List<T> nodes = new List<T>();
            void GetNodes(Node node)
            {
                if (node is T)
                {
                    nodes.Add(node as T);
                }
                foreach (var sub in node.Nodes)
                {
                    GetNodes(sub);
                }
            }
            GetNodes(Root);
            return nodes;
        }

        /// <summary>
        /// finds all nodes of a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Node> GetAllOfNodeType(string type)
        {
            List<Node> list = new List<Node>();
            void AddNodeOfType(string type, Node node)
            {
                if (type == node.NodeType)
                {
                    list.Add(node);
                }
                foreach (var sub in node.Nodes)
                {
                    AddNodeOfType(type, sub);
                }
            }
            AddNodeOfType(type, Root);
            return list;
        }
    }
}