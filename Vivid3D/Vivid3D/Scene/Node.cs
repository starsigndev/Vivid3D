using OpenTK.Mathematics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Vivid.Audio;
using Vivid.Renderers;
using Vivid.Resource;

namespace Vivid.Scene
{
    //This is a C# class named Node that represents a node in a scene graph.
    //A scene graph is a data structure used in computer graphics to represent the hierarchy of objects in a scene
    // where each node in the graph represents an object or a group of objects.
    // The Node class contains properties and methods for managing the position, rotation, scale, and parent-child relationship of a node in the scene graph.
    // The class is part of the Vivid namespace, which provides functionality for 3D graphics and audio rendering.

    public class Node : GemResource
    {
        /// <summary>
        /// If enabled is set to false, the node and it's children are not a part of the overall
        /// scene update's and render;
        /// </summary>
        public bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// This is a property of the Node class that represents the name of the node. It has both a getter and a setter,
        /// which allows you to get or set the value of the name property of the Node object.
        /// The value of the property is of type string, and it is used to give a meaningful name to the node.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        public float DrawnX
        {
            get;
            set;
        }

        public float DrawnY
        {
            get;
            set;
        }

        /// <summary>
        /// This code defines a public property called "Scale" of type Vec3.
        /// This property allows the user to access and modify the scaling values of an object in a 3D space.
        /// </summary>
        public Vector3 Scale
        {
            get
            {
                return _Scale;
            }
            set
            {
                _Scale = value;
                _ScaleMat = Matrix4.CreateScale(value);
            }
        }

        private Vector3 _Scale = new Vector3(1, 1, 1);
        private Matrix4 _ScaleMat = Matrix4.Identity;

        /// <summary>
        /// This is the local position, which will be relative to the parent node in actual use.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return _LocalPos;
            }
            set
            {
                _LocalPos = value;
                _PosMatrix = Matrix4.CreateTranslation(_LocalPos);
            }
        }

        private Matrix4 _PosMatrix = Matrix4.Identity;
        private Vector3 _LocalPos = new Vector3(0, 0, 0);

        /// <summary>
        /// This is generated via the world matrix, it is the overall world position of a node including
        /// it's parent transforms.
        /// </summary>
        public Vector3 WorldPosition
        {
            get;

            set;
        }

        /// <summary>
        /// The local rotation of the node.
        /// </summary>
        public Matrix4 Rotation
        {
            get;
            set;
        }

        //protected Matrix4 CachedWorld = null;

        /// <summary>
        /// The world matrix is the final matrix used to render a entity.
        /// This is the world position, rotation and scale, which is the composite of
        /// the node and all it's parent nodes
        /// </summary>
        
        public virtual Matrix4 WorldMatrix
        {
            get
            {
                Matrix4 root_mat = Matrix4.Identity;

                if (Root != null)
                {
                    root_mat = Root.WorldMatrix;
                }

                return Matrix4.CreateScale(Scale)* Rotation * Matrix4.CreateTranslation(Position) * root_mat;// * scale_mat;

                // }

                //return final_mat * root_mat;
            }
        }

        /// <summary>
        /// Unique Identifier
        /// </summary>
        public long UID
        {
            get;
            set;
        }

        /// <summary>
        /// Nodes are the child nodes of this node.
        /// </summary>
        public List<Node> Nodes
        {
            get;
            set;
        }

        /// <summary>
        /// The parent node.
        /// </summary>
        public Node Root
        {
            get;
            set;
        }

        public List<Sound> HitSounds
        {
            get;
            set;
        }

        /// <summary>
        /// The bounding box is a tight fit of min/max 3D vectors.
        /// </summary>
        public virtual Vivid.Scene.BoundingBox Bounds
        {
            get;
            set;
        }

        /// <summary>
        /// A user generated/used sub-type for any type of node.
        /// </summary>
        public string NodeType
        {
            get;
            set;
        }

        public Stack<NodeState> States
        {
            get;
            set;
        }

        public void PushState(NodeState state)
        {

            if (States.Count > 0)
            {
                var state1 = States.Peek();
                state1.Stop();
            }
            States.Push(state);
            state.Node = this;
            state.Start();

        }

        public void PopState()
        {

            if (States.Count > 0)
            {
                var state = States.Pop();
                state.Stop();
            }


        }

        /// <summary>
        /// This will create a basic node, with all internals reset to zero/identity.
        /// </summary>
        public Node()
        {
            Name = "Node001";
            Position = new Vector3();
            WorldPosition = new Vector3();
            Rotation = Matrix4.Identity;
            Nodes = new List<Node>();
            Root = null;
            HitSounds = new List<Sound>();
            Enabled = true;
            Scale = Vector3.One;
            States = new Stack<NodeState>();
            NodeType = "";
            InitNode();
            _PosMatrix = Matrix4.CreateTranslation(0, 0, 0);
            _ScaleMat = Matrix4.CreateScale(1, 1, 1);
        }

        public virtual void Start()
        {

        }

        public void UpdateStates()
        {

            if (States.Count > 0)
            {
                var state = States.Peek();
                state.Update();
            }

            foreach(var node in Nodes)
            {
                node.UpdateStates();
            }

        }

        public void RenderStates()
        {

            if (States.Count > 0)
            {
                var state = States.Peek();
                state.Render();
            }

            foreach(var node in Nodes)
            {
                node.RenderStates();
            }

        }

        

        /// <summary>
        /// Moves the node along it's current rotation. so 0,0,1 would be straight ahead, etc.
        /// </summary>
        /// <param name="x">X Units</param>
        /// <param name="y">Y Units</param>
        /// <param name="z">Z Units</param>
        public void Move(float x, float y, float z)
        {
            Vector3 mv = TransformVector(new Vector3(x, y, z));
            Position = Position + mv;
        }

        /// <summary>
        /// Adds a node as a child of this node, and sets it's parent
        /// as this node.
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(Node node)
        {
            Nodes.Add(node);
            node.Root = this;
        }

        /// <summary>
        /// Sets a node local rotation using euclidian coordinates.
        /// </summary>
        /// <param name="pitch"></param>
        /// <param name="yaw"></param>
        /// <param name="roll"></param>
        public void SetRotation(float pitch, float yaw, float roll)
        {
            pitch = MathHelper.DegreesToRadians(pitch);
            yaw = MathHelper.DegreesToRadians(yaw);
            roll = MathHelper.DegreesToRadians(roll);

            //pitch = MathHelp.Degrees2Rad(pitch);
            //yaw = MathHelp.Degrees2Rad(yaw);
            //roll =  MathHelp.Degrees2Rad(roll);

            Matrix4 p_mat = Matrix4.CreateRotationX(pitch);
            Matrix4 y_mat = Matrix4.CreateRotationY(yaw);
            Matrix4 r_mat = Matrix4.CreateRotationZ(roll);

            Rotation = p_mat * y_mat * r_mat;
        }

        /// <summary>
        /// Aligns this node to another, copying it's positon and rotation.
        /// </summary>
        /// <param name="node">The node to align to.</param>
        public void AlignToNode(Node node)
        {
            Position = node.Position;
            if (this is Camera)
            {
                Rotation = node.Rotation.Inverted();
            }
            else
            {
                Rotation = node.Rotation;
            }
        }

        public virtual void UpdatePX()
        {
        }

        /// <summary>
        /// Turns the physics body by the given amount.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        public virtual void TurnBody(float p, float y, float r)
        {
        }

        /// <summary>
        /// Moves the physics body by the given amount.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public virtual void MoveBody(float x, float y, float z)
        {
        }

        /// <summary>
        /// Returns true if the node is considered to be on-ground. This usually relies on
        /// the node being a Entity with physics enabled.
        /// </summary>
        /// <returns></returns>
        public virtual bool BodyOnGround()
        {
            return true;
        }

        /// <summary>
        /// Override this method so your node can respond to key presses.
        /// In this, this method will be called every frame the key is pressed.
        /// </summary>
        /// <param name="key"></param>
        public virtual void OnKeyPressed(KeyID key)
        {
        }

        /// <summary>
        /// Override this method so your node can respond to key presses.
        /// In this, this method will be called just once, when a key is first pushed down.
        /// </summary>
        /// <param name="key"></param>
        public virtual void OnKeyDown(KeyID key)
        {
        }

        /// <summary>
        /// Override this method so your node can respond to key presses.
        /// In this, this method will be called just once, when a key is first released.
        /// </summary>
        /// <param name="key"></param>
        public virtual void OnKeyUp(KeyID key)
        {
        }

        /// <summary>
        /// Override this method so your nodes can respond to the mouse moving.
        /// </summary>
        /// <param name="x">X amount in pixels.</param>
        /// <param name="y">Y amount in pixels.</param>
        public virtual void OnMuseMove(float x, float y)
        {
        }

        /// <summary>
        // when you rotate a node in local space, the rotation is applied relative to the node's current orientation.
        // In other words, the node rotates around its own axis.
        // On the other hand, when you rotate a node in global space, the rotation is applied relative to the world's
        // coordinate system. This means the node rotates around the global axis, regardless of its current orientation.

        /// </summary>
        /// <param name="pitch"></param>
        /// <param name="yaw"></param>
        /// <param name="roll"></param>
        /// <param name="local"></param>
        public void Turn(float pitch, float yaw, float roll, bool local = true)
        {
            pitch = MathHelper.DegreesToRadians(pitch);
            yaw = MathHelper.DegreesToRadians(yaw);
            roll = MathHelper.DegreesToRadians(roll);

            //pitch = MathHelp.Degrees2Rad(pitch);
            //yaw = MathHelp.Degrees2Rad(yaw);
            //roll =  MathHelp.Degrees2Rad(roll);

            Matrix4 p_mat = Matrix4.CreateRotationX(pitch);
            Matrix4 y_mat = Matrix4.CreateRotationY(yaw);
            Matrix4 r_mat = Matrix4.CreateRotationZ(roll);
            if (local)
            {
                Rotation = (y_mat * p_mat * r_mat) * Rotation;
            }
            else
            {
                Rotation = Rotation * (y_mat * p_mat * r_mat);
            }
            UpdatePX();
        }

        /// <summary>
        /// Allowes you to set the rotation matrix directly.
        /// </summary>
        /// <param name="rotation"></param>
        public void SetRotation(Matrix4 rotation)
        {
            Rotation = rotation;
            //CachedWorld = null;
        }

        /// <summary>
        /// Render simple will render a node and it's children as full bright without any lighting or normal mapping.
        /// </summary>
        public virtual void RenderSimple()
        {
            RenderGlobals.CurrentNode = this;
            foreach (var node in Nodes)
            {
                node.RenderSimple();
            }
        }

        /// <summary>
        /// This is the primary render method, it will render a node with both lighting and shadows.
        /// This should not be called directly, rather use Scene.Render() to render the whole scene.
        ///
        /// </summary>
        /// <param name="l"></param>
        /// <param name="c"></param>
        /// <param name="firstPass"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Render(Light l, Camera c, bool firstPass)
        {
            foreach (var node in Nodes)
            {
                node.Render(l, c, firstPass);
            }
        }

        public virtual void RenderPositions(Camera c)
        {
            foreach (var node in Nodes)
            {
                node.RenderPositions(c);
            }
        }

        public virtual void RenderNormals(Camera c)
        {
            foreach (var node in Nodes)
            {
                node.RenderNormals(c);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void RenderDepth(Camera c, bool ignore_child = false)
        {
            RenderGlobals.CurrentNode = this;
            foreach (var node in Nodes)
            {
                node.RenderDepth(c);
            }
        }

        public void LookAtZero(Vector3 p, Vector3 up)
        {
            Matrix4 m = Matrix4.LookAt(Vector3.Zero, p, up);
            Rotation = m;
        }

        /// <summary>
        /// This will initiate physics for this node, including the creation of it's
        /// internal physics body.
        /// See BodyKind for me information.
        /// </summary>
        public virtual void BeginPhysics()
        {
            if (!Enabled) return;
            foreach (var node in Nodes)
            {
                node.BeginPhysics();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void UpdatePhysics()
        {
            if (!Enabled) return;
            foreach (var node in Nodes)
            {
                node.UpdatePhysics();
            }
        }

        /// <summary>
        /// Transform a vector around the rotation matrix.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 TransformVector(Vector3 vector)
        {
            if (this is Camera)
            {
                //     return vector * Rotation;
            }
            return Vector3.TransformVector(vector, Rotation);

            //var result = vector * Rotation;// * new Vec4(vector, 1.0f);
            //return result;
        }

        /// <summary>
        /// Transform a vector around the node in world space.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 TransformPosition(Vector3 position)
        {
            if (this is Camera)
            {
                //var result = WorldMatrix* new Vec4(position, 1.0f);
                return Vector3.TransformPosition(position, WorldMatrix.Inverted());
            }
            else
            {
                var result = Vector3.TransformPosition(position, WorldMatrix);// * new Vec4(position, 1.0f);
                return result;
            }
        }

        public void PointAt(Vector3 point)
        {
            PointAt(point, new Vector3(0, 1, 0));
        }

        public void PointAt(Vector3 point, Vector3 up)
        {
            //!!!!!!!!!!!!!
            //Rotation = Matrix4.LookAt(vec3.Zero, -(point - LocalPosition),up).Inverse;
        }

        /// <summary>
        /// Clones a node and all of it's internal properties.
        /// </summary>
        /// <returns></returns>
        public virtual Node Clone()
        {
            var clone = new Node();
            clone.Rotation = Rotation;
            clone.Position = Position;
            clone.Root = Root;
            clone.Nodes = Nodes;
            clone.Name = Name + "_Clone";
            return clone;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update()
        {
            if (!Enabled) return;
            UpdateNode();

            if(this is SkeletalEntity)
            {
                var sk = this as SkeletalEntity;
                sk.UpdateAnimation();
            }

            foreach (var node in Nodes)
            {
                node.Update();
            }
        }

        /// <summary>
        /// Called when the simulation begins.
        /// </summary>
        public virtual void InitNode()
        {
        }

        /// <summary>
        /// Called once per frame of the simulation.
        /// </summary>
        public virtual void UpdateNode()
        {
        }

        /// <summary>
        /// Called once per render of the simulation.
        /// </summary>
        public virtual void RenderNode()
        {
        }

        private void PlayHitSound(float force)
        {
            if (HitSounds.Count == 0) return;

            if (force > 0.01f)
            {
                var sound = HitSounds[0];
                var chan = sound.Play2D();
                float bas = 0.2f;

                chan.SetPitch(0.2f + force);
                chan.SetVolume(0.2f + force);
            }
            //Console.WriteLine("Force:" + force);
        }

        public virtual void OnCollision(Vector3 pos, Vector3 force, Vector3 normal, Node other)
        {
            PlayHitSound(force.LengthSquared);
            //Console.WriteLine("Collided AT:" + pos.ToString());
        }
    }
}