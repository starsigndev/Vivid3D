namespace Editor.Editors
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
            propList.ItemHeight = 25;
            propList.Location = new Point(15, 40);
            propList.Margin = new Padding(4, 4, 4, 4);
            propList.Name = "propList";
            propList.Size = new Size(366, 504);
            propList.TabIndex = 0;
            propList.SelectedIndexChanged += propList_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 11);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(92, 25);
            label1.TabIndex = 1;
            label1.Text = "Properties";
            // 
            // button1
            // 
            button1.Location = new Point(15, 552);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(172, 36);
            button1.TabIndex = 2;
            button1.Text = "New Property";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(195, 552);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new Size(188, 36);
            button2.TabIndex = 3;
            button2.Text = "Delete Property";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(390, 160);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 25);
            label2.TabIndex = 4;
            label2.Text = "Value";
            // 
            // valuePanel
            // 
            valuePanel.Location = new Point(390, 189);
            valuePanel.Margin = new Padding(4, 4, 4, 4);
            valuePanel.Name = "valuePanel";
            valuePanel.Size = new Size(595, 191);
            valuePanel.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(390, 58);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(59, 25);
            label3.TabIndex = 6;
            label3.Text = "Name";
            // 
            // propName
            // 
            propName.Location = new Point(459, 54);
            propName.Margin = new Padding(4, 4, 4, 4);
            propName.Name = "propName";
            propName.Size = new Size(525, 31);
            propName.TabIndex = 7;
            propName.TextChanged += propName_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(390, 106);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(49, 25);
            label4.TabIndex = 8;
            label4.Text = "Type";
            // 
            // propType
            // 
            propType.FormattingEnabled = true;
            propType.Location = new Point(459, 106);
            propType.Margin = new Padding(4, 4, 4, 4);
            propType.Name = "propType";
            propType.Size = new Size(525, 33);
            propType.TabIndex = 9;
            propType.SelectedIndexChanged += propType_SelectedIndexChanged;
            // 
            // PropertyEditor
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1000, 599);
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
            Margin = new Padding(4, 4, 4, 4);
            Name = "PropertyEditor";
            Text = "Vivid3D - Properties Editor";
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