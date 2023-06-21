using System.Diagnostics;

namespace VividHome
{
    public partial class VividHome : Form
    {

        public static string EnginePath = "C:\\G3D\\Vivid3D\\Tools\\Vivid3D\\bin\\x64\\Debug\\net7.0\\Vivid3D.exe";

        public static VividHome This = null;
        public static List<string> Projects = new List<string>();
        public VividHome()
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

        private void button1_Click(object sender, EventArgs e)
        {

            int proj_num = lbProjects.SelectedIndex;
            string actual_path = Projects[proj_num];

            string directory = Path.GetDirectoryName(EnginePath);

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = EnginePath,
                Arguments = actual_path,
                WorkingDirectory = directory
            };

            Process process = new Process()
            {
                StartInfo = startInfo
            };

            process.Start();

        }
    }
}