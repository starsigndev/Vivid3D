namespace SceneEditor.Editors
{
    partial class GenerateOctree
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
            groupBox1 = new GroupBox();
            cTris = new Label();
            cVerts = new Label();
            cNode = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            label1 = new Label();
            leafVertices = new NumericUpDown();
            button1 = new Button();
            label2 = new Label();
            ocStatus = new Label();
            groupBox2 = new GroupBox();
            ocLeafs = new Label();
            ocNodes = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)leafVertices).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cTris);
            groupBox1.Controls.Add(cVerts);
            groupBox1.Controls.Add(cNode);
            groupBox1.Location = new Point(10, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(215, 120);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Scene Information";
            // 
            // cTris
            // 
            cTris.AutoSize = true;
            cTris.Location = new Point(6, 86);
            cTris.Name = "cTris";
            cTris.Size = new Size(31, 20);
            cTris.TabIndex = 2;
            cTris.Text = "Tris";
            // 
            // cVerts
            // 
            cVerts.AutoSize = true;
            cVerts.Location = new Point(6, 52);
            cVerts.Name = "cVerts";
            cVerts.Size = new Size(60, 20);
            cVerts.TabIndex = 1;
            cVerts.Text = "Vertices";
            // 
            // cNode
            // 
            cNode.AutoSize = true;
            cNode.Location = new Point(6, 23);
            cNode.Name = "cNode";
            cNode.Size = new Size(55, 20);
            cNode.TabIndex = 0;
            cNode.Text = "Nodes:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 136);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 1;
            label1.Text = "Leaf Threashold";
            // 
            // leafVertices
            // 
            leafVertices.Location = new Point(134, 134);
            leafVertices.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            leafVertices.Minimum = new decimal(new int[] { 128, 0, 0, 0 });
            leafVertices.Name = "leafVertices";
            leafVertices.Size = new Size(150, 27);
            leafVertices.TabIndex = 2;
            leafVertices.Value = new decimal(new int[] { 1024, 0, 0, 0 });
            leafVertices.ValueChanged += LeafVerts_Changed;
            // 
            // button1
            // 
            button1.Location = new Point(290, 132);
            button1.Name = "button1";
            button1.Size = new Size(267, 29);
            button1.TabIndex = 3;
            button1.Text = "Generate Octree(Can take long)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Click_GenerateOctree;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(290, 164);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 4;
            label2.Text = "Octree Status:";
            // 
            // ocStatus
            // 
            ocStatus.AutoSize = true;
            ocStatus.Location = new Point(396, 164);
            ocStatus.Name = "ocStatus";
            ocStatus.Size = new Size(95, 20);
            ocStatus.TabIndex = 5;
            ocStatus.Text = "Not yet built.";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ocLeafs);
            groupBox2.Controls.Add(ocNodes);
            groupBox2.Location = new Point(236, 8);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(211, 118);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Octree Information";
            // 
            // ocLeafs
            // 
            ocLeafs.AutoSize = true;
            ocLeafs.Location = new Point(8, 52);
            ocLeafs.Name = "ocLeafs";
            ocLeafs.Size = new Size(43, 20);
            ocLeafs.TabIndex = 1;
            ocLeafs.Text = "Leafs";
            // 
            // ocNodes
            // 
            ocNodes.AutoSize = true;
            ocNodes.Location = new Point(6, 23);
            ocNodes.Name = "ocNodes";
            ocNodes.Size = new Size(52, 20);
            ocNodes.TabIndex = 0;
            ocNodes.Text = "Nodes";
            // 
            // GenerateOctree
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(616, 266);
            Controls.Add(groupBox2);
            Controls.Add(ocStatus);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(leafVertices);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "GenerateOctree";
            Text = "Vivid - Octree Generator";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)leafVertices).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private Label cTris;
        private Label cVerts;
        private Label cNode;
        private Label label1;
        private NumericUpDown leafVertices;
        private Button button1;
        private Label label2;
        private Label ocStatus;
        private GroupBox groupBox2;
        private Label ocLeafs;
        private Label ocNodes;
    }
}