using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Mesh;
using OpenTK.Graphics;
using OpenTK.Mathematics;

namespace Vivid3D.Features
{
    public class Grid
    {
        public static MeshLines CreateGrid()
        {

            MeshLines mesh = new MeshLines();

            float line_size = 0.065f;
            //   Vivid.Meshes.Mesh mesh = new Vivid.Meshes.Mesh(null);

            Vector4 col = new Vector4(0, 1, 0.5f, 1.0f);
            int vv = 0;
            int vc = 0;
            float w = 0.095f;
            for (int x = -80; x < 80; x++)
            {

                mesh.AddLine(new Vector3(x, 0, -80), new Vector3(x, 0, 80), new Vector4(0, 1, 1, 1));

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

                mesh.AddLine(new Vector3(-80, 0, z), new Vector3(80, 0, z), new Vector4(0, 0.5f, 1.0f, 1));
                //Vector3 p1, p2, p3, p4;




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
