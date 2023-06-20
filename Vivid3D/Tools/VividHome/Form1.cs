namespace VividHome
{
    public partial class Form1 : Form
    {
        public static Form1 This = null;
        public static List<string> Projects = new List<string>();
        public Form1()
        {
            InitializeComponent();
            This = this;
            LoadProjects();
        }

        public static void LoadProjects()
        {
            if (File.Exists("projects.list") == false)
            {
                SaveProjects();
            }

            Projects.Clear();
            FileStream fs = new FileStream("projects.list", FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            int pc = r.ReadInt32();
            for (int i = 0; i < pc; i++)
            {
                Projects.Add(r.ReadString());
            }
            fs.Close();
            This.UpdateUI();

        }

        public static void SaveProjects()
        {
            FileStream fs = new FileStream("projects.list", FileMode.Create, FileAccess.Write);
            BinaryWriter w = new BinaryWriter(fs);
            w.Write(Projects.Count);
            foreach (var proj in Projects)
            {
                w.Write((string)proj);
            }

            w.Flush();
            fs.Flush();
            fs.Close();


        }

        public void UpdateUI()
        {
            lbProjects.Items.Clear();
            foreach (var proj in Projects)
            {
                lbProjects.Items.Add(Path.GetFileNameWithoutExtension(proj));
            }
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject proj = new NewProject();

            proj.Show();
        }
    }
}