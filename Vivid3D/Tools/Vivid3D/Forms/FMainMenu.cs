using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.UI.Forms;

namespace Vivid3D.Forms
{
    public class FMainMenu : Vivid.UI.Forms.IMenu
    {

        public FMainMenu()
        {
            Set(0, 0, VividApp.FrameWidth, 30, "");

            ToolTip = "The main menu, for you to access various features.";

            var project = AddItem("Project");

            var new_project = project.AddItem("New Project");
            var load_project = project.AddItem("Load Project");
            var save_project = project.AddItem("Save Project");
            var proj_sep1 = project.AddItem("-----");
            var exit_app = project.AddItem("Exit");

            var edit = AddItem("Edit");


            exit_app.Click += (item) =>
            {
                Environment.Exit(1);
            };
            
            




        }

    }
}
