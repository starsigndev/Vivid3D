namespace Editor.Editors
{
    partial class EntityEditor
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
            label1 = new Label();
            entityName = new TextBox();
            label2 = new Label();
            pxType = new ComboBox();
            groupBox1 = new GroupBox();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            nSpecG = new NumericUpDown();
            nSpecB = new NumericUpDown();
            nSpecR = new NumericUpDown();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            nDiffG = new NumericUpDown();
            nDiffB = new NumericUpDown();
            nDiffR = new NumericUpDown();
            label8 = new Label();
            label7 = new Label();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            panSpec = new Panel();
            panNormal = new Panel();
            panColor = new Panel();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            cbMesh = new ComboBox();
            openFileDialog1 = new OpenFileDialog();
            label15 = new Label();
            entType = new TextBox();
            label16 = new Label();
            cbDynamicType = new ComboBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nSpecG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nSpecB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nSpecR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nDiffG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nDiffB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nDiffR).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(59, 25);
            label1.TabIndex = 3;
            label1.Text = "Name";
            // 
            // entityName
            // 
            entityName.Location = new Point(135, 15);
            entityName.Margin = new Padding(4, 4, 4, 4);
            entityName.Name = "entityName";
            entityName.Size = new Size(382, 31);
            entityName.TabIndex = 2;
            entityName.TextChanged += entityName_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 81);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(111, 25);
            label2.TabIndex = 4;
            label2.Text = "Physics Type";
            // 
            // pxType
            // 
            pxType.FormattingEnabled = true;
            pxType.Location = new Point(135, 71);
            pxType.Margin = new Padding(4, 4, 4, 4);
            pxType.Name = "pxType";
            pxType.Size = new Size(382, 33);
            pxType.TabIndex = 5;
            pxType.SelectedIndexChanged += pxType_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(nSpecG);
            groupBox1.Controls.Add(nSpecB);
            groupBox1.Controls.Add(nSpecR);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(nDiffG);
            groupBox1.Controls.Add(nDiffB);
            groupBox1.Controls.Add(nDiffR);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(panSpec);
            groupBox1.Controls.Add(panNormal);
            groupBox1.Controls.Add(panColor);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(15, 188);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            groupBox1.Size = new Size(1020, 481);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Material";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(382, 114);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(22, 25);
            label12.TabIndex = 30;
            label12.Text = "B";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(250, 114);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(24, 25);
            label13.TabIndex = 29;
            label13.Text = "G";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(105, 114);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(23, 25);
            label14.TabIndex = 28;
            label14.Text = "R";
            // 
            // nSpecG
            // 
            nSpecG.DecimalPlaces = 2;
            nSpecG.Location = new Point(279, 111);
            nSpecG.Margin = new Padding(4, 4, 4, 4);
            nSpecG.Name = "nSpecG";
            nSpecG.Size = new Size(91, 31);
            nSpecG.TabIndex = 27;
            nSpecG.ValueChanged += nSpecG_ValueChanged;
            // 
            // nSpecB
            // 
            nSpecB.DecimalPlaces = 2;
            nSpecB.Location = new Point(412, 114);
            nSpecB.Margin = new Padding(4, 4, 4, 4);
            nSpecB.Name = "nSpecB";
            nSpecB.Size = new Size(90, 31);
            nSpecB.TabIndex = 26;
            nSpecB.ValueChanged += nSpecB_ValueChanged;
            // 
            // nSpecR
            // 
            nSpecR.DecimalPlaces = 2;
            nSpecR.Location = new Point(140, 111);
            nSpecR.Margin = new Padding(4, 4, 4, 4);
            nSpecR.Name = "nSpecR";
            nSpecR.Size = new Size(102, 31);
            nSpecR.TabIndex = 25;
            nSpecR.ValueChanged += nSpecR_ValueChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(382, 54);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(22, 25);
            label11.TabIndex = 24;
            label11.Text = "B";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(250, 54);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(24, 25);
            label10.TabIndex = 23;
            label10.Text = "G";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(105, 54);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(23, 25);
            label9.TabIndex = 22;
            label9.Text = "R";
            // 
            // nDiffG
            // 
            nDiffG.DecimalPlaces = 2;
            nDiffG.Location = new Point(279, 51);
            nDiffG.Margin = new Padding(4, 4, 4, 4);
            nDiffG.Name = "nDiffG";
            nDiffG.Size = new Size(91, 31);
            nDiffG.TabIndex = 21;
            nDiffG.ValueChanged += nDiffG_ValueChanged;
            // 
            // nDiffB
            // 
            nDiffB.DecimalPlaces = 2;
            nDiffB.Location = new Point(412, 54);
            nDiffB.Margin = new Padding(4, 4, 4, 4);
            nDiffB.Name = "nDiffB";
            nDiffB.Size = new Size(90, 31);
            nDiffB.TabIndex = 20;
            nDiffB.ValueChanged += nDiffB_ValueChanged;
            // 
            // nDiffR
            // 
            nDiffR.DecimalPlaces = 2;
            nDiffR.Location = new Point(140, 51);
            nDiffR.Margin = new Padding(4, 4, 4, 4);
            nDiffR.Name = "nDiffR";
            nDiffR.Size = new Size(102, 31);
            nDiffR.TabIndex = 19;
            nDiffR.ValueChanged += nDiffR_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(15, 114);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(79, 25);
            label8.TabIndex = 18;
            label8.Text = "Specular";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 54);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(68, 25);
            label7.TabIndex = 17;
            label7.Text = "Diffuse";
            // 
            // button3
            // 
            button3.Location = new Point(736, 354);
            button3.Margin = new Padding(4, 4, 4, 4);
            button3.Name = "button3";
            button3.Size = new Size(118, 36);
            button3.TabIndex = 16;
            button3.Text = "Browse";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(736, 211);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new Size(118, 36);
            button2.TabIndex = 15;
            button2.Text = "Browse";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(736, 69);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(118, 36);
            button1.TabIndex = 14;
            button1.Text = "Browse";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panSpec
            // 
            panSpec.Location = new Point(861, 325);
            panSpec.Margin = new Padding(4, 4, 4, 4);
            panSpec.Name = "panSpec";
            panSpec.Size = new Size(139, 129);
            panSpec.TabIndex = 13;
            // 
            // panNormal
            // 
            panNormal.Location = new Point(861, 182);
            panNormal.Margin = new Padding(4, 4, 4, 4);
            panNormal.Name = "panNormal";
            panNormal.Size = new Size(139, 129);
            panNormal.TabIndex = 12;
            // 
            // panColor
            // 
            panColor.Location = new Point(861, 40);
            panColor.Margin = new Padding(4, 4, 4, 4);
            panColor.Name = "panColor";
            panColor.Size = new Size(139, 129);
            panColor.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(771, 325);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(79, 25);
            label6.TabIndex = 10;
            label6.Text = "Specular";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(780, 182);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(71, 25);
            label5.TabIndex = 9;
            label5.Text = "Normal";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(798, 40);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(55, 25);
            label4.TabIndex = 8;
            label4.Text = "Color";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 140);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(55, 25);
            label3.TabIndex = 7;
            label3.Text = "Mesh";
            // 
            // cbMesh
            // 
            cbMesh.FormattingEnabled = true;
            cbMesh.Location = new Point(135, 136);
            cbMesh.Margin = new Padding(4, 4, 4, 4);
            cbMesh.Name = "cbMesh";
            cbMesh.Size = new Size(382, 33);
            cbMesh.TabIndex = 0;
            cbMesh.SelectedIndexChanged += cbMesh_SelectedIndexChanged;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(525, 19);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(49, 25);
            label15.TabIndex = 9;
            label15.Text = "Type";
            // 
            // entType
            // 
            entType.Location = new Point(582, 19);
            entType.Margin = new Padding(4, 4, 4, 4);
            entType.Name = "entType";
            entType.Size = new Size(452, 31);
            entType.TabIndex = 10;
            entType.TextChanged += entType_TextChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(525, 71);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(123, 25);
            label16.TabIndex = 11;
            label16.Text = "Dynamic Type";
            // 
            // cbDynamicType
            // 
            cbDynamicType.FormattingEnabled = true;
            cbDynamicType.Items.AddRange(new object[] { "Static", "Dynamic" });
            cbDynamicType.Location = new Point(660, 68);
            cbDynamicType.Margin = new Padding(4, 4, 4, 4);
            cbDynamicType.Name = "cbDynamicType";
            cbDynamicType.Size = new Size(374, 33);
            cbDynamicType.TabIndex = 12;
            cbDynamicType.SelectedIndexChanged += Change_DynamicType;
            // 
            // EntityEditor
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1050, 689);
            Controls.Add(cbDynamicType);
            Controls.Add(label16);
            Controls.Add(entType);
            Controls.Add(label15);
            Controls.Add(cbMesh);
            Controls.Add(label3);
            Controls.Add(groupBox1);
            Controls.Add(pxType);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(entityName);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 4, 4, 4);
            Name = "EntityEditor";
            Text = "Vivid3D - Entity Editor";
            Load += EntityEditor_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nSpecG).EndInit();
            ((System.ComponentModel.ISupportInitialize)nSpecB).EndInit();
            ((System.ComponentModel.ISupportInitialize)nSpecR).EndInit();
            ((System.ComponentModel.ISupportInitialize)nDiffG).EndInit();
            ((System.ComponentModel.ISupportInitialize)nDiffB).EndInit();
            ((System.ComponentModel.ISupportInitialize)nDiffR).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox entityName;
        private Label label2;
        private ComboBox pxType;
        private GroupBox groupBox1;
        private Panel panSpec;
        private Panel panNormal;
        private Panel panColor;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private ComboBox cbMesh;
        private Button button3;
        private Button button2;
        private Button button1;
        private OpenFileDialog openFileDialog1;
        private Label label12;
        private Label label13;
        private Label label14;
        private NumericUpDown nSpecG;
        private NumericUpDown nSpecB;
        private NumericUpDown nSpecR;
        private Label label11;
        private Label label10;
        private Label label9;
        private NumericUpDown nDiffG;
        private NumericUpDown nDiffB;
        private NumericUpDown nDiffR;
        private Label label8;
        private Label label7;
        private Label label15;
        private TextBox entType;
        private Label label16;
        private ComboBox cbDynamicType;
    }
}