namespace Editor.Editors
{
    partial class LightEditor
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
            lightName = new TextBox();
            label1 = new Label();
            colorDialog1 = new ColorDialog();
            diffRed = new NumericUpDown();
            diffGreen = new NumericUpDown();
            diffBlue = new NumericUpDown();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            button1 = new Button();
            button2 = new Button();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            specBlue = new NumericUpDown();
            specGreen = new NumericUpDown();
            specRed = new NumericUpDown();
            label10 = new Label();
            lightRange = new NumericUpDown();
            shadowsEnabled = new CheckBox();
            lightType = new ComboBox();
            lightTypelabel = new Label();
            ((System.ComponentModel.ISupportInitialize)diffRed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)diffGreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)diffBlue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)specBlue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)specGreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)specRed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lightRange).BeginInit();
            SuspendLayout();
            // 
            // lightName
            // 
            lightName.Location = new Point(84, 9);
            lightName.Margin = new Padding(4, 4, 4, 4);
            lightName.Name = "lightName";
            lightName.Size = new Size(382, 31);
            lightName.TabIndex = 0;
            lightName.TextChanged += lightName_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 12);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(59, 25);
            label1.TabIndex = 1;
            label1.Text = "Name";
            // 
            // diffRed
            // 
            diffRed.DecimalPlaces = 3;
            diffRed.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            diffRed.Location = new Point(141, 66);
            diffRed.Margin = new Padding(4, 4, 4, 4);
            diffRed.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            diffRed.Name = "diffRed";
            diffRed.Size = new Size(81, 31);
            diffRed.TabIndex = 2;
            diffRed.ValueChanged += diffRed_ValueChanged;
            // 
            // diffGreen
            // 
            diffGreen.DecimalPlaces = 3;
            diffGreen.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            diffGreen.Location = new Point(298, 66);
            diffGreen.Margin = new Padding(4, 4, 4, 4);
            diffGreen.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            diffGreen.Name = "diffGreen";
            diffGreen.Size = new Size(81, 31);
            diffGreen.TabIndex = 3;
            diffGreen.ValueChanged += diffGreen_ValueChanged;
            // 
            // diffBlue
            // 
            diffBlue.DecimalPlaces = 3;
            diffBlue.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            diffBlue.Location = new Point(441, 69);
            diffBlue.Margin = new Padding(4, 4, 4, 4);
            diffBlue.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            diffBlue.Name = "diffBlue";
            diffBlue.Size = new Size(81, 31);
            diffBlue.TabIndex = 4;
            diffBlue.ValueChanged += diffBlue_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 69);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(68, 25);
            label2.TabIndex = 5;
            label2.Text = "Diffuse";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(91, 69);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(42, 25);
            label3.TabIndex = 6;
            label3.Text = "Red";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(230, 71);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(58, 25);
            label4.TabIndex = 7;
            label4.Text = "Green";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(386, 74);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(45, 25);
            label5.TabIndex = 8;
            label5.Text = "Blue";
            // 
            // button1
            // 
            button1.Location = new Point(530, 66);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(118, 36);
            button1.TabIndex = 9;
            button1.Text = "Set";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(531, 119);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new Size(118, 36);
            button2.TabIndex = 17;
            button2.Text = "Set";
            button2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(388, 126);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(45, 25);
            label6.TabIndex = 16;
            label6.Text = "Blue";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(231, 124);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(58, 25);
            label7.TabIndex = 15;
            label7.Text = "Green";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(92, 121);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(42, 25);
            label8.TabIndex = 14;
            label8.Text = "Red";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(15, 121);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(79, 25);
            label9.TabIndex = 13;
            label9.Text = "Specular";
            // 
            // specBlue
            // 
            specBlue.DecimalPlaces = 3;
            specBlue.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            specBlue.Location = new Point(442, 121);
            specBlue.Margin = new Padding(4, 4, 4, 4);
            specBlue.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            specBlue.Name = "specBlue";
            specBlue.Size = new Size(81, 31);
            specBlue.TabIndex = 12;
            specBlue.ValueChanged += specBlue_ValueChanged;
            // 
            // specGreen
            // 
            specGreen.DecimalPlaces = 3;
            specGreen.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            specGreen.Location = new Point(299, 119);
            specGreen.Margin = new Padding(4, 4, 4, 4);
            specGreen.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            specGreen.Name = "specGreen";
            specGreen.Size = new Size(81, 31);
            specGreen.TabIndex = 11;
            specGreen.ValueChanged += specGreen_ValueChanged;
            // 
            // specRed
            // 
            specRed.DecimalPlaces = 3;
            specRed.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            specRed.Location = new Point(142, 119);
            specRed.Margin = new Padding(4, 4, 4, 4);
            specRed.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            specRed.Name = "specRed";
            specRed.Size = new Size(81, 31);
            specRed.TabIndex = 10;
            specRed.ValueChanged += specRed_ValueChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(21, 184);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(62, 25);
            label10.TabIndex = 18;
            label10.Text = "Range";
            // 
            // lightRange
            // 
            lightRange.DecimalPlaces = 1;
            lightRange.Location = new Point(92, 180);
            lightRange.Margin = new Padding(4, 4, 4, 4);
            lightRange.Maximum = new decimal(new int[] { 350, 0, 0, 0 });
            lightRange.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            lightRange.Name = "lightRange";
            lightRange.Size = new Size(131, 31);
            lightRange.TabIndex = 19;
            lightRange.Value = new decimal(new int[] { 5, 0, 0, 0 });
            lightRange.ValueChanged += lightRange_ValueChanged;
            // 
            // shadowsEnabled
            // 
            shadowsEnabled.AutoSize = true;
            shadowsEnabled.Location = new Point(496, 12);
            shadowsEnabled.Margin = new Padding(4, 4, 4, 4);
            shadowsEnabled.Name = "shadowsEnabled";
            shadowsEnabled.Size = new Size(149, 29);
            shadowsEnabled.TabIndex = 20;
            shadowsEnabled.Text = "Cast Shadows";
            shadowsEnabled.UseVisualStyleBackColor = true;
            shadowsEnabled.CheckedChanged += shadowsEnabled_CheckedChanged;
            // 
            // lightType
            // 
            lightType.FormattingEnabled = true;
            lightType.Location = new Point(298, 179);
            lightType.Margin = new Padding(4, 4, 4, 4);
            lightType.Name = "lightType";
            lightType.Size = new Size(225, 33);
            lightType.TabIndex = 21;
            lightType.SelectedIndexChanged += lightType_SelectedIndexChanged;
            // 
            // lightTypelabel
            // 
            lightTypelabel.AutoSize = true;
            lightTypelabel.Location = new Point(241, 184);
            lightTypelabel.Margin = new Padding(4, 0, 4, 0);
            lightTypelabel.Name = "lightTypelabel";
            lightTypelabel.Size = new Size(49, 25);
            lightTypelabel.TabIndex = 22;
            lightTypelabel.Text = "Type";
            // 
            // LightEditor
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(659, 234);
            Controls.Add(lightTypelabel);
            Controls.Add(lightType);
            Controls.Add(shadowsEnabled);
            Controls.Add(lightRange);
            Controls.Add(label10);
            Controls.Add(button2);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(specBlue);
            Controls.Add(specGreen);
            Controls.Add(specRed);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(diffBlue);
            Controls.Add(diffGreen);
            Controls.Add(diffRed);
            Controls.Add(label1);
            Controls.Add(lightName);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 4, 4, 4);
            Name = "LightEditor";
            Text = "Vivid3D - Light Editor";
            Load += LightEditor_Load;
            ((System.ComponentModel.ISupportInitialize)diffRed).EndInit();
            ((System.ComponentModel.ISupportInitialize)diffGreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)diffBlue).EndInit();
            ((System.ComponentModel.ISupportInitialize)specBlue).EndInit();
            ((System.ComponentModel.ISupportInitialize)specGreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)specRed).EndInit();
            ((System.ComponentModel.ISupportInitialize)lightRange).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox lightName;
        private Label label1;
        private ColorDialog colorDialog1;
        private NumericUpDown diffRed;
        private NumericUpDown diffGreen;
        private NumericUpDown diffBlue;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button button1;
        private Button button2;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private NumericUpDown specBlue;
        private NumericUpDown specGreen;
        private NumericUpDown specRed;
        private Label label10;
        private NumericUpDown lightRange;
        private CheckBox shadowsEnabled;
        private ComboBox lightType;
        private Label lightTypelabel;
    }
}