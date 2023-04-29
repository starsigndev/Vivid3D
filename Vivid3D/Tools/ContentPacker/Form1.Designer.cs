namespace ContentPacker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            contentName = new TextBox();
            label2 = new Label();
            output = new TextBox();
            button1 = new Button();
            button6 = new Button();
            menuStrip1 = new MenuStrip();
            contentToolStripMenuItem = new ToolStripMenuItem();
            addFileToolStripMenuItem = new ToolStripMenuItem();
            addFolderToolStripMenuItem = new ToolStripMenuItem();
            createVirtualFolderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            loadListToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitAppToolStripMenuItem = new ToolStripMenuItem();
            buildToolStripMenuItem = new ToolStripMenuItem();
            buildContentToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            contentList = new ListBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            saveFileDialog1 = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 47);
            label1.Name = "label1";
            label1.Size = new Size(105, 20);
            label1.TabIndex = 0;
            label1.Text = "Content Name";
            label1.Click += label1_Click;
            // 
            // contentName
            // 
            contentName.Location = new Point(119, 44);
            contentName.Name = "contentName";
            contentName.Size = new Size(224, 27);
            contentName.TabIndex = 1;
            contentName.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(284, 474);
            label2.Name = "label2";
            label2.Size = new Size(79, 20);
            label2.TabIndex = 2;
            label2.Text = "Output Dir";
            // 
            // output
            // 
            output.Location = new Point(369, 471);
            output.Name = "output";
            output.Size = new Size(439, 27);
            output.TabIndex = 3;
            output.TextChanged += output_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(814, 471);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 4;
            button1.Text = "Browse";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button6
            // 
            button6.Location = new Point(12, 469);
            button6.Name = "button6";
            button6.Size = new Size(266, 29);
            button6.TabIndex = 10;
            button6.Text = "Build Content";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { contentToolStripMenuItem, buildToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(919, 28);
            menuStrip1.TabIndex = 11;
            menuStrip1.Text = "menuStrip1";
            // 
            // contentToolStripMenuItem
            // 
            contentToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addFileToolStripMenuItem, addFolderToolStripMenuItem, createVirtualFolderToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem, loadListToolStripMenuItem, toolStripSeparator2, exitAppToolStripMenuItem });
            contentToolStripMenuItem.Name = "contentToolStripMenuItem";
            contentToolStripMenuItem.Size = new Size(75, 24);
            contentToolStripMenuItem.Text = "Content";
            // 
            // addFileToolStripMenuItem
            // 
            addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
            addFileToolStripMenuItem.Size = new Size(224, 26);
            addFileToolStripMenuItem.Text = "Add File...";
            addFileToolStripMenuItem.Click += addFileToolStripMenuItem_Click;
            // 
            // addFolderToolStripMenuItem
            // 
            addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            addFolderToolStripMenuItem.Size = new Size(224, 26);
            addFolderToolStripMenuItem.Text = "Add Folder...";
            addFolderToolStripMenuItem.Click += addFolderToolStripMenuItem_Click;
            // 
            // createVirtualFolderToolStripMenuItem
            // 
            createVirtualFolderToolStripMenuItem.Name = "createVirtualFolderToolStripMenuItem";
            createVirtualFolderToolStripMenuItem.Size = new Size(224, 26);
            createVirtualFolderToolStripMenuItem.Text = "Clear List";
            createVirtualFolderToolStripMenuItem.Click += createVirtualFolderToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(221, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(224, 26);
            exitToolStripMenuItem.Text = "Save List";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // loadListToolStripMenuItem
            // 
            loadListToolStripMenuItem.Name = "loadListToolStripMenuItem";
            loadListToolStripMenuItem.Size = new Size(224, 26);
            loadListToolStripMenuItem.Text = "Load List";
            loadListToolStripMenuItem.Click += loadListToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(221, 6);
            // 
            // exitAppToolStripMenuItem
            // 
            exitAppToolStripMenuItem.Name = "exitAppToolStripMenuItem";
            exitAppToolStripMenuItem.Size = new Size(224, 26);
            exitAppToolStripMenuItem.Text = "Exit App";
            exitAppToolStripMenuItem.Click += exitAppToolStripMenuItem_Click;
            // 
            // buildToolStripMenuItem
            // 
            buildToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { buildContentToolStripMenuItem });
            buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            buildToolStripMenuItem.Size = new Size(57, 24);
            buildToolStripMenuItem.Text = "Build";
            // 
            // buildContentToolStripMenuItem
            // 
            buildContentToolStripMenuItem.Name = "buildContentToolStripMenuItem";
            buildContentToolStripMenuItem.Size = new Size(182, 26);
            buildContentToolStripMenuItem.Text = "Build Content";
            buildContentToolStripMenuItem.Click += buildContentToolStripMenuItem_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // contentList
            // 
            contentList.FormattingEnabled = true;
            contentList.ItemHeight = 20;
            contentList.Location = new Point(369, 44);
            contentList.Name = "contentList";
            contentList.Size = new Size(538, 424);
            contentList.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(919, 510);
            Controls.Add(contentList);
            Controls.Add(button6);
            Controls.Add(button1);
            Controls.Add(output);
            Controls.Add(label2);
            Controls.Add(contentName);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Gemini - Content Packer (c)StarSign Games";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox contentName;
        private Label label2;
        private TextBox output;
        private Button button1;
        private Button button6;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem contentToolStripMenuItem;
        private ToolStripMenuItem addFileToolStripMenuItem;
        private ToolStripMenuItem addFolderToolStripMenuItem;
        private ToolStripMenuItem createVirtualFolderToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem buildToolStripMenuItem;
        private ToolStripMenuItem buildContentToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private ListBox contentList;
        private FolderBrowserDialog folderBrowserDialog1;
        private ToolStripMenuItem loadListToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitAppToolStripMenuItem;
    }
}