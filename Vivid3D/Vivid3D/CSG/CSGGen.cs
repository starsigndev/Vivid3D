using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Vivid.Scene;
using Vivid.Meshes;

namespace Vivid.CSG
{
    public class CSGGen
    {

        public enum CSGOpType
        {
            Union,
            Intersection,
            Subtraction
        }
        public Entity Perform(Entity Input,Entity Receiver,CSGOpType type)
        {
            switch (type)
            {
                case CSGOpType.Union:
                    Receiver = Union(Receiver, Input);
                    break;
                case CSGOpType.Intersection:
                    Receiver = Intersection(Receiver, Input);
                    break;
                case CSGOpType.Subtraction:
                    Receiver = Subtraction(Receiver, Input);
                    break;
            }
            return Receiver;
        }

        public Entity Union(Entity recevier,Entity input)
        {

            var tris1 = recevier.Meshes[0].Triangles;
            var tris2 = input.Meshes[0].Triangles;
            var verts1 = recevier.Meshes[0].Vertices;
            var verts2 = recevier.Meshes[0].Vertices;
            //

            int vert_base = recevier.Meshes[0].Vertices.Count;

            var tris3 = new List<Triangle>();

            foreach(var t in tris1)
            {
                tris3.Add(t);
            }

            foreach(var t in tris2.ToArray())
            {
                var new_tri = new Triangle();
                new_tri.V0 = vert_base + t.V0;
                new_tri.V1 = vert_base + t.V1;
                new_tri.V2 = vert_base + t.V2;
                tris3.Add(new_tri);

            }

            List<Vertex> vertices = new List<Vertex>();

            foreach(var v in verts1)
            {

                var vpos = v.Position;
                var vnorm = v.Position;

                vpos = recevier.TransformPosition(vpos);
                vnorm = recevier.TransformVector(vnorm);

                Vertex new_vert = new Vertex();

                new_vert.Position = vpos;
                new_vert.Normal = vnorm;
                new_vert.Color = v.Color;
                new_vert.TexCoord = v.TexCoord;
                new_vert.BiNormal = v.BiNormal;
                new_vert.Tangent = v.Tangent;

                vertices.Add(new_vert);
            }

            foreach (var v in verts2)
            {

                var vpos = v.Position;
                var vnorm = v.Position;

                vpos = input.TransformPosition(vpos);
                vnorm = input.TransformVector(vnorm);

                Vertex new_vert = new Vertex();

                new_vert.Position = vpos;
                new_vert.Normal = vnorm;
                new_vert.Color = v.Color;
                new_vert.TexCoord = v.TexCoord;
                new_vert.BiNormal = v.BiNormal;
                new_vert.Tangent = v.Tangent;

                vertices.Add(new_vert);
                //vertices.Add(v);

            }

            Entity res = new Entity();
            res.Position = Vector3.Zero;
            res.Rotation = Matrix4.Identity;

            Meshes.Mesh mesh = new Meshes.Mesh(res);

            mesh.Vertices = vertices;
            mesh.Triangles = tris3;

            mesh.CreateBuffers();



            return res;
        }

        public Entity Intersection(Entity receiver,Entity input)
        {

            return null;
        }

        public Entity Subtraction(Entity receiver,Entity input)
        {

            return null;
        }


    }
}
