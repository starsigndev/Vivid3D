using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VividHome
{
    public partial class NewProject : Form
    {
        public NewProject()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            tbPath.Text = folderBrowserDialog1.SelectedPath;
            //CrNewProject(folderBrowserDialog1.SelectedPath);

        }

        public void CrNewProject(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.GetDirectories().Length > 0 || info.GetFiles().Length > 0)
            {
                MessageBox.Show("New project folder must be empty.");
                return;
            }

            Directory.CreateDirectory(path + "\\Code\\");

            File.Copy("res/ProjectCode.csproj", path + "\\Code\\ProjectCode.csproj");

            VividHome.Projects.Add(path);
            VividHome.SaveProjects();
            VividHome.This.UpdateUI();

            MessageBox.Show("Created project at path:" + path);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbPath.Text.Length <= 1)
            {
                MessageBox.Show("Illegal path for new project.");
                return;
            }
            CrNewProject(tbPath.Text);
        }

        private void NewProject_Load(object sender, EventArgs e)
        {

        }
    }
}