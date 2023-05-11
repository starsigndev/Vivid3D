namespace Editor.Editors
{
    partial class ModelEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            modelToolStripMenuItem = new ToolStripMenuItem();
            commitChangesToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            saveModelToolStripMenuItem = new ToolStripMenuItem();
            loadModelToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            glControl1 = new OpenTK.WinForms.GLControl();
            panel1 = new Panel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { modelToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // modelToolStripMenuItem
            // 
            modelToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { commitChangesToolStripMenuItem, toolStripSeparator1, saveModelToolStripMenuItem, loadModelToolStripMenuItem });
            modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            modelToolStripMenuItem.Size = new Size(66, 24);
            modelToolStripMenuItem.Text = "Model";
            // 
            // commitChangesToolStripMenuItem
            // 
            commitChangesToolStripMenuItem.Name = "commitChangesToolStripMenuItem";
            commitChangesToolStripMenuItem.Size = new Size(205, 26);
            commitChangesToolStripMenuItem.Text = "Commit Changes";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(202, 6);
            // 
            // saveModelToolStripMenuItem
            // 
            saveModelToolStripMenuItem.Name = "saveModelToolStripMenuItem";
            saveModelToolStripMenuItem.Size = new Size(205, 26);
            saveModelToolStripMenuItem.Text = "Load Model";
            // 
            // loadModelToolStripMenuItem
            // 
            loadModelToolStripMenuItem.Name = "loadModelToolStripMenuItem";
            loadModelToolStripMenuItem.Size = new Size(205, 26);
            loadModelToolStripMenuItem.Text = "Save Model";
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 28);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(glControl1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Size = new Size(800, 422);
            splitContainer1.SplitterDistance = 554;
            splitContainer1.TabIndex = 1;
            // 
            // glControl1
            // 
            glControl1.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl1.APIVersion = new Version(3, 3, 0, 0);
            glControl1.Dock = DockStyle.Fill;
            glControl1.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl1.IsEventDriven = true;
            glControl1.Location = new Point(0, 0);
            glControl1.Name = "glControl1";
            glControl1.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            glControl1.Size = new Size(550, 418);
            glControl1.TabIndex = 0;
            glControl1.Text = "glControl1";
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(238, 418);
            panel1.TabIndex = 0;
            // 
            // ModelEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "ModelEditor";
            Text = "Vivid3D - Model Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem modelToolStripMenuItem;
        private ToolStripMenuItem commitChangesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem saveModelToolStripMenuItem;
        private ToolStripMenuItem loadModelToolStripMenuItem;
        private SplitContainer splitContainer1;
        private OpenTK.WinForms.GLControl glControl1;
        private Panel panel1;
    }
}