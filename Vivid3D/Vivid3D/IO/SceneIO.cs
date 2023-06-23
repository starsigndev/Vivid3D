using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;
using Vivid.Scene;
using Vivid.NodeModules;
using Vivid.Script;

namespace Vivid.IO
{
    public enum NodeType
    {
        Node,Entity,Skeletal,Light,Camera
    }
    public class SceneIO 
    {
        public Node LoadNode(string path)
        {

            Node node;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

            node = ReadNode(r);

            fs.Close();

            return node;
        }
        public void SaveNode(string path,Node node)
        {

            FileStream fs = new FileStream(path,FileMode.Create,FileAccess.Write);
            BinaryWriter w = new BinaryWriter(fs);

            WriteNode(w, node);
            //int bb = 5;
            fs.Flush();
            fs.Close();
        }
        public Scene.Scene LoadScene(string path)
        {
            Scene.Scene scene = new Scene.Scene();

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

            scene.Root = ReadNode(r);

            int lc = r.ReadInt32();
            for(int i = 0; i < lc; i++)
            {

                scene.Lights.Add((Light)ReadNode(r));

            }

            fs.Close();

            return scene;

        }
        public void SaveScene(Scene.Scene scene,string path)
        {

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryWriter w = new BinaryWriter(fs);

            WriteNode(w, scene.Root);

            w.Write(scene.Lights.Count);
            foreach (var light in scene.Lights)
            {
                WriteNode(w,light);
            }

            fs.Flush();
            fs.Close();

        }

        public Node ReadNode(BinaryReader r)
        {
            int type = r.ReadInt32();
            switch ((NodeType)type)
            {
                case NodeType.Node:

                    Node node = new Node();
                    bool enabled = r.ReadBoolean();
                    Vector3 pos = FileHelp.ReadVec3(r);
                    Vector3 scal = FileHelp.ReadVec3(r);
                    Matrix4 rot = FileHelp.ReadMat4(r);
                    string name = r.ReadString();

                    node.Name = name;
                    node.Position = pos;
                    node.Scale = scal;
                    node.Rotation = rot;

                    ReadChildren(r, node);

                    return node;

                    break;
                case NodeType.Entity:

                    return ReadEntity(r);

                    break;
                case NodeType.Light:

                    return ReadLight(r);

                    break;
            }
            return null;
        }

        public void ReadChildren(BinaryReader r, Node node)
        {
            int nc = r.ReadInt32();
            for(int i = 0; i < nc; i++)
            {
                Node child = ReadNode(r);
                node.Nodes.Add(child);
            }
        }

        public void WriteNode(BinaryWriter w,Node node)
        {
            if (node is Entity)
            {
                WriteEntity(w, (Entity)node);
                WriteChildren(w, node);
            }
            else if (node is SkeletalEntity)
            {
                WriteSkeletal(w, (SkeletalEntity)node);
                WriteChildren(w, node);
            }
            else if (node is Light)
            {

                WriteLight(w, (Light)node);
                WriteChildren(w, node);
                
            }
             else if(node is Node)
            {
                w.Write((int)NodeType.Node);
                w.Write(node.Enabled);
                FileHelp.WriteVec3(w, node.Position);
                FileHelp.WriteVec3(w, node.Scale);
                FileHelp.WriteMat4(w, node.Rotation);
                w.Write(node.Name);

                WriteChildren(w, node);
            }
        }

        public void WriteLight(BinaryWriter w,Light light)
        {
            w.Write((int)NodeType.Light);
            w.Write(light.Enabled);
            FileHelp.WriteVec3(w, light.Position);
            FileHelp.WriteVec3(w, light.Scale);
            FileHelp.WriteMat4(w, light.Rotation);
            FileHelp.WriteVec3(w, light.Diffuse);
            FileHelp.WriteVec3(w, light.Specular);
            w.Write(light.Range);
            w.Write(light.CastShadows);
            w.Write(light.Name);
        }

        public Node ReadLight(BinaryReader r)
        {

            Light node = new Light();
            node.Enabled = r.ReadBoolean();
            node.Position = FileHelp.ReadVec3(r);
            node.Scale = FileHelp.ReadVec3(r);
            node.Rotation = FileHelp.ReadMat4(r);
            node.Diffuse = FileHelp.ReadVec3(r);
            node.Specular = FileHelp.ReadVec3(r);
            node.Range = r.ReadSingle();
            node.CastShadows = r.ReadBoolean();
            node.Name = r.ReadString();
            return node;

        }

        public void WriteEntity(BinaryWriter w,Entity entity)
        {
            w.Write((int)NodeType.Entity);
            w.Write(entity.ResourcePath);
            w.Write(entity.Enabled);
            FileHelp.WriteVec3(w, entity.Position);
            FileHelp.WriteVec3(w, entity.Scale);
            FileHelp.WriteMat4(w, entity.Rotation);
            w.Write(entity.Name);
            WriteModules(w, (Node)entity);

        }

        public void WriteModules(BinaryWriter w,Node node)
        {
            w.Write(node.Modules.Count);
            foreach(var mod in node.Modules)
            {
                w.Write(mod.ResourcePath);
                mod.SaveState(w);
            }


        }

        public Node ReadEntity(BinaryReader r)
        {

            string resource = r.ReadString();
            Entity node = Importing.Importer.ImportEntity<Entity>(resource);

            bool enabled = r.ReadBoolean();
            Vector3 pos = FileHelp.ReadVec3(r);
            Vector3 scal = FileHelp.ReadVec3(r);
            Matrix4 rot = FileHelp.ReadMat4(r);
            string name = r.ReadString();

            node.Name = name;
            node.Position = pos;
            node.Scale = scal;
            node.Rotation = rot;

            int mc = r.ReadInt32();

            for(int i = 0; i < mc; i++)
            {
                var mod = Vivid.Script.Scripting.LoadScript(r.ReadString());
                mod.LoadState(r);
                node.Modules.Add(mod);
            }

            ReadChildren(r, node);

            return node;

        }

        public void WriteSkeletal(BinaryWriter w,SkeletalEntity entity)
        {
            w.Write((int)NodeType.Skeletal);
            w.Write(entity.ResourcePath);
            w.Write(entity.Enabled);
            FileHelp.WriteVec3(w, entity.Position);
            FileHelp.WriteVec3(w, entity.Scale);
            FileHelp.WriteMat4(w, entity.Rotation);
            w.Write(entity.Name);

        }

        public void WriteChildren(BinaryWriter w, Node node)
        {
            w.Write(node.Nodes.Count);
            foreach(var snode in node.Nodes)
            {
                WriteNode(w,snode);
            }
        }

    }
}
