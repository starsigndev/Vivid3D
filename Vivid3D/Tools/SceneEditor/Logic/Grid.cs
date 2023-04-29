
using Vivid.Maths;
using Vivid.Mesh;
using Vivid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using static SceneEditor.SceneEditor;
using Vivid.Meshes;

namespace SceneEditor.Logic
{
    public class Grid
    {
        public static Mesh CreateGrid()
        {
            float line_size = 0.065f;
            Vivid.Meshes.Mesh mesh = new Vivid.Meshes.Mesh(null);

            Vector4 col = new Vector4(0, 1, 0.5f, 1.0f);
            int vv = 0;
            int vc = 0;
            float w = 0.095f;
            for (int x = -80; x < 80; x++)
            {

                Vector3 p1, p2, p3, p4;
                p1 = new Vector3(x, 0, -80);
                p2 = new Vector3(x +w, 0, -80);
                p3 = new Vector3(x+w, 0, 80);
                p4 = new Vector3(x, 0, 80);

                Vertex v1, v2, v3, v4;
                
                v1 = new Vertex();
                v2 = new Vertex();
                v3 = new Vertex();
                v4 = new Vertex();

                v1.Position = p1;
                v2.Position = p2;
                v3.Position = p3;
                v4.Position = p4;

                mesh.AddVertices(v1, v2, v3, v4);

                Triangle t1, t2;

                t1 = new Triangle();
                t2 = new Triangle();

                t1.V0 = vc;
                t1.V1 = vc + 1;
                t1.V2 = vc + 2;

                t2.V0 = vc + 2;
                t2.V1 = vc + 3;
                t2.V2 = vc;

                mesh.AddTriangles(t1, t2);



                //grid_Mesh.AddLine(p1, p2, col);
                vc += 4;

            }
         
            //
            //EditScene.MeshLines.Add(mesh);
           // return;
            // vv = 0;
            col = new Vector4(0, 1.0f, 1.0f, 1.0f);
            for (int z = -80; z < 80; z++)
            {
                Vector3 p1, p2, p3, p4;
                p1 = new Vector3(-80, 0, z);
                p2 = new Vector3(-80, 0, z+w);
                p3 = new Vector3(80, 0, z+w);
                p4 = new Vector3(80, 0, z);

                Vertex v1, v2, v3, v4;

                v1 = new Vertex();
                v2 = new Vertex();
                v3 = new Vertex();
                v4 = new Vertex();

                v1.Position = p1;
                v2.Position = p2;
                v3.Position = p3;
                v4.Position = p4;

                mesh.AddVertices(v1, v2, v3, v4);

                Triangle t1, t2;

                t1 = new Triangle();
                t2 = new Triangle();

                t1.V0 = vc;
                t1.V1 = vc + 1;
                t1.V2 = vc + 2;

                t2.V0 = vc + 2;
                t2.V1 = vc + 3;
                t2.V2 = vc;

                mesh.AddTriangles(t1, t2);



                //grid_Mesh.AddLine(p1, p2, col);
                vc += 4;

            }

            mesh.CreateBuffers();
            return mesh;

            //RenderGlobals.MeshRenderer = GemBridge.gem_CreateMeshRenderer();
            //    Grid.CreateBuffers();
            //grid_Mesh.CreateBuffers();
            //EditScene.MeshLines.Add(grid_Mesh);
        }

    }
}
