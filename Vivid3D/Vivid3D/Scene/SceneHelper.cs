using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Meshes;
using OpenTK.Mathematics;
namespace Vivid.Scene
{
    public class SceneHelper
    {

        public static Vivid.Meshes.Mesh BoundsToMesh(BoundingBox box,Entity owner)
        {

            Meshes.Mesh mesh = new Meshes.Mesh(owner);

            Vector3 min, max;

            min = box.Min;
            max = box.Max;

            Vector3[] vertices = new Vector3[8];

            vertices[0] = new Vector3(min.X, min.Y, min.Z);
            vertices[1] = new Vector3(max.X, min.Y, min.Z);
            vertices[2] = new Vector3(max.X, min.Y, max.Z);
            vertices[3] = new Vector3(min.X, min.Y, max.Z);
            vertices[4] = new Vector3(min.X, max.Y, min.Z);
            vertices[5] = new Vector3(max.X, max.Y, min.Z);
            vertices[6] = new Vector3(max.X, max.Y, max.Z);
            vertices[7] = new Vector3(min.X, max.Y, max.Z);

            for(int i = 0; i < 8; i++)
            {
                Vertex v = new Vertex();
                v.Position = vertices[i];
                mesh.AddVertex(v, false);
            }
            

            int[] triangles = new int[36];

            // Front
            triangles[0] = 0;
            triangles[1] = 4;
            triangles[2] = 5;

            triangles[3] = 0;
            triangles[4] = 5;
            triangles[5] = 1;

            // Right
            triangles[6] = 1;
            triangles[7] = 5;
            triangles[8] = 6;

            triangles[9] = 1;
            triangles[10] = 6;
            triangles[11] = 2;

            // Back
            triangles[12] = 2;
            triangles[13] = 6;
            triangles[14] = 7;

            triangles[15] = 2;
            triangles[16] = 7;
            triangles[17] = 3;

            // Left
            triangles[18] = 3;
            triangles[19] = 7;
            triangles[20] = 4;

            triangles[21] = 3;
            triangles[22] = 4;
            triangles[23] = 0;

            // Top
            triangles[24] = 4;
            triangles[25] = 7;
            triangles[26] = 6;

            triangles[27] = 4;
            triangles[28] = 6;
            triangles[29] = 5;

            // Bottom
            triangles[30] = 0;
            triangles[31] = 1;
            triangles[32] = 2;

            triangles[33] = 0;
            triangles[34] = 2;
            triangles[35] = 3;

            for(int i=0;i<triangles.Length;i+=3)
            {
                Triangle t = new Triangle();
                t.V0 = triangles[i];
                t.V1 = triangles[i + 1];
                t.V2 = triangles[i + 2];
                mesh.AddTriangle(t);
                t.V1 = t.V2;
                t.V2 = triangles[i + 1];
                mesh.AddTriangle(t);
            }

            mesh.CreateBuffers();

            return mesh;

        }

    }
}
