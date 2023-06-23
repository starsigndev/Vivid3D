using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.IO;
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

            var project = AddItem("Scene");

            var new_scene = project.AddItem("New Scene");
            var load_scene = project.AddItem("Load Scene");
            var save_scene = project.AddItem("Save Scene");
            var proj_sep1 = project.AddItem("-----");
            var exit_app = project.AddItem("Exit");

            load_scene.Click += (form) =>
            {
                IFileRequestor request = new IFileRequestor("Load Scene", RequestorType.Load, Editor.ProjectPath);
                Vivid.UI.UI.This.Top = request;
                request.OnFileSelected += (file) =>
                {
                    Editor.Stop();
                    Console.WriteLine("Loading:" + file);
                    SceneIO io = new SceneIO();
                    var scene = io.LoadScene(file);
                    Editor.SetScene(scene);
                    FConsoleOutput.LogMessage("Loaded scene from:" + file);
           


                };

            };

            save_scene.Click += (form) =>
            {
                IFileRequestor request = new IFileRequestor("Save Scene as...",RequestorType.Save,Editor.ProjectPath,".scene");
                Vivid.UI.UI.This.Top = request;
                request.OnFileSelected += (file) =>
                {
                    
                    Console.WriteLine("Saving:" + file);
                    SceneIO io = new SceneIO();
                    io.SaveScene(Editor.CurrentScene,file);
                    FConsoleOutput.LogMessage("Saved scene to:" + file);
                
                };
            };

            var edit = AddItem("Edit");

 //           var edit_csg = edit.AddItem("CSG");

   //         var csg_union = edit_csg.AddItem("Open CSG Editor");

     //       csg_union.Click += (item) =>
            {

              //  WCsg new_csg = new WCsg();
               
             //   Vivid3DApp.MainUI.AddWindow(new_csg);

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

            var settings = AddItem("Settings");

            var settings_pp = settings.AddItem("Post-Processing");

            settings_pp.Click += (item) =>
            {

                WPostProcessing pp_win = new WPostProcessing();
                Vivid3DApp.MainUI.AddWindow(pp_win);

            };


            var ai = AddItem("A.I");

            var asker = ai.AddItem("AI Chat Helper");

            asker.Click += (form) =>
            {
                WAIHelper helper = new WAIHelper();
                Vivid3DApp.MainUI.AddWindow(helper);
            };



        }

    }
}
