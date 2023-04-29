namespace SceneEditor
{
    partial class SceneEditor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TreeNode treeNode2 = new TreeNode("Scene");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SceneEditor));
            menuStrip1 = new MenuStrip();
            sceneToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            openSceneToolStripMenuItem = new ToolStripMenuItem();
            saveSceneToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            pasteInstanceToolStripMenuItem = new ToolStripMenuItem();
            pasteNewToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            alignToCameraToolStripMenuItem = new ToolStripMenuItem();
            alignToObjectToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            globalPostProcessingToolStripMenuItem = new ToolStripMenuItem();
            focusOnNodeToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            import3DModelToolStripMenuItem = new ToolStripMenuItem();
            createToolStripMenuItem = new ToolStripMenuItem();
            zoneToolStripMenuItem = new ToolStripMenuItem();
            spawnPointToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            lightToolStripMenuItem = new ToolStripMenuItem();
            pointToolStripMenuItem = new ToolStripMenuItem();
            spotToolStripMenuItem = new ToolStripMenuItem();
            directionalToolStripMenuItem = new ToolStripMenuItem();
            primitiveToolStripMenuItem = new ToolStripMenuItem();
            cubeToolStripMenuItem = new ToolStripMenuItem();
            planeToolStripMenuItem = new ToolStripMenuItem();
            sphereToolStripMenuItem = new ToolStripMenuItem();
            cylinderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            postProcessingZoneToolStripMenuItem = new ToolStripMenuItem();
            otherToolStripMenuItem = new ToolStripMenuItem();
            propertyEditorToolStripMenuItem = new ToolStripMenuItem();
            pathToolsToolStripMenuItem = new ToolStripMenuItem();
            lightMapperToolStripMenuItem = new ToolStripMenuItem();
            generateOctreeToolStripMenuItem = new ToolStripMenuItem();
            debugToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            sceneTree = new TreeView();
            editOutput = new Panel();
            glControl1 = new OpenTK.WinForms.GLControl();
            statusStrip1 = new StatusStrip();
            tsMode = new ToolStripStatusLabel();
            tsSpace = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripSeparator8 = new ToolStripSeparator();
            spaceCombo = new ToolStripComboBox();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            editOutput.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ActiveCaption;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { sceneToolStripMenuItem, editToolStripMenuItem, importToolStripMenuItem, createToolStripMenuItem, otherToolStripMenuItem, debugToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(6, 3, 0, 3);
            menuStrip1.Size = new Size(968, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // sceneToolStripMenuItem
            // 
            sceneToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, toolStripSeparator1, openSceneToolStripMenuItem, saveSceneToolStripMenuItem, toolStripSeparator4, exitToolStripMenuItem });
            sceneToolStripMenuItem.Name = "sceneToolStripMenuItem";
            sceneToolStripMenuItem.Size = new Size(62, 24);
            sceneToolStripMenuItem.Text = "Scene";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(216, 26);
            newToolStripMenuItem.Text = "New...";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(213, 6);
            // 
            // openSceneToolStripMenuItem
            // 
            openSceneToolStripMenuItem.Name = "openSceneToolStripMenuItem";
            openSceneToolStripMenuItem.Size = new Size(216, 26);
            openSceneToolStripMenuItem.Text = "Open Scene";
            openSceneToolStripMenuItem.Click += Menu_OpenScene;
            // 
            // saveSceneToolStripMenuItem
            // 
            saveSceneToolStripMenuItem.Name = "saveSceneToolStripMenuItem";
            saveSceneToolStripMenuItem.Size = new Size(216, 26);
            saveSceneToolStripMenuItem.Text = "Save Scene";
            saveSceneToolStripMenuItem.Click += Menu_SaveScene;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(216, 26);
            toolStripSeparator4.Text = "Load Octree Scene";
            toolStripSeparator4.Click += Menu_LoadOctreeScene;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(216, 26);
            exitToolStripMenuItem.Text = "Save Octree Scene";
            exitToolStripMenuItem.Click += Menu_SaveOctreeScene;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cutToolStripMenuItem, copyToolStripMenuItem, toolStripSeparator2, pasteInstanceToolStripMenuItem, pasteNewToolStripMenuItem, toolStripSeparator3, alignToCameraToolStripMenuItem, alignToObjectToolStripMenuItem, toolStripSeparator7, globalPostProcessingToolStripMenuItem, focusOnNodeToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(49, 24);
            editToolStripMenuItem.Text = "Edit";
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new Size(243, 26);
            cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(243, 26);
            copyToolStripMenuItem.Text = "Copy";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(240, 6);
            // 
            // pasteInstanceToolStripMenuItem
            // 
            pasteInstanceToolStripMenuItem.Name = "pasteInstanceToolStripMenuItem";
            pasteInstanceToolStripMenuItem.Size = new Size(243, 26);
            pasteInstanceToolStripMenuItem.Text = "Paste Instance";
            // 
            // pasteNewToolStripMenuItem
            // 
            pasteNewToolStripMenuItem.Name = "pasteNewToolStripMenuItem";
            pasteNewToolStripMenuItem.Size = new Size(243, 26);
            pasteNewToolStripMenuItem.Text = "Paste New";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(240, 6);
            // 
            // alignToCameraToolStripMenuItem
            // 
            alignToCameraToolStripMenuItem.Name = "alignToCameraToolStripMenuItem";
            alignToCameraToolStripMenuItem.Size = new Size(243, 26);
            alignToCameraToolStripMenuItem.Text = "Align To Camera";
            alignToCameraToolStripMenuItem.Click += Menu_AlignToCamera;
            // 
            // alignToObjectToolStripMenuItem
            // 
            alignToObjectToolStripMenuItem.Name = "alignToObjectToolStripMenuItem";
            alignToObjectToolStripMenuItem.Size = new Size(243, 26);
            alignToObjectToolStripMenuItem.Text = "Align To Object";
            alignToObjectToolStripMenuItem.Click += Menu_AlignToObject;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(240, 6);
            // 
            // globalPostProcessingToolStripMenuItem
            // 
            globalPostProcessingToolStripMenuItem.Name = "globalPostProcessingToolStripMenuItem";
            globalPostProcessingToolStripMenuItem.Size = new Size(243, 26);
            globalPostProcessingToolStripMenuItem.Text = "Global Post-Processing";
            // 
            // focusOnNodeToolStripMenuItem
            // 
            focusOnNodeToolStripMenuItem.Name = "focusOnNodeToolStripMenuItem";
            focusOnNodeToolStripMenuItem.Size = new Size(243, 26);
            focusOnNodeToolStripMenuItem.Text = "Focus On Node";
            focusOnNodeToolStripMenuItem.Click += Menu_FocusOnNode;
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { import3DModelToolStripMenuItem });
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(68, 24);
            importToolStripMenuItem.Text = "Import";
            // 
            // import3DModelToolStripMenuItem
            // 
            import3DModelToolStripMenuItem.Name = "import3DModelToolStripMenuItem";
            import3DModelToolStripMenuItem.Size = new Size(207, 26);
            import3DModelToolStripMenuItem.Text = "Import 3D Model";
            import3DModelToolStripMenuItem.Click += Menu_ImportModel;
            // 
            // createToolStripMenuItem
            // 
            createToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zoneToolStripMenuItem, spawnPointToolStripMenuItem, toolStripSeparator5, lightToolStripMenuItem, primitiveToolStripMenuItem, toolStripSeparator6, postProcessingZoneToolStripMenuItem });
            createToolStripMenuItem.Name = "createToolStripMenuItem";
            createToolStripMenuItem.Size = new Size(66, 24);
            createToolStripMenuItem.Text = "Create";
            // 
            // zoneToolStripMenuItem
            // 
            zoneToolStripMenuItem.Name = "zoneToolStripMenuItem";
            zoneToolStripMenuItem.Size = new Size(233, 26);
            zoneToolStripMenuItem.Text = "Zone";
            // 
            // spawnPointToolStripMenuItem
            // 
            spawnPointToolStripMenuItem.Name = "spawnPointToolStripMenuItem";
            spawnPointToolStripMenuItem.Size = new Size(233, 26);
            spawnPointToolStripMenuItem.Text = "Spawn Point";
            spawnPointToolStripMenuItem.Click += Menu_CreateSpawnPoint;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(230, 6);
            // 
            // lightToolStripMenuItem
            // 
            lightToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pointToolStripMenuItem, spotToolStripMenuItem, directionalToolStripMenuItem });
            lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            lightToolStripMenuItem.Size = new Size(233, 26);
            lightToolStripMenuItem.Text = "Light";
            // 
            // pointToolStripMenuItem
            // 
            pointToolStripMenuItem.Name = "pointToolStripMenuItem";
            pointToolStripMenuItem.Size = new Size(165, 26);
            pointToolStripMenuItem.Text = "Point";
            pointToolStripMenuItem.Click += Menu_CreatePointLight;
            // 
            // spotToolStripMenuItem
            // 
            spotToolStripMenuItem.Name = "spotToolStripMenuItem";
            spotToolStripMenuItem.Size = new Size(165, 26);
            spotToolStripMenuItem.Text = "Spot";
            spotToolStripMenuItem.Click += Menu_CreateSpotLight;
            // 
            // directionalToolStripMenuItem
            // 
            directionalToolStripMenuItem.Name = "directionalToolStripMenuItem";
            directionalToolStripMenuItem.Size = new Size(165, 26);
            directionalToolStripMenuItem.Text = "Directional";
            directionalToolStripMenuItem.Click += Menu_CreateDirLight;
            // 
            // primitiveToolStripMenuItem
            // 
            primitiveToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cubeToolStripMenuItem, planeToolStripMenuItem, sphereToolStripMenuItem, cylinderToolStripMenuItem });
            primitiveToolStripMenuItem.Name = "primitiveToolStripMenuItem";
            primitiveToolStripMenuItem.Size = new Size(233, 26);
            primitiveToolStripMenuItem.Text = "Primitive";
            // 
            // cubeToolStripMenuItem
            // 
            cubeToolStripMenuItem.Name = "cubeToolStripMenuItem";
            cubeToolStripMenuItem.Size = new Size(146, 26);
            cubeToolStripMenuItem.Text = "Cube";
            cubeToolStripMenuItem.Click += Menu_CreateCube;
            // 
            // planeToolStripMenuItem
            // 
            planeToolStripMenuItem.Name = "planeToolStripMenuItem";
            planeToolStripMenuItem.Size = new Size(146, 26);
            planeToolStripMenuItem.Text = "Plane";
            planeToolStripMenuItem.Click += Menu_CreatePlane;
            // 
            // sphereToolStripMenuItem
            // 
            sphereToolStripMenuItem.Name = "sphereToolStripMenuItem";
            sphereToolStripMenuItem.Size = new Size(146, 26);
            sphereToolStripMenuItem.Text = "Sphere";
            sphereToolStripMenuItem.Click += Menu_CreateSphere;
            // 
            // cylinderToolStripMenuItem
            // 
            cylinderToolStripMenuItem.Name = "cylinderToolStripMenuItem";
            cylinderToolStripMenuItem.Size = new Size(146, 26);
            cylinderToolStripMenuItem.Text = "Cylinder";
            cylinderToolStripMenuItem.Click += Menu_CreateCylinder;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(230, 6);
            // 
            // postProcessingZoneToolStripMenuItem
            // 
            postProcessingZoneToolStripMenuItem.Name = "postProcessingZoneToolStripMenuItem";
            postProcessingZoneToolStripMenuItem.Size = new Size(233, 26);
            postProcessingZoneToolStripMenuItem.Text = "Post-Processing Zone";
            // 
            // otherToolStripMenuItem
            // 
            otherToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { propertyEditorToolStripMenuItem, pathToolsToolStripMenuItem, lightMapperToolStripMenuItem, generateOctreeToolStripMenuItem });
            otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            otherToolStripMenuItem.Size = new Size(58, 24);
            otherToolStripMenuItem.Text = "Tools";
            // 
            // propertyEditorToolStripMenuItem
            // 
            propertyEditorToolStripMenuItem.Name = "propertyEditorToolStripMenuItem";
            propertyEditorToolStripMenuItem.Size = new Size(200, 26);
            propertyEditorToolStripMenuItem.Text = "Property Editor";
            propertyEditorToolStripMenuItem.Click += Menu_OpenPropertyEditor;
            // 
            // pathToolsToolStripMenuItem
            // 
            pathToolsToolStripMenuItem.Name = "pathToolsToolStripMenuItem";
            pathToolsToolStripMenuItem.Size = new Size(200, 26);
            pathToolsToolStripMenuItem.Text = "Path Tools";
            // 
            // lightMapperToolStripMenuItem
            // 
            lightMapperToolStripMenuItem.Name = "lightMapperToolStripMenuItem";
            lightMapperToolStripMenuItem.Size = new Size(200, 26);
            lightMapperToolStripMenuItem.Text = "Light Mapper";
            // 
            // generateOctreeToolStripMenuItem
            // 
            generateOctreeToolStripMenuItem.Name = "generateOctreeToolStripMenuItem";
            generateOctreeToolStripMenuItem.Size = new Size(200, 26);
            generateOctreeToolStripMenuItem.Text = "Generate Octree";
            generateOctreeToolStripMenuItem.Click += Menu_GenerateOctree;
            // 
            // debugToolStripMenuItem
            // 
            debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            debugToolStripMenuItem.Size = new Size(68, 24);
            debugToolStripMenuItem.Text = "Debug";
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 30);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(sceneTree);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(editOutput);
            splitContainer1.Size = new Size(968, 527);
            splitContainer1.SplitterDistance = 224;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 1;
            // 
            // sceneTree
            // 
            sceneTree.BackColor = SystemColors.InactiveCaption;
            sceneTree.Dock = DockStyle.Fill;
            sceneTree.Location = new Point(0, 0);
            sceneTree.Name = "sceneTree";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Scene";
            sceneTree.Nodes.AddRange(new TreeNode[] { treeNode2 });
            sceneTree.Size = new Size(220, 523);
            sceneTree.TabIndex = 0;
            sceneTree.AfterSelect += SceneTree_AfterSelect;
            sceneTree.DoubleClick += SceneTree_DoubleClick;
            // 
            // editOutput
            // 
            editOutput.Controls.Add(glControl1);
            editOutput.Controls.Add(statusStrip1);
            editOutput.Controls.Add(toolStrip1);
            editOutput.Dock = DockStyle.Fill;
            editOutput.Location = new Point(0, 0);
            editOutput.Name = "editOutput";
            editOutput.Size = new Size(735, 523);
            editOutput.TabIndex = 0;
            editOutput.SizeChanged += Output_Resized;
            editOutput.MouseDown += editOutput_MouseDown;
            editOutput.MouseMove += editOutput_MouseMove;
            editOutput.MouseUp += editOutput_MouseUp;
            // 
            // glControl1
            // 
            glControl1.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl1.APIVersion = new Version(4, 1, 0, 0);
            glControl1.Dock = DockStyle.Fill;
            glControl1.Flags = OpenTK.Windowing.Common.ContextFlags.Debug;
            glControl1.IsEventDriven = true;
            glControl1.Location = new Point(0, 28);
            glControl1.Margin = new Padding(3, 4, 3, 4);
            glControl1.Name = "glControl1";
            glControl1.Profile = OpenTK.Windowing.Common.ContextProfile.Compatability;
            glControl1.Size = new Size(735, 469);
            glControl1.TabIndex = 2;
            glControl1.Text = "glControl1";
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.ActiveCaption;
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsMode, tsSpace });
            statusStrip1.Location = new Point(0, 497);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(735, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsMode
            // 
            tsMode.Name = "tsMode";
            tsMode.Size = new Size(114, 20);
            tsMode.Text = "Mode: Translate";
            // 
            // tsSpace
            // 
            tsSpace.Name = "tsSpace";
            tsSpace.Size = new Size(91, 20);
            tsSpace.Text = "Space: Local";
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = SystemColors.ActiveCaption;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripSeparator8, spaceCombo });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(735, 28);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(29, 25);
            toolStripButton1.Text = "toolStripButton1";
            toolStripButton1.Click += Click_SetTranslate;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(29, 25);
            toolStripButton2.Text = "toolStripButton2";
            toolStripButton2.Click += Click_SetRotate;
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(29, 25);
            toolStripButton3.Text = "toolStripButton3";
            toolStripButton3.Click += Click_SetScale;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(6, 28);
            // 
            // spaceCombo
            // 
            spaceCombo.Name = "spaceCombo";
            spaceCombo.Size = new Size(121, 28);
            spaceCombo.SelectedIndexChanged += spaceCombo_SelectedIndexChanged;
            spaceCombo.Click += spaceCombo_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // SceneEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(968, 557);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "SceneEditor";
            Text = "Gemini Scene Editor (c)StarSign Games 2023";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            editOutput.ResumeLayout(false);
            editOutput.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem sceneToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem openSceneToolStripMenuItem;
        private ToolStripMenuItem saveSceneToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem pasteInstanceToolStripMenuItem;
        private ToolStripMenuItem pasteNewToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem alignToCameraToolStripMenuItem;
        private ToolStripMenuItem alignToObjectToolStripMenuItem;
        private SplitContainer splitContainer1;
        private Panel editOutput;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem import3DModelToolStripMenuItem;
        private ToolStripMenuItem createToolStripMenuItem;
        private ToolStripMenuItem zoneToolStripMenuItem;
        private ToolStripMenuItem spawnPointToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem lightToolStripMenuItem;
        private ToolStripMenuItem pointToolStripMenuItem;
        private ToolStripMenuItem spotToolStripMenuItem;
        private ToolStripMenuItem directionalToolStripMenuItem;
        private ToolStripMenuItem primitiveToolStripMenuItem;
        private ToolStripMenuItem cubeToolStripMenuItem;
        private ToolStripMenuItem planeToolStripMenuItem;
        private ToolStripMenuItem sphereToolStripMenuItem;
        private ToolStripMenuItem cylinderToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem globalPostProcessingToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem postProcessingZoneToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripComboBox spaceCombo;
        private TreeView sceneTree;
        private ToolStripMenuItem otherToolStripMenuItem;
        private ToolStripMenuItem propertyEditorToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsMode;
        private ToolStripStatusLabel tsSpace;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem pathToolsToolStripMenuItem;
        private ToolStripMenuItem focusOnNodeToolStripMenuItem;
        private ToolStripMenuItem lightMapperToolStripMenuItem;
        private ToolStripMenuItem toolStripSeparator4;
        private ToolStripMenuItem generateOctreeToolStripMenuItem;
        private ToolStripMenuItem debugToolStripMenuItem;
        private OpenTK.WinForms.GLControl glControl1;
    }
}