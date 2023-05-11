using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor.Tools
{

    public partial class TextureSources : Form
    {
        public class TextureSource
        {
            public string Name = "";
            public string FullPath = "";
            public override string ToString()
            {
                return Name;
            }
        }
        public static List<TextureSource> Sources = new List<TextureSource>();
        public TextureSources()
        {
            InitializeComponent();
            LoadList();
            Rebuild();

        }

        private void addAllWithinFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

            folderBrowserDialog1.ShowDialog();

            string path = folderBrowserDialog1.SelectedPath;

            AddFolders(path);
            Rebuild();
            SaveList();

        }
        public static void LoadList()
        {

            if (File.Exists("texsources.list") == false)
            {
                return;
            }

            Sources.Clear();

            FileStream fs = new FileStream("texsources.list", FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

            int c = r.ReadInt32();

            for(int i = 0; i < c; i++)
            {

                TextureSource src = new TextureSource();
                src.Name = r.ReadString();
                src.FullPath = r.ReadString();
                Sources.Add(src);

            }

            Vivid.Importing.Importer.Sources.Clear();
            foreach (var ts in Sources)
            {
               // tsList.Items.Add(ts);


                Vivid.Importing.Importer.AddSource(ts.Name, ts.FullPath);
            };
        }
        public static void SaveList()
        {

            FileStream fs = new FileStream("texsources.list", FileMode.Create, FileAccess.Write);
            BinaryWriter w = new BinaryWriter(fs);

            w.Write(Sources.Count);
            foreach(TextureSource source in Sources)
            {

                w.Write(source.Name);
                w.Write(source.FullPath);

            }
            w.Flush();
            fs.Flush();
            fs.Close();

        }

        public void Rebuild()
        {

            tsList.Items.Clear();
            Vivid.Importing.Importer.Sources.Clear();
            foreach (var ts in Sources)
            {
                tsList.Items.Add(ts);


                //Vivid.Importing.Importer.AddSource(ts.Name, ts.FullPath);
            };
        }

        public void AddFolders(string path)
        {
            foreach (var file in new DirectoryInfo(path).GetDirectories())
            {
                AddFolders(file.FullName);
            }

            foreach (var file in new DirectoryInfo(path).GetFiles())
            {

                string ext = file.Extension;

                switch (ext)
                {
                    case ".jpg":
                    case ".png":
                    case ".bmp":
                        TextureSource ts = new TextureSource();
                        ts.Name = file.Name;
                        ts.FullPath = file.FullName;
                        Sources.Add(ts);
                        break;
                }
            }

        }

    }
}
