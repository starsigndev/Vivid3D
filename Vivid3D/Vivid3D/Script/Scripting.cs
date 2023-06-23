using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSScriptLib;

namespace Vivid.Script
{
    public class Scripting
    {

        public static dynamic LoadScript(string path)
        {

            dynamic res = CSScript.Evaluator.LoadFile(path);

            res.ResourcePath = path;

            res.Begin();

            return res;

        }

    }
}
