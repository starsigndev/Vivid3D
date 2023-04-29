namespace SceneEditor.Editors
{
    partial class SpawnEditor
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
            spawnName = new TextBox();
            label2 = new Label();
            spawnIndex = new NumericUpDown();
            label3 = new Label();
            spawnType = new TextBox();
            ((System.ComponentModel.ISupportInitialize)spawnIndex).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 5;
            label1.Text = "Name";
            // 
            // spawnName
            // 
            spawnName.Location = new Point(79, 12);
            spawnName.Name = "spawnName";
            spawnName.Size = new Size(306, 27);
            spawnName.TabIndex = 4;
            spawnName.TextChanged += spawnName_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 60);
            label2.Name = "label2";
            label2.Size = new Size(45, 20);
            label2.TabIndex = 6;
            label2.Text = "Index";
            // 
            // spawnIndex
            // 
            spawnIndex.Location = new Point(79, 60);
            spawnIndex.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            spawnIndex.Name = "spawnIndex";
            spawnIndex.Size = new Size(150, 27);
            spawnIndex.TabIndex = 7;
            spawnIndex.ValueChanged += spawnIndex_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 113);
            label3.Name = "label3";
            label3.Size = new Size(40, 20);
            label3.TabIndex = 8;
            label3.Text = "Type";
            // 
            // spawnType
            // 
            spawnType.Location = new Point(79, 110);
            spawnType.Name = "spawnType";
            spawnType.Size = new Size(306, 27);
            spawnType.TabIndex = 9;
            spawnType.TextChanged += spawnType_TextChanged;
            // 
            // SpawnEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(402, 150);
            Controls.Add(spawnType);
            Controls.Add(label3);
            Controls.Add(spawnIndex);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(spawnName);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "SpawnEditor";
            Text = "Gemini - Spawn Editor";
            ((System.ComponentModel.ISupportInitialize)spawnIndex).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox spawnName;
        private Label label2;
        private NumericUpDown spawnIndex;
        private Label label3;
        private TextBox spawnType;
    }
}