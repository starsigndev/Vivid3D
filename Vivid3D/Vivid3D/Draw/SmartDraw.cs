using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Vivid.App;
using Vivid.Texture;

namespace Vivid.Draw
{
    public enum BlendMode
    {
        Solid, Mod, Additive, Mod2X, Mode4X, Alpha
    }

    public class SmartDraw
    {
        public SMDraw2D DrawSM;

        public List<DrawList> Lists
        {
            get;
            set;
        }

        public float DrawZ
        {
            get;
            set;
        }

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

        public BlendMode Blend
        {
            get;
            set;
        }

        public SmartDraw()
        {
            DrawSM = new SMDraw2D();
            Lists = new List<DrawList>();
            DrawZ = 0.01f;
            Blend = BlendMode.Solid;
        }

        public void SoloDraw(Texture2D tex, Vivid.Maths.Rect rect, Vivid.Maths.Color color, bool flip_uv = false)
        {
            Begin();
            Draw(tex, rect, color, flip_uv);
            End();
        }

        public void Draw(Texture2D tex, Vivid.Maths.Rect rect, Vivid.Maths.Color color, bool flip_uv = false)
        {
            DrawList list = GetList(tex);

            DrawInfo info = new DrawInfo();

            info.X[0] = rect.x;
            info.Y[0] = rect.y;

            info.X[1] = rect.x + rect.w;
            info.Y[1] = rect.y;

            info.X[2] = rect.x + rect.w;
            info.Y[2] = rect.y + rect.h;

            info.X[3] = rect.x;
            info.Y[3] = rect.y + rect.h;

            info.FlipUV = flip_uv;

            info.Texture[0] = tex;

            info.Color = color;

            info.Z = DrawZ;

            DrawZ += 0.0002f;

            list.Add(info);

            //  gem_DrawTexture(handle, tex.CObj, x, y, w, h, r, g, b, a,fz);
        }

        public DrawList GetList(params Texture2D[] texture)
        {
            foreach (var dlist in Lists)
            {
                if (dlist.Texture[0] == texture[0])
                {
                    return dlist;
                }
            }

            DrawList list = new DrawList();

            for (int i = 0; i < texture.Length; i++)
            {
                list.Texture[i] = texture[i];
            }

            Lists.Add(list);

            return list;
        }

        public void SetMode(int mode)
        {
        }

        public void Begin()
        {
            Lists = new List<DrawList>();
            DrawZ = 0f;
            //gem_DrawBegin(handle);
        }

        public void GenerateBuffers()
        {
            if (VertexArray == VertexArrayHandle.Zero)
            {
                VertexArray = GL.GenVertexArray();
                Buffer = GL.GenBuffer();
                IndexBuffer = GL.GenBuffer();
            }
        }

        public void End()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
          //  GL.Disable(EnableCap.DepthTest);
            GL.Viewport(0, 0, VividApp.FrameWidth, VividApp.FrameHeight);
           // GL.Disable(EnableCap.ScissorTest);

            switch (Blend)
            {
                case BlendMode.Solid:
                    GL.Disable(EnableCap.Blend);
                    break;

                case BlendMode.Additive:
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
                    break;

                case BlendMode.Alpha:
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                    break;
            }
            GenerateBuffers();
            foreach (var list in Lists)
            {
                GL.BindVertexArray(VertexArray);
                GL.BindBuffer(BufferTargetARB.ArrayBuffer, Buffer);

                float[] data = list.GenerateVertexData();
                uint[] index_data = list.GenerateIndexData();

                GL.BufferData(BufferTargetARB.ArrayBuffer, data, BufferUsageARB.StaticDraw);

                GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, IndexBuffer);
                GL.BufferData(BufferTargetARB.ElementArrayBuffer, index_data, BufferUsageARB.StaticDraw);

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 9 * 4, 0);

                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 9 * 4, 3 * 4);

                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, 9 * 4, 5 * 4);

                DrawSM.Bind();
                list.Texture[0].Bind(0);

                GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, IndexBuffer);
                GL.DrawElements(PrimitiveType.Triangles, list.InfoList.Count * 6, DrawElementsType.UnsignedInt, 0);

                list.Texture[0].Unbind(0);

                DrawSM.Unbind();
                //   gem_DrawEnd(handle);
                //  GemBridge.gem_ClearZBuffer();
            }
        }
    }
}