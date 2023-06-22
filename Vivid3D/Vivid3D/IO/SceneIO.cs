using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Scene;

namespace Vivid.IO
{
    public enum NodeType
    {
        Node,Entity,Skeletal,Light,Camera
    }
    public class SceneIO 
    {

        public void SaveScene(Scene.Scene scene,string path)
        {

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryWriter w = new BinaryWriter(fs);

            WriteNode(w, scene.Root);

            fs.Flush();
            fs.Close();

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
        }
        public void WriteEntity(BinaryWriter w,Entity entity)
        {
            w.Write((int)NodeType.Entity);
            w.Write(entity.Enabled);
            FileHelp.WriteVec3(w, entity.Position);
            FileHelp.WriteVec3(w, entity.Scale);
            FileHelp.WriteMat4(w, entity.Rotation);
            w.Write(entity.ResourcePath);
        }

        public void WriteSkeletal(BinaryWriter w,SkeletalEntity entity)
        {
            w.Write((int)NodeType.Skeletal);
            w.Write(entity.Enabled);
            FileHelp.WriteVec3(w, entity.Position);
            FileHelp.WriteVec3(w, entity.Scale);
            FileHelp.WriteMat4(w, entity.Rotation);
            w.Write(entity.ResourcePath);
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
