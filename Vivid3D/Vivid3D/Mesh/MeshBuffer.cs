using Vivid.Meshes;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Vivid.Mesh
{
    public class MeshBuffer
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

        public bool SetBuffer(Vivid.Meshes.Mesh mesh)
        {

            VertexArray = GL.GenVertexArray();
            Buffer = GL.GenBuffer();
            IndexBuffer = GL.GenBuffer();

            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, Buffer);

            var data = mesh.GenerateVertexData();
            var index_data = mesh.GenerateIndexData();

            IndexCount = index_data.Length;

            GL.BufferData(BufferTargetARB.ArrayBuffer, data, BufferUsageARB.StaticDraw);


            //position
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 27 * 4, 0);

            //color
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 27 * 4, 3 * 4);

            //tex uv
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 27 * 4, 7 * 4);

            //normal
            GL.EnableVertexAttribArray(3);
            GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, 27 * 4, 10 * 4);

            //bi-normal
            GL.EnableVertexAttribArray(4);
            GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, 27 * 4, 13 * 4);

            //tangent
            GL.EnableVertexAttribArray(5);
            GL.VertexAttribPointer(5, 3, VertexAttribPointerType.Float, false, 27 * 4, 16 * 4);

            //bones
            GL.EnableVertexAttribArray(6);
            GL.VertexAttribPointer(6, 4, VertexAttribPointerType.Float, false, 27 * 4, 19 * 4);

            //weights
            GL.EnableVertexAttribArray(7);
            GL.VertexAttribPointer(7, 4, VertexAttribPointerType.Float, false, 27 * 4, 23 * 4);

            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, IndexBuffer);
            GL.BufferData(BufferTargetARB.ElementArrayBuffer, index_data, BufferUsageARB.StaticDraw);

            GL.BindVertexArray(VertexArrayHandle.Zero);
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, BufferHandle.Zero);
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer,BufferHandle.Zero);

            return true;

        }

        public void Render()
        {

            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, Buffer);
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, IndexBuffer);
            GL.DrawElements(PrimitiveType.Triangles, IndexCount, DrawElementsType.UnsignedInt, 0);
            //GL.BindVertexArray(VertexArrayHandle.Zero);
           // GL.BindBuffer(BufferTargetARB.ArrayBuffer,BufferHandle.Zero);
          //  GL.BindBuffer(BufferTargetARB.ElementArrayBuffer,BufferHandle.Zero);


        }

    }
}
