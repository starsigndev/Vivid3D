
using Vivid.App;
using Vivid.Content;

using Vivid.Draw;
using Vivid.Scene;
using Vivid.Importing;
using Vivid.Maths;
using Vivid.Mesh;
using Vivid.Meshes;
using Vivid.Renderers;
using Vivid.Resource;
using Vivid.Texture;
using Editor.Editors;
using Editor.Logic;
using System.Security.Cryptography.Pkcs;
using System.Windows.Forms;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.WinForms;
using Editor.Tools;
using Vivid.PostProcesses;
using SceneEditor.Editors;
//using static Assimp.Metadata;

namespace Editor
{

    public partial class SceneEditor : Form
    {
        VividApp App;
        public enum GizMode
        {
            Translate, Rotate, Scale
        }

        public enum EditSpace
        {
            Local, Global, Screen
        }
        public static Entity GizmoTranslate, GizmoRotate, GizmoScale;
        public static Entity CurrentGizmo = null;
        public static Vivid.Scene.Node CurrentNode = null;
        //bool localMode = true;
        public static EditSpace Space = EditSpace.Local;
        public static GizMode Mode = GizMode.Translate;
        public static bool GizLockX, GizLockY, GizLockZ;
        public static SmartDraw draw;
        public static Texture2D LightIcon, CameraIcon, SpawnIcon, ZoneIcon, EntityIcon;
        bool outputActive = false;
        public static Panel Output;
        public static TreeView NodeTree;
        public static ToolStripComboBox SpaceCombo;
        public static ToolStripStatusLabel TsMode, TsSpace;
        public static OpenFileDialog OpenFile;
        public static SaveFileDialog SaveFile;
        public static SceneEditor This = null;
        public static Vector3 CamMove = new Vector3();
        public static bool MoveZ = false, MoveZBack = false, MoveXLeft = false, MoveXRight = false;
        public static bool MoveUp = false, MoveDown = false;
        public static bool MoveFaster = false;
        public static Scene EditScene;
        public static Vivid.Acceleration.Octree.ASOctree EditSceneOT = null;
        public static Mesh Grid;
        public static GLControl G_Control;
        public static MeshLines BearingLines;
        public static Camera EditCam;
        public static float PX, PY;
        public static PPOutline pp_outline;

        public static float MouseX, MouseY;
        public static float CamPitch = 0, CamYaw = 0;
        System.Windows.Forms.Timer timer;
        public static bool CamDrag = false;
        public static bool GizDrag = false;
        public static Vivid.Renderers.Renderer FinalRender = null;
        public static Dictionary<TreeNode, Node> NodeMap = new Dictionary<TreeNode, Node>();
        public SceneEditor()
        {
            GizLockX = GizLockY = GizLockZ = false;
            InitializeComponent();
            G_Control = glControl1;
            glControl1.Load += GlControl1_Load1;
            glControl1.Paint += GlControl1_Paint1;
            glControl1.MouseMove += GlControl1_MouseMove;
            glControl1.MouseDown += GlControl1_MouseDown;
            glControl1.MouseUp += GlControl1_MouseUp;
            glControl1.KeyDown += GlControl1_KeyDown;
            glControl1.KeyUp += GlControl1_KeyUp;
            //   glControl1.Paint += GlControl1_Paint;
            //     glControl1.Load += GlControl1_Load;
            This = this;

            //GemBridge.gem_InitForm(this.Handle);
            // Paint += Form1_Paint;
            NodeTree = sceneTree;
            SpaceCombo = spaceCombo;
            var primitives = new Content("content/primitives");
            // GemBridge.gem_InitForm(editOutput.Handle, editOutput.Width, editOutput.Height);
            //App = new GeminiApp(editOutput.Width, editOutput.Height);
            This = this;

            OpenFile = openFileDialog1;
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            editOutput.Paint += EditOutput_Paint;
            Output = editOutput;

            //  EditScene.AddNode(grid_node);


            spaceCombo.Items.Add("Local");
            spaceCombo.Items.Add("Global");
            spaceCombo.Items.Add("Screen");
            spaceCombo.SelectedIndex = 0;

            TsMode = tsMode;
            TsSpace = tsSpace;


            timer = new System.Windows.Forms.Timer();
            timer.Interval = 5;

            timer.Tick += Timer_Tick;
            timer.Enabled = true;
            UpdateSB();

            MouseWheel += Form1_MouseWheel;
            PX = 0;
            PY = 0;
        }

        private void GlControl1_KeyUp(object? sender, KeyEventArgs e)
        {
            Logic.KB.Up(e);
            //throw new NotImplementedException();
        }

        private void GlControl1_KeyDown(object? sender, KeyEventArgs e)
        {
            Logic.KB.Down(e);
            //throw new NotImplementedException();

        }

        private void GlControl1_MouseUp(object? sender, MouseEventArgs e)
        {
            Logic.Mouse.Up(e);
        }

        private void GlControl1_MouseDown(object? sender, MouseEventArgs e)
        {
            Logic.Mouse.Down(e);
            //throw new NotImplementedException();
            glControl1.Invalidate();
        }

        private void GlControl1_MouseMove(object? sender, MouseEventArgs e)
        {
            Logic.Mouse.Move(e);
            glControl1.Invalidate();
            //throw new NotImplementedException();

        }

        private void GlControl1_Paint1(object? sender, PaintEventArgs e)
        {
            glControl1.MakeCurrent();
            GL.ClearColor(0.12f, 0.12f, 0.12f, 1);
            OpenTK.Graphics.OpenGL.GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // draw.Begin();
            //  draw.Draw(t1, new Rect(32, 32, 128, 128), new Vivid.Maths.Color(1, 1, 1, 1));
            // draw.End();


            //throw new NotImplementedException();
            Logic.Paint.Draw(e);
            glControl1.SwapBuffers();
            return;
        }

        private void GlControl1_Load1(object? sender, EventArgs e)
        {
            EditApp.Init();
            outputActive = true;
            Output_Resized(null, null);
            BearingLines = new MeshLines();
            EditScene.MeshLines.Add(BearingLines);
            //glControl1.Invalidate();
        }

        private void GlControl1_Load(object? sender, EventArgs e)
        {
            //glControl1.MakeCurrent();


        }

        Texture2D t1;
        private void GlControl1_Paint(object? sender, PaintEventArgs e)
        {
            // glControl1.MakeCurrent();
            GL.ClearColor(0, 1, 0, 1);
            OpenTK.Graphics.OpenGL.GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            draw.Begin();
            draw.Draw(t1, new Rect(32, 32, 128, 128), new Vivid.Maths.Color(1, 1, 1, 1));
            draw.End();

            //throw new NotImplementedException();
            Logic.Paint.Draw(e);
            // glControl1.SwapBuffers();
            //  glControl1.Invalidate();
        }

        private void Form1_MouseWheel(object? sender, MouseEventArgs e)
        {

            Logic.Mouse.Wheel(e);
        }



        public void CreateGrid()
        {
            var eg = Logic.Grid.CreateGrid();
            EditScene.MeshLines.Add(eg);
        }


        public static Entity EditGrid;



        private void Timer_Tick(object? sender, EventArgs e)
        {

            Logic.Time.MainTick();
            glControl1.Invalidate();

        }


        private void Form1_KeyUp(object? sender, KeyEventArgs e)
        {
            KB.Up(e);

        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {

            KB.Down(e);

        }



        private void EditOutput_Paint(object? sender, PaintEventArgs e)
        {
            Logic.Paint.Draw(e);

        }



        private void editOutput_MouseMove(object sender, MouseEventArgs e)
        {

            Logic.Mouse.Move(e);

            editOutput.Invalidate();
        }



        private void editOutput_MouseDown(object sender, MouseEventArgs e)
        {

            Logic.Mouse.Down(e);


        }

        private void editOutput_MouseUp(object sender, MouseEventArgs e)
        {
            Logic.Mouse.Up(e);
        }

        public static void Rebuild()
        {
            This.RebuildTree();

        }
        private void Output_Resized(object sender, EventArgs e)
        {

            if (outputActive)
            {
                VividApp._FW = glControl1.Size.Width;
                VividApp._FH = glControl1.Size.Height;
                GL.Viewport(0, 0, glControl1.Size.Width, glControl1.Size.Height);
                //App = new GeminiApp(editOutput.Width, editOutput.Height);
                //GemBridge.gem_WinResize(editOutput.Size.Width, editOutput.Size.Height);
            }
        }

        private void Menu_ImportModel(object sender, EventArgs e)
        {
            Logic.Features.ImportModel();
        }

        private void Menu_SaveOctreeScene(object sender, EventArgs e)
        {
            Logic.FileIO.SaveOctreeScene();
        }

        public void Click_SetTranslate(object sender, EventArgs e)
        {

            CurrentGizmo = GizmoTranslate;
            Mode = GizMode.Translate;
            UpdateSB();
        }

        public void Click_SetRotate(object sender, EventArgs e)
        {
            CurrentGizmo = GizmoRotate;
            Mode = GizMode.Rotate;
            UpdateSB();
        }

        public void Click_SetScale(object sender, EventArgs e)
        {
            CurrentGizmo = GizmoScale;
            Mode = GizMode.Scale;
            UpdateSB();
        }

        public static void UpdateSB()
        {
            if (TsMode == null) return;

            TsMode.Text = "Mode:" + Mode.ToString();
            TsSpace.Text = "Space:" + Space.ToString();

        }



        private void Menu_CreatePointLight(object sender, EventArgs e)
        {

            Logic.Create.CreatePointLight();

            RebuildTree();


        }

        public void RebuildTree()
        {

            NodeMap.Clear();
            sceneTree.Nodes.Clear();
            AddNodeToTree(EditScene.Root, sceneTree.Nodes);

        }

        public void AddNodeToTree(Node node, TreeNodeCollection nodes)
        {

            TreeNode t_node = nodes.Add(node.Name);

            NodeMap.Add(t_node, node);


            foreach (var sub in node.Nodes)
            {

                AddNodeToTree(sub, t_node.Nodes);

            }

        }


        private void SceneTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selected = sceneTree.SelectedNode;

            if (selected != null && NodeMap.ContainsKey(selected))
            {
                var g_node = NodeMap[selected];
                CurrentNode = g_node;
            }

        }

        private void SceneTree_DoubleClick(object sender, EventArgs e)
        {
            if (sceneTree.SelectedNode == null) return;
            if (NodeMap.ContainsKey(sceneTree.SelectedNode))
            {
                var g_node = NodeMap[sceneTree.SelectedNode];
                if (g_node is Light)
                {

                    var light_edit = new LightEditor();
                    light_edit.Show();
                    light_edit.CurrentLight = g_node as Light;



                }
                if (g_node is Entity)
                {

                    var ent_edit = new EntityEditor();
                    ent_edit.Show();
                    ent_edit.CurrentEntity = g_node as Entity;

                }
                if (g_node is SpawnPoint)
                {

                    var spawn_edit = new SpawnEditor();
                    spawn_edit.Show();
                    spawn_edit.CurrentSpawn = g_node as SpawnPoint;
                    spawn_edit.FromSpawn();

                }

            }

        }

        private void Menu_CreateSpotLight(object sender, EventArgs e)
        {
            Logic.Create.CreateSpotLight();
            RebuildTree();
        }

        private void Menu_CreateDirLight(object sender, EventArgs e)
        {
            Logic.Create.CreateDirLight();
            RebuildTree();
        }

        private void Menu_SaveScene(object sender, EventArgs e)
        {
            Logic.FileIO.SaveScene();


        }

        private void Menu_OpenScene(object sender, EventArgs e)
        {
            Logic.FileIO.OpenScene();
            RebuildTree();

        }

        private void Menu_OpenPropertyEditor(object sender, EventArgs e)
        {
            PropertyEditor.EditScene = EditScene;
            PropertyEditor prop_edit = new PropertyEditor();
            prop_edit.Show();
            prop_edit.RebuildUI();


        }

        private void Menu_CreateCube(object sender, EventArgs e)
        {

            Logic.Create.CreateCube();

        }

        private void Menu_CreatePlane(object sender, EventArgs e)
        {
            Logic.Create.CreatePlane();
        }

        private void Menu_CreateSphere(object sender, EventArgs e)
        {
            Logic.Create.CreateSphere();
        }

        private void Menu_CreateCylinder(object sender, EventArgs e)
        {
            Logic.Create.CreateCylinder();
        }

        private void Menu_CreateSpawnPoint(object sender, EventArgs e)
        {
            Logic.Create.CreateSpawnPoint();

            RebuildTree();

        }

        private void Menu_AlignToCamera(object sender, EventArgs e)
        {

            Logic.Features.AlignToCamera();

        }

        private void Menu_AlignToObject(object sender, EventArgs e)
        {
            Logic.Features.AlignToObject();
        }

        private void Menu_FocusOnNode(object sender, EventArgs e)
        {
            Logic.Features.FocusOnNode();

            Invalidate();
            editOutput.Invalidate();

        }

        private void Menu_GenerateOctree(object sender, EventArgs e)
        {

            Editors.GenerateOctree gen_tree = new GenerateOctree();
            gen_tree.Scene = EditScene;
            gen_tree.Show();

        }

        private void Menu_LoadOctreeScene(object sender, EventArgs e)
        {
            EditScene.Lights.Clear();
            EditScene.Root = new Node();
            Logic.FileIO.LoadOctreeScene();
            Rebuild();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditApp.New_Scene();
        }

        private void spaceCombo_Click(object sender, EventArgs e)
        {

        }

        private void spaceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //spaceCombo.
            if (SpaceCombo == null) return;
            //GemBridge.gem_BeginFrame();
            if (SpaceCombo.SelectedIndex == 0)
            {
                //localMode = true;
                Space = EditSpace.Local;
                UpdateSB();
            }
            else if (SpaceCombo.SelectedIndex == 1)
            {

                //localMode = false;
                Space = EditSpace.Global;
                UpdateSB();
            }
            else if (SpaceCombo.SelectedIndex == 2)
            {
                Space = EditSpace.Screen;
                UpdateSB();
            }
        }

        private void textureFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextureSources ts = new TextureSources();
            ts.Show();

        }

        private void modelEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Editors.ModelEditor model_edit = new ModelEditor();
            model_edit.Show();
        }

        private void renderConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenderConfig ren_config = new RenderConfig();
            ren_config.Show();

        }

        private void Menu_SkySettings(object sender, EventArgs e)
        {
           //
        }
    }
}