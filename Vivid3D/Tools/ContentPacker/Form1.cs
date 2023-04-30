using Vivid.Content;

namespace ContentPacker
{
    public partial class Form1 : Form
    {

        public Content ActiveContent = null;
        public string OutputPath = "";

        public Form1()
        {
            InitializeComponent();
            ActiveContent = new Content();
            RebuildUI();
            LoadOutputPath();
            if (File.Exists("contentname.inf"))
            {
                contentName.Text = File.ReadAllText("contentname.inf");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            File.WriteAllText("contentname.inf", contentName.Text);


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void RebuildUI()
        {

            contentList.Items.Clear();

            foreach (var file in ActiveContent.Items)
            {
                // ListViewItem item = new ListViewItem(file.DottedName);
                contentList.Items.Add(file);


            }

        }

        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();

            var file = openFileDialog1.FileName;

            FileInfo info = new FileInfo(file);

            ActiveContent.AddFile(info);

            RebuildUI();


        }

        public void AddDir(string path)
        {
            var folder = new DirectoryInfo(path);
            foreach (var file in folder.GetFiles())
            {

                ActiveContent.AddFile(file);


            }
            foreach(var fold in folder.GetDirectories())
            {

                AddDir(fold.FullName);

            }

        }
        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            AddDir(folderBrowserDialog1.SelectedPath);
                        //var folder = new DirectoryInfo(folderBrowserDialog1.SelectedPath);

           
            RebuildUI();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            OutputPath = folderBrowserDialog1.SelectedPath + "\\";
            output.Text = OutputPath;
            SaveOutputPath(OutputPath);


        }

        public void SaveOutputPath(string path)
        {
            File.WriteAllText("packer.inf", path);
        }
        public void LoadOutputPath()
        {
            if (File.Exists("packer.inf") == false)
            {
                return;
            }


            OutputPath = File.ReadAllText("packer.inf");
            output.Text = OutputPath;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ActiveContent.Build(OutputPath + contentName.Text);
            RebuildUI();
            MessageBox.Show("Content successfully built.", "Content Builder");
        }

        private void buildContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveContent.Build(OutputPath + contentName.Text);
            RebuildUI();
            MessageBox.Show("Content successfully built.", "Content Builder");
        }

        private void output_TextChanged(object sender, EventArgs e)
        {
            OutputPath = output.Text;
            SaveOutputPath(OutputPath);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Content List Files (*.list)|*.list|All Files (*.*)|*.*";
            saveFileDialog1.DefaultExt = "list"; // This line sets the default extension to .txt
            saveFileDialog1.ShowDialog(this);
            string path = saveFileDialog1.FileName;


            string[] paths = new string[ActiveContent.Items.Count];
            for (int i = 0; i < ActiveContent.Items.Count; i++)
            {
                paths[i] = ActiveContent.Items[i].FullName;



            }

            File.WriteAllLines(path, paths);

        }

        private void loadListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveContent = new Content();
            openFileDialog1.Filter = "Content List Files (*.list)|*.list|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "list"; // This 
            openFileDialog1.ShowDialog();

            string path = openFileDialog1.FileName;
            if (File.Exists(path) == false)
            {
                MessageBox.Show("File does not exist.");
                RebuildUI();
                return;

            }

            string[] paths = File.ReadAllLines(path);

            for (int i = 0; i < paths.Length; i++)
            {
                var fi = new FileInfo(paths[i]);
                ActiveContent.AddFile(fi);

            }

            RebuildUI();
        }

        private void exitAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void createVirtualFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveContent = new Content();
            RebuildUI();
        }
    }
}