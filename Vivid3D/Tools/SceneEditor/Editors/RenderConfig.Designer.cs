namespace Editor.Editors
{
    partial class RenderConfig
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
            cbBloom = new CheckBox();
            button1 = new Button();
            cbOutline = new CheckBox();
            button2 = new Button();
            cbLightShafts = new CheckBox();
            button3 = new Button();
            SuspendLayout();
            // 
            // cbBloom
            // 
            cbBloom.AutoSize = true;
            cbBloom.Location = new Point(12, 12);
            cbBloom.Name = "cbBloom";
            cbBloom.Size = new Size(75, 24);
            cbBloom.TabIndex = 0;
            cbBloom.Text = "Bloom";
            cbBloom.UseVisualStyleBackColor = true;
            cbBloom.CheckedChanged += CB_Bloom;
            // 
            // button1
            // 
            button1.Location = new Point(131, 9);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "Settings";
            button1.UseVisualStyleBackColor = true;
            // 
            // cbOutline
            // 
            cbOutline.AutoSize = true;
            cbOutline.Location = new Point(12, 47);
            cbOutline.Name = "cbOutline";
            cbOutline.Size = new Size(79, 24);
            cbOutline.TabIndex = 2;
            cbOutline.Text = "Outline";
            cbOutline.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(131, 44);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 3;
            button2.Text = "Settings";
            button2.UseVisualStyleBackColor = true;
            // 
            // cbLightShafts
            // 
            cbLightShafts.AutoSize = true;
            cbLightShafts.Location = new Point(12, 82);
            cbLightShafts.Name = "cbLightShafts";
            cbLightShafts.Size = new Size(108, 24);
            cbLightShafts.TabIndex = 4;
            cbLightShafts.Text = "Light Shafts";
            cbLightShafts.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(131, 79);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 5;
            button3.Text = "Settings";
            button3.UseVisualStyleBackColor = true;
            // 
            // RenderConfig
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(244, 287);
            Controls.Add(button3);
            Controls.Add(cbLightShafts);
            Controls.Add(button2);
            Controls.Add(cbOutline);
            Controls.Add(button1);
            Controls.Add(cbBloom);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "RenderConfig";
            Text = "Vivid3D - Renderer Config";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox cbBloom;
        private Button button1;
        private CheckBox cbOutline;
        private Button button2;
        private CheckBox cbLightShafts;
        private Button button3;
    }
}