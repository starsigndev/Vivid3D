﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.UI.Forms;
using Vivid3D.Windows;

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

            var edit_csg = edit.AddItem("CSG");

            var csg_union = edit_csg.AddItem("Open CSG Editor");

            csg_union.Click += (item) =>
            {

                WCsg new_csg = new WCsg();
               
                Vivid3DApp.MainUI.AddWindow(new_csg);

            };

            exit_app.Click += (item) =>
            {
                Environment.Exit(1);
            };

            var cr = AddItem("Create");

            var cr_lights = cr.AddItem("Lights");

            var cr_lights_point = cr_lights.AddItem("Point Light");
            var cr_lights_spot = cr_lights.AddItem("Spot Light");
            var cr_lights_dir = cr_lights.AddItem("Directional Light");

            cr_lights_point.Click += (item) =>
            {

                Editor.CreatePointLight();

            };






        }

    }
}
