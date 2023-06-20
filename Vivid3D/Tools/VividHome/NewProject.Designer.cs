namespace VividHome
{
    partial class NewProject
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
            tbPath = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(196, 20);
            label1.TabIndex = 0;
            label1.Text = "Project Path(Must be empty)";
            // 
            // tbPath
            // 
            tbPath.Location = new Point(214, 9);
            tbPath.Name = "tbPath";
            tbPath.Size = new Size(477, 27);
            tbPath.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(697, 8);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "Browse";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 153);
            button2.Name = "button2";
            button2.Size = new Size(128, 29);
            button2.TabIndex = 3;
            button2.Text = "Create Project";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(654, 153);
            button3.Name = "button3";
            button3.Size = new Size(137, 29);
            button3.TabIndex = 4;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            // 
            // NewProject
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(803, 194);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(tbPath);
            Controls.Add(label1);
            Name = "NewProject";
            Text = "Vivid3D - New Project";
            Load += NewProject_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbPath;
        private Button button1;
        private Button button2;
        private Button button3;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}