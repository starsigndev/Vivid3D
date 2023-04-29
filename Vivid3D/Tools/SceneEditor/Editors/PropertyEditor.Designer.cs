namespace SceneEditor.Editors
{
    partial class PropertyEditor
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
            propList = new ListBox();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            valuePanel = new Panel();
            label3 = new Label();
            propName = new TextBox();
            label4 = new Label();
            propType = new ComboBox();
            SuspendLayout();
            // 
            // propList
            // 
            propList.FormattingEnabled = true;
            propList.Location = new Point(12, 32);
            propList.Name = "propList";
            propList.Size = new Size(294, 404);
            propList.TabIndex = 0;
            propList.SelectedIndexChanged += propList_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(76, 20);
            label1.TabIndex = 1;
            label1.Text = "Properties";
            // 
            // button1
            // 
            button1.Location = new Point(12, 442);
            button1.Name = "button1";
            button1.Size = new Size(138, 29);
            button1.TabIndex = 2;
            button1.Text = "New Property";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(156, 442);
            button2.Name = "button2";
            button2.Size = new Size(150, 29);
            button2.TabIndex = 3;
            button2.Text = "Delete Property";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(312, 128);
            label2.Name = "label2";
            label2.Size = new Size(45, 20);
            label2.TabIndex = 4;
            label2.Text = "Value";
            // 
            // valuePanel
            // 
            valuePanel.Location = new Point(312, 151);
            valuePanel.Name = "valuePanel";
            valuePanel.Size = new Size(476, 153);
            valuePanel.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(312, 46);
            label3.Name = "label3";
            label3.Size = new Size(49, 20);
            label3.TabIndex = 6;
            label3.Text = "Name";
            // 
            // propName
            // 
            propName.Location = new Point(367, 43);
            propName.Name = "propName";
            propName.Size = new Size(421, 27);
            propName.TabIndex = 7;
            propName.TextChanged += propName_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(312, 85);
            label4.Name = "label4";
            label4.Size = new Size(40, 20);
            label4.TabIndex = 8;
            label4.Text = "Type";
            // 
            // propType
            // 
            propType.FormattingEnabled = true;
            propType.Location = new Point(367, 85);
            propType.Name = "propType";
            propType.Size = new Size(421, 28);
            propType.TabIndex = 9;
            propType.SelectedIndexChanged += propType_SelectedIndexChanged;
            // 
            // PropertyEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 479);
            Controls.Add(propType);
            Controls.Add(label4);
            Controls.Add(propName);
            Controls.Add(label3);
            Controls.Add(valuePanel);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(propList);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "PropertyEditor";
            Text = "Gemini - Properties Editor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox propList;
        private Label label1;
        private Button button1;
        private Button button2;
        private Label label2;
        private Panel valuePanel;
        private Label label3;
        private TextBox propName;
        private Label label4;
        private ComboBox propType;
    }
}