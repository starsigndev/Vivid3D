using Vivid.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using static SceneEditor.SceneEditor;
using Vivid.Nodes;
using Vivid.Scene;

namespace SceneEditor.Logic
{
    public class Mouse
    {
        public static void Up(MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                CamDrag = false;
            }
            else
            {
                GizDrag = false;
                GizLockX = false;
                GizLockY = false;
                GizLockZ = false;
            }

        }
        public static void Down(MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {

                SceneEditor.CamDrag = true;

            }
            else if (e.Button == MouseButtons.Left)
            {


                if (SceneEditor.GizDrag)
                {
                    return;
                }

                //Vivid.App.GeminiApp.FrameWidth = SceneEditor.Output.Size.Width;
              //  Vivid.App.GeminiApp.FrameHeight = SceneEditor.Output.Size.Height;


                
                var res = SceneEditor.EditScene.MousePickNode((int)SceneEditor.MouseX, (int)SceneEditor.MouseY, SceneEditor.CurrentGizmo);
                
                if (res.Hit)
                {
                    if (SceneEditor.CurrentGizmo == SceneEditor.GizmoTranslate)
                    {
                        if (res.Mesh == SceneEditor.CurrentGizmo.Meshes[0])
                        {
                            SceneEditor.GizDrag = true;
                            SceneEditor.GizLockX = false;
                            SceneEditor.GizLockY = false;
                            SceneEditor.GizLockZ = true;
                        }
                        if (res.Mesh == SceneEditor.CurrentGizmo.Meshes[1])
                        {
                            SceneEditor.GizDrag = true;
                            SceneEditor.GizLockX = true;
                            SceneEditor.GizLockY = false;
                            SceneEditor.GizLockZ = false;
                        }
                        if (res.Mesh == SceneEditor.CurrentGizmo.Meshes[2])
                        {
                            SceneEditor.GizDrag = true;
                            SceneEditor.GizLockX = false;
                             SceneEditor.GizLockY = true;
                            SceneEditor.GizLockZ = false;
                        }
                    }
                    else if (SceneEditor.CurrentGizmo == SceneEditor.GizmoRotate)
                    {

                        if (res.Mesh == SceneEditor.CurrentGizmo.Meshes[2])
                        {
                            SceneEditor.GizDrag = true;
                            SceneEditor.GizLockX = true;
                            SceneEditor.GizLockY = false;
                            SceneEditor.GizLockZ = false;
                        }
                        if (res.Mesh == SceneEditor.CurrentGizmo.Meshes[0])
                        {
                            SceneEditor.GizDrag = true;
                            SceneEditor.GizLockX = false;
                               SceneEditor.GizLockY = true;
                            SceneEditor.GizLockZ = false;
                        }
                        if (res.Mesh == SceneEditor.CurrentGizmo.Meshes[1])
                        {
                            SceneEditor.GizDrag = true;
                            SceneEditor.GizLockX = false;
                            SceneEditor.GizLockY = false;
                            SceneEditor.GizLockZ = true;
                        }

                    }
                    //int b = 5;
                    return;
                }
                
                else
                {
                    if (CheckIcons()) return;

                    res = global::SceneEditor.SceneEditor.EditScene.MousePick((int)SceneEditor.MouseX, (int)SceneEditor.MouseY);
                    if (res != null)
                    {
                        SceneEditor.CurrentNode = res.Node;
                        SceneEditor.CurrentGizmo.Position = SceneEditor.CurrentNode.Position;
                        SceneEditor.NodeTree.SelectedNode = FindNode(SceneEditor.CurrentNode);
                        return;
                    }
                }


            }

        }



        public static bool CheckIcons()
        {
            foreach (var spawn in SceneEditor.EditScene.Spawns)
            {

                if (SceneEditor.MouseX > spawn.DrawnX - 32 && SceneEditor.MouseY > spawn.DrawnY - 32 && SceneEditor.MouseX < spawn.DrawnX + 32 && SceneEditor.MouseY < spawn.DrawnY + 32)
                {
                    SceneEditor.CurrentNode = spawn;
                    SceneEditor.NodeTree.SelectedNode = FindNode(SceneEditor.CurrentNode);
                    return true;
                }

            }

            foreach (var light in SceneEditor.EditScene.Lights)
            {

                if ( SceneEditor.MouseX > light.DrawnX - 32 && SceneEditor.MouseY > light.DrawnY - 32 && SceneEditor.MouseX < light.DrawnX + 32 && SceneEditor.MouseY < light.DrawnY + 32)
                {
                    SceneEditor.CurrentNode = light;
                    SceneEditor.NodeTree.SelectedNode = FindNode(SceneEditor.CurrentNode);
                    return true;
                }

            }
            return false;
        }

        public static TreeNode FindNode(Vivid.Scene.Node node)
        {

            foreach (var key in SceneEditor.NodeMap.Keys)
            {

                var g_node = SceneEditor.NodeMap[key];
                if (g_node == node)
                {
                    return key;
                }

            }
            return null;
        }

        public static void Move(MouseEventArgs e)
        {

            
            MouseX = e.X;
            MouseY = e.Y;
            float mx = e.X - PX;
            float my = e.Y - PY;
            PX = e.X;
            PY = e.Y;

            if (CamDrag)
            {
                //Environment.Exit(0);

                CamPitch -= my * 0.2f;
                CamYaw -= mx * 0.2f;
                // Console.WriteLine("MX:" + mx + " MY:" + my);
                //Console.WriteLine("CAMP:" + cam_p);
            }
            if (GizDrag)
            {
                if (CurrentNode != null)
                {
                    float spd = 0.05f;
                    if (CurrentGizmo == GizmoTranslate)
                    {
                        if(Space == EditSpace.Screen)
                        {

                            if (GizLockZ)
                            {

                                Vector3 mv = EditCam.TransformVector(new Vector3(0,0,my * spd));

                                

                                CurrentNode.Position = CurrentNode.Position + mv;
                            }
                            if (GizLockX)
                            {

                                Vector3 mv = EditCam.TransformVector(new Vector3(mx*spd, 0, 0));

                                CurrentNode.Position = CurrentNode.Position + mv;

                                //CurrentNode.LocalPosition = CurrentNode.LocalPosition + new Vec3(0, my * spd, 0);
                            }
                            if (GizLockY)
                            {
                                Vector3 mv = EditCam.TransformVector(new Vector3(0,-my * spd, 0));

                                CurrentNode.Position = CurrentNode.Position + mv;
                                //CurrentNode.LocalPosition = CurrentNode.LocalPosition + new Vec3(0, 0, mx * spd);
                            }

                        }
                        else 
                        if (Space == EditSpace.Local)
                        {
                            if (GizLockZ)
                            {

                                CurrentNode.Move(0, 0, mx * spd);
                            }
                            if (GizLockX)
                            {
                                CurrentNode.Move(mx * spd, 0, 0);

                                //CurrentNode.LocalPosition = CurrentNode.LocalPosition + new Vec3(0, my * spd, 0);
                            }
                            if (GizLockY)
                            {
                                CurrentNode.Move(0, -my * spd, 0);
                                //CurrentNode.LocalPosition = CurrentNode.LocalPosition + new Vec3(0, 0, mx * spd);
                            }
                        }
                        else
                        {
                            if (GizLockY)
                            {
                                CurrentNode.Position = CurrentNode.Position + new Vector3(0, -my * spd, 0);
                            }
                            if (GizLockX)
                            {
                                CurrentNode.Position = CurrentNode.Position + new Vector3(mx * spd, 0, 0);
                            }
                            if (GizLockZ)
                            {
                                CurrentNode.Position = CurrentNode.Position + new Vector3(0, 0, mx * spd);
                            }
                        }
                        //Console.WriteLine("CN:" + CurrentNode.ToString());
                        if (CurrentNode is Vivid.Scene.SpawnPoint)
                        {

                            var start_point = CurrentNode.Position.Y + 6;
                            Ray ray = new Ray();
                            ray.Pos = CurrentNode.Position;
                            ray.Pos.Y = ray.Pos.Y + 6;
                            ray.Dir = new Vector3(0, -12, 0);
                            var res = EditScene.Raycast(ray);
                            if (res != null)
                            {
                                if (res.Hit)
                                {
                                    Vector3 pos = res.Point;
                                    pos.Y = pos.Y + 1.25f;
                                    CurrentNode.Position = pos;
                                }
                            }


                          //  Environment.Exit(1);
                           // int b = 5;

                        }
                    }
                 

                    if (CurrentGizmo == GizmoRotate)
                    {

                        if (Space == EditSpace.Local)
                        {
                            if (GizLockX)
                            {
                                CurrentNode.Turn(spd * mx, 0, 0, true);
                            }
                            if (GizLockY)
                            {
                                CurrentNode.Turn(0, spd * mx, 0, true);
                                //      CurrentNode.LocalPosition = CurrentNode.LocalPosition + new Vec3(0, my * spd, 0);
                            }
                            if (GizLockZ)
                            {
                                CurrentNode.Turn(0, 0, spd * mx, true);
                                //    CurrentNode.LocalPosition = CurrentNode.LocalPosition + new Vec3(0, 0, mx * spd);
                            }

                        }
                        else
                        {
                            if (GizLockX)
                            {
                                CurrentNode.Turn(spd * mx, 0, 0, false);
                            }
                            if (GizLockY)
                            {
                                CurrentNode.Turn(0, spd * mx, 0, false);
                                //      CurrentNode.LocalPosition = CurrentNode.LocalPosition + new Vec3(0, my * spd, 0);
                            }
                            if (GizLockZ)
                            {
                                CurrentNode.Turn(0, 0, spd * mx, false);
                                //    CurrentNode.LocalPosition = CurrentNode.LocalPosition + new Vec3(0, 0, mx * spd);
                            }
                        }

                    }
                }
            
            }
            EditCam.SetRotation(CamPitch, CamYaw, 0);
        }

        public static void Wheel(MouseEventArgs e)
        {

            if (Math.Abs(e.Delta) > 0)
            {

              //  EditCam.Move(0, 0, ((float)(e.Delta)) * 0.01f);
                return;

            }
        }

    }
}
