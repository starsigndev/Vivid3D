namespace Editor.Editors
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
            groupBox1.Location = new Point(12, 10);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            groupBox1.Size = new Size(269, 150);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Scene Information";
            // 
            // cTris
            // 
            cTris.AutoSize = true;
            cTris.Location = new Point(8, 108);
            cTris.Margin = new Padding(4, 0, 4, 0);
            cTris.Name = "cTris";
            cTris.Size = new Size(37, 25);
            cTris.TabIndex = 2;
            cTris.Text = "Tris";
            // 
            // cVerts
            // 
            cVerts.AutoSize = true;
            cVerts.Location = new Point(8, 65);
            cVerts.Margin = new Padding(4, 0, 4, 0);
            cVerts.Name = "cVerts";
            cVerts.Size = new Size(72, 25);
            cVerts.TabIndex = 1;
            cVerts.Text = "Vertices";
            // 
            // cNode
            // 
            cNode.AutoSize = true;
            cNode.Location = new Point(8, 29);
            cNode.Margin = new Padding(4, 0, 4, 0);
            cNode.Name = "cNode";
            cNode.Size = new Size(68, 25);
            cNode.TabIndex = 0;
            cNode.Text = "Nodes:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 170);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(136, 25);
            label1.TabIndex = 1;
            label1.Text = "Leaf Threashold";
            // 
            // leafVertices
            // 
            leafVertices.Location = new Point(168, 168);
            leafVertices.Margin = new Padding(4, 4, 4, 4);
            leafVertices.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            leafVertices.Minimum = new decimal(new int[] { 128, 0, 0, 0 });
            leafVertices.Name = "leafVertices";
            leafVertices.Size = new Size(188, 31);
            leafVertices.TabIndex = 2;
            leafVertices.Value = new decimal(new int[] { 1024, 0, 0, 0 });
            leafVertices.ValueChanged += LeafVerts_Changed;
            // 
            // button1
            // 
            button1.Location = new Point(362, 165);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(334, 36);
            button1.TabIndex = 3;
            button1.Text = "Generate Octree(Can take long)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Click_GenerateOctree;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(362, 205);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(121, 25);
            label2.TabIndex = 4;
            label2.Text = "Octree Status:";
            // 
            // ocStatus
            // 
            ocStatus.AutoSize = true;
            ocStatus.Location = new Point(495, 205);
            ocStatus.Margin = new Padding(4, 0, 4, 0);
            ocStatus.Name = "ocStatus";
            ocStatus.Size = new Size(115, 25);
            ocStatus.TabIndex = 5;
            ocStatus.Text = "Not yet built.";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ocLeafs);
            groupBox2.Controls.Add(ocNodes);
            groupBox2.Location = new Point(295, 10);
            groupBox2.Margin = new Padding(4, 4, 4, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 4, 4, 4);
            groupBox2.Size = new Size(264, 148);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Octree Information";
            // 
            // ocLeafs
            // 
            ocLeafs.AutoSize = true;
            ocLeafs.Location = new Point(10, 65);
            ocLeafs.Margin = new Padding(4, 0, 4, 0);
            ocLeafs.Name = "ocLeafs";
            ocLeafs.Size = new Size(52, 25);
            ocLeafs.TabIndex = 1;
            ocLeafs.Text = "Leafs";
            // 
            // ocNodes
            // 
            ocNodes.AutoSize = true;
            ocNodes.Location = new Point(8, 29);
            ocNodes.Margin = new Padding(4, 0, 4, 0);
            ocNodes.Name = "ocNodes";
            ocNodes.Size = new Size(64, 25);
            ocNodes.TabIndex = 0;
            ocNodes.Text = "Nodes";
            // 
            // GenerateOctree
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(770, 332);
            Controls.Add(groupBox2);
            Controls.Add(ocStatus);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(leafVertices);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 4, 4, 4);
            Name = "GenerateOctree";
            Text = "Vivid3D - Octree Generator";
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