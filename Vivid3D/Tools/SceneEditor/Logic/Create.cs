using Vivid.Content;
using Vivid.Scene;
using Vivid.Importing;
using Vivid.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using static Editor.SceneEditor;
namespace Editor.Logic
{
    public class Create
    {
        public static void CreateCube()
        {

            var cube = Content.GlobalFindItem("cube.fbx");
            var ent = Importer.ImportEntity<Entity>(cube.GetStream());
            EditScene.AddNode(ent);
        }
        public static void CreatePlane()
        {
            var cube = Content.GlobalFindItem("plane.fbx");
            var ent = Importer.ImportEntity<Entity>(cube.GetStream());
            EditScene.AddNode(ent);
        }
        public static void CreateSphere()
        {
            var cube = Content.GlobalFindItem("sphere.fbx");
            var ent = Importer.ImportEntity<Entity>(cube.GetStream());
            EditScene.AddNode(ent);
        }
        public static void CreateCylinder()
        {

            var cube = Content.GlobalFindItem("cylinder.fbx");
            var ent = Importer.ImportEntity<Entity>(cube.GetStream());
            EditScene.AddNode(ent);
        }
        public static void CreateSpawnPoint()
        {
            SpawnPoint spawn = new SpawnPoint();
            spawn.Position = EditScene.MainCamera.TransformPosition(new Vector3(0, 0, 5.0f));
            EditScene.AddNode(spawn);
            spawn.Name = "Spawn";
        }
        public static void CreatePointLight()
        {

            var new_light = new Vivid.Scene.Light();
            new_light.Range = 50;
            EditScene.Lights.Add(new_light);
            EditScene.Root.AddNode(new_light);
            new_light.Position = EditScene.MainCamera.TransformPosition(new Vector3(0, 0, -5.0f));
            new_light.Name = "Light " + EditScene.Lights.Count;

        }

        public static void CreateSpotLight()
        {
            var new_light = new Light();
            new_light.Range = 50;
            EditScene.Lights.Add(new_light);
            EditScene.Root.AddNode(new_light);
            new_light.Type = LightType.Spot;
            new_light.Position = EditScene.MainCamera.TransformPosition(new Vector3(0, 0, -5.0f));
            new_light.Rotation = EditScene.MainCamera.Rotation;
            new_light.Name = "Light " + EditScene.Lights.Count;
        }

        public static void CreateDirLight()
        {
            var new_light = new Light();
            new_light.Range = 50;
            EditScene.Lights.Add(new_light);
            EditScene.Root.AddNode(new_light);
            new_light.Type = LightType.Directional;
            new_light.Name = "Light " + EditScene.Lights.Count;
            new_light.Position = EditScene.MainCamera.TransformPosition(new Vector3(0, 0, -5.0f));
        }

    }
}
