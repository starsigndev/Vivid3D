namespace Editor.Tools
{
    partial class TextureSources
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
            foldersToolStripMenuItem = new ToolStripMenuItem();
            addAllWithinFolderToolStripMenuItem = new ToolStripMenuItem();
            tsList = new ListBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { foldersToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(394, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // foldersToolStripMenuItem
            // 
            foldersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addAllWithinFolderToolStripMenuItem });
            foldersToolStripMenuItem.Name = "foldersToolStripMenuItem";
            foldersToolStripMenuItem.Size = new Size(71, 24);
            foldersToolStripMenuItem.Text = "Folders";
            // 
            // addAllWithinFolderToolStripMenuItem
            // 
            addAllWithinFolderToolStripMenuItem.Name = "addAllWithinFolderToolStripMenuItem";
            addAllWithinFolderToolStripMenuItem.Size = new Size(235, 26);
            addAllWithinFolderToolStripMenuItem.Text = "Add All Within Folder";
            addAllWithinFolderToolStripMenuItem.Click += addAllWithinFolderToolStripMenuItem_Click;
            // 
            // tsList
            // 
            tsList.FormattingEnabled = true;
            tsList.ItemHeight = 20;
            tsList.Location = new Point(12, 31);
            tsList.Name = "tsList";
            tsList.Size = new Size(372, 504);
            tsList.TabIndex = 1;
            // 
            // TextureSources
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 543);
            Controls.Add(tsList);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MainMenuStrip = menuStrip1;
            Name = "TextureSources";
            Text = "Vivid3D - Texture Sources";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem foldersToolStripMenuItem;
        private ToolStripMenuItem addAllWithinFolderToolStripMenuItem;
        private ListBox tsList;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}