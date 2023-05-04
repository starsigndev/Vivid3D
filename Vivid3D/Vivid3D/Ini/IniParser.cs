using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Ini
{
    public class IniRecord
    {
        public string Name
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
    public class IniParser
    {

        public List<IniRecord> Records
        {
            get;
            set;
        }

        public string GetRecord(string name)
        {

            foreach(var rec in Records)
            {
                if (rec.Name.ToLower() == name.ToLower())
                {
                    return rec.Value;
                }
            }
            return null;

        }

        public IniParser(string path)
        {
            Records = new List<IniRecord>();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            TextReader r = new StreamReader(fs);
            ReadContents(r);
        }

        public IniParser(MemoryStream stream)
        {
            Records = new List<IniRecord>();
            TextReader r = new StreamReader(stream);
            ReadContents(r);
        }

        public void ReadContents(TextReader r)
        {

            while (true)
            {
                string line = r.ReadLine();
                if (line == null) break;
                int mp = line.IndexOf(":");
                string rec = line.Substring(0, mp);
                string info = line.Substring(mp + 1);
                IniRecord new_Rec = new IniRecord();
                new_Rec.Name = rec;
                new_Rec.Value = info;
                Records.Add(new_Rec);
                int b = 5;

            }

        }
    }
}
