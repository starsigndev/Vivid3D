using Vivid.Scene;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.UI.Forms;
using Vivid.App;
using Vivid3D.Forms;
using System.Net.Sockets;

namespace Vivid3D
{
    public enum EditorMode
    {
        Translate,Rotate,Scale
    }

    public enum SpaceMode
    {
        Local,Global,Screen,Smart
    }

    public enum PlayMode
    {
        Play,Pause,Stopped
    }

    public class Editor
    {

        public static PlayMode PlayMode = PlayMode.Stopped;
        public static string ProjectPath = "";
        public static EditorMode EditMode = EditorMode.Translate;
        public static SpaceMode SpaceMode = SpaceMode.Local;
        public static Vivid.Scene.Scene CurrentScene = null;
        public static Vivid.Scene.Camera EditCamera = null;
        public static Vivid.Scene.Camera GameCamera = null;
        public static ITreeView SceneTree = null;
        public static Node SelectedNode = null;
        public static float GizmoSpeed = 0.02f;
        public static Dictionary<object, bool> OpenMap = new Dictionary<object, bool>();
        //Gizmo
        //Gizmos
        public static Entity GizmoMove, GizmoRotate, GizmoScale;
        public static Entity CurrentGizmo;
        public static bool g_x, g_y, g_z;
        public static Node NodeBB = null;

        public static void Play()
        {
            if(PlayMode == PlayMode.Pause)
            {
                PlayMode = PlayMode.Play;
                return;
            }
            PlayMode = PlayMode.Play;
            CurrentScene.Start();
        }

        public static void Pause()
        {
            if (PlayMode != PlayMode.Play) return;
            PlayMode = PlayMode.Pause;
        }
        public static void Update()
        {
            if (PlayMode == PlayMode.Play)
            {
                CurrentScene.Update();
            }
        }
        public static void Stop()
        {
            if (PlayMode == PlayMode.Stopped) return;
            PlayMode = PlayMode.Stopped;
            CurrentScene.Stop();
            
            if (FNodeEditor.Editor.CurrentNode != null)
            {
                FNodeEditor.Editor.SetNode(FNodeEditor.Editor.CurrentNode);
            }
        } 

        public static void AddNode(Node node)
        {
            CurrentScene.AddNode(node);
            UpdateSceneGraph();
        }
        public static void SetScene(Scene scene)
        {
            var grids = Editor.CurrentScene.MeshLines;
            scene.MeshLines = grids;
            Editor.CurrentScene = scene;
            Editor.UpdateSceneGraph();
            Editor.CurrentScene.MainCamera = Editor.EditCamera;
            SelectedNode = null;
            Vivid3DApp.SetupPostProcessing();

        }
        public static void UpdateSceneGraph()
        {
            SceneTree.Root = new TreeItem();
            SceneTree.Root.Text = "Scene";
            AddNodeToTree(CurrentScene.Root,SceneTree.Root);
            int lid = 0;
            foreach(var light in CurrentScene.Lights)
            {
                lid++;
                var item = SceneTree.Root.AddItem("Light " + lid);
                item.Data = (light);
                item.Click = (item) =>
                {
                    Editor.SetSelectedNode((Node)item.Data);
                };
            }
        }

        public static void AddNodeToTree(Node node,TreeItem root)
        {
            var item = root.AddItem(node.Name);
            node.EditData = item;
            item.Data = (object)node;

            if (OpenMap.ContainsKey(node))
            {
                item.Open = OpenMap[node];
            }
            else
            {
                OpenMap.Add(node, false);
            }

            item.Click = (item) =>
            {
                Editor.SetSelectedNode((Node)item.Data);
            };

            foreach(var subnode in node.Nodes)
            {
                AddNodeToTree(subnode, item);
                
            }
        }

        public static void NewScene()
        {

            CurrentScene = new Vivid.Scene.Scene();
            EditCamera = CurrentScene.MainCamera;
            GameCamera = EditCamera;
            SelectedNode = null;
            Vivid3DApp.SetupPostProcessing();

        }

        public static RaycastResult CamPick(Vector2 pos,Entity just = null)
        {

            int _fw, _fh;
            _fw = VividApp.FrameWidth;
            _fh = VividApp.FrameHeight;
            VividApp.FrameWidth = (int)FSceneRT.RenderSize.X;
            VividApp.FrameHeight = (int)FSceneRT.RenderSize.Y;
            RaycastResult res = null;
            if (just == null)
            {
                res = CurrentScene.MousePick((int)pos.X, (int)pos.Y);
            }
            else
            {
                res = CurrentScene.MousePickNode((int)pos.X, (int)pos.Y, just);
            }

            VividApp.FrameWidth = _fw;
            VividApp.FrameHeight = _fh;

            return res;
        }

        public static void SetSelectedNode(Node node)
        {
            SelectedNode = node;
            Editor.SceneTree.SelectedItem = (TreeItem)node.EditData;
            CurrentGizmo.Position = node.Position;
            FNodeEditor.Editor.SetNode(node);
        }

        public static void Cut()
        {
            if (SelectedNode == null) return;
            NodeBB = SelectedNode;
            NodeBB.Root.Nodes.Remove(NodeBB);
            NodeBB.Root = null;
            UpdateSceneGraph();
        }

        public static void Copy()
        {
            if (SelectedNode == null) return;
            NodeBB = SelectedNode.Clone();
        }

        public static void Paste()
        {
            if (NodeBB == null) return;
            if (SelectedNode == null)
            {
                if (NodeBB != null)
                {
                    CurrentScene.AddNode(NodeBB);
                }
            }
            else
            if (SelectedNode != NodeBB)
            {
                SelectedNode.AddNode(NodeBB);
            }

            UpdateSceneGraph();

        }

        public static void CreatePointLight()
        {

            var light = new Light();

            Vector3 light_pos = EditCamera.TransformPosition(new Vector3(0, 0, -5));

            light.Position = light_pos;

            CurrentScene.Lights.Add(light);

            UpdateSceneGraph();

        }

        public static void CreateSpotLight()
        {

            var light = new Light();

            Vector3 light_pos = EditCamera.Position;

            light.Position = light_pos;

            CurrentScene.Lights.Add(light);

            UpdateSceneGraph();

            light.Type = LightType.Spot;
            light.Rotation = EditCamera.Rotation;

        }

        public static void CreateDirLight()
        {

            var light = new Light();

            Vector3 light_pos = EditCamera.Position;

            light.Position = light_pos;

            CurrentScene.Lights.Add(light);

            UpdateSceneGraph();

            light.Type = LightType.Directional;
            light.Rotation = EditCamera.Rotation;

        }

        public long GenerateUID(object data)
        {

            return 0;
        }

        public void WriteData(object data)
        {

        }

        public object ReadData(Stream source)
        {

            return null;
        }

    }
}
