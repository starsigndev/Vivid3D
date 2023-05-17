namespace SceneEditor.Editors
{
    partial class SkyEditor
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
            cbProceduralSky = new CheckBox();
            SuspendLayout();
            // 
            // cbProceduralSky
            // 
            cbProceduralSky.AutoSize = true;
            cbProceduralSky.Location = new Point(12, 12);
            cbProceduralSky.Name = "cbProceduralSky";
            cbProceduralSky.Size = new Size(128, 24);
            cbProceduralSky.TabIndex = 0;
            cbProceduralSky.Text = "Procedural Sky";
            cbProceduralSky.UseVisualStyleBackColor = true;
            cbProceduralSky.CheckedChanged += cbProceduralSky_CheckedChanged;
            // 
            // SkyEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cbProceduralSky);
            Name = "SkyEditor";
            Text = "Vivid3D - Sky Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox cbProceduralSky;
    }
}