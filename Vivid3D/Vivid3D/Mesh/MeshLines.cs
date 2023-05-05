using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Vivid.Meshes;

namespace Vivid.Mesh
{
    public class MeshLines
    {
        public VertexArrayHandle VertexArray
        {
            get;
            set;
        }

        public BufferHandle Buffer
        {
            get;
            set;
        }

        public BufferHandle IndexBuffer
        {
            get;
            set;
        }

        public int IndexCount
        {
            get;
            set;
        }

        public List<LineVertex> Vertices
        {
            get;
            set;
        }

        public List<Line> Lines
        {
            get;
            set;
        }

        public MeshLines()
        {
            Vertices = new List<LineVertex>();
            Lines = new List<Line>();
        }

        public void New()
        {
            Vertices = new List<LineVertex>();
            Lines = new List<Line>();
        }
        public void AddLine(Vector3 p1, Vector3 p2, Vector4 col)
        {
            LineVertex v0;
            LineVertex v1;

            v0.Position = p1;// new dpos3(p1.x, p1.y, p1.z);
            v0.Color = col;// new dpos4(col.x, col.y, col.z, col.w);

            v1.Position = p2;// new dpos3(p2.x, p2.y, p2.z);
            v1.Color = col;// new dpos4(col.x, col.y, col.z, col.w);

            Vertices.AddRange(new LineVertex[] { v0, v1 });

            Line l1;

            l1.v0 = Vertices.Count - 2;
            l1.v1 = Vertices.Count - 1;

            Lines.Add(l1);
        }

        public void AddBox(Vector3 min, Vector3 max, Vector4 col)
        {
            Vector3 p1, p2, p3, p4;
            Vector3 p5, p6, p7, p8;

            p1 = new Vector3(min.X, min.Y, min.Z);
            p2 = new Vector3(max.X, min.Y, min.Z);
            p3 = new Vector3(max.X, min.Y, max.Z);
            p4 = new Vector3(min.X, min.Y, max.Z);

            p5 = new Vector3(min.X, max.Y, min.Z);
            p6 = new Vector3(max.X, max.Y, min.Z);
            p7 = new Vector3(max.X, max.Y, max.Z);
            p8 = new Vector3(min.X, max.Y, max.Z);

            AddLine(p1, p2, col);
            AddLine(p2, p3, col);
            AddLine(p3, p4, col);
            AddLine(p4, p1, col);

            AddLine(p5, p6, col);
            AddLine(p6, p7, col);
            AddLine(p7, p8, col);
            AddLine(p8, p5, col);

            AddLine(p1, p5, col);
            AddLine(p2, p6, col);
            AddLine(p3, p7, col);
            AddLine(p4, p8, col);
        }

        public float[] GenerateVertexData()
        {
            float[] data = new float[Vertices.Count * (3 + 4)];

            int loc = 0;
            foreach (var vertex in Vertices)
            {
                data[loc++] = vertex.Position.X;
                data[loc++] = vertex.Position.Y;
                data[loc++] = vertex.Position.Z;

                data[loc++] = vertex.Color.X;
                data[loc++] = vertex.Color.Y;
                data[loc++] = vertex.Color.Z;
                data[loc++] = vertex.Color.W;
            }
            return data;
        }

        public uint[] GenerateIndexData()
        {
            uint[] data = new uint[Lines.Count * 2];
            int loc = 0;
            foreach (var line in Lines)
            {
                data[loc++] = (uint)line.v0;
                data[loc++] = (uint)line.v1;
            }
            return data;
        }

        public void CreateBuffers()
        {
            VertexArray = GL.GenVertexArray();
            Buffer = GL.GenBuffer();
            IndexBuffer = GL.GenBuffer();

            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, Buffer);

            var data = GenerateVertexData();
            var index_data = GenerateIndexData();

            IndexCount = index_data.Length;

            GL.BufferData(BufferTargetARB.ArrayBuffer, data, BufferUsageARB.StaticDraw);

            //position
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 7 * 4, 0);

            //color
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 7 * 4, 3 * 4);

            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, IndexBuffer);
            GL.BufferData(BufferTargetARB.ElementArrayBuffer, index_data, BufferUsageARB.StaticDraw);

            GL.BindVertexArray(VertexArrayHandle.Zero);
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, BufferHandle.Zero);
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, BufferHandle.Zero);
            IndexCount = Lines.Count * 2;
        }

        public void Render()
        {
            GL.LineWidth(305);
            GL.Enable(EnableCap.LineSmooth);
            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, Buffer);
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, IndexBuffer);
            GL.DrawElements(PrimitiveType.Lines, IndexCount, DrawElementsType.UnsignedInt, 0);
        }

        public IntPtr MeshBuffer;
    }
}