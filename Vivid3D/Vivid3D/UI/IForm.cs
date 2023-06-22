using OpenTK.Compute.OpenCL;
using Vivid.Maths;
using Vivid.Shaders;
using Vivid.Texture;
using OpenTK.Graphics.OpenGL;
using System.Numerics;

namespace Vivid.UI
{
    public delegate void FormAction(IForm form, object data = null);
    public delegate void FormMove(IForm form, int x, int y);
    public delegate void ActionDrop(IForm form, DragObject drag);

    public class IForm
    {
        public Position Position
        {
            get;
            set;
        }

        public Vivid.Maths.Size Size
        {
            get;
            set;
        }

        public Vivid.Maths.Size PrevSize
        {
            get;
            set;
        }

        public virtual string Text
        {
            get;
            set;
        }

        public Texture2D Image
        {
            get;
            set;
        }

        public Maths.Color Color
        {
            get; set;
        }

        public IForm Root
        {
            get;
            set;
        }

        public List<IForm> Forms
        {
            get;
            set;
        }

        public Position RenderPosition
        {
            get
            {
                Position root = new Position(0, 0);
                if (Root != null)
                {
                    if (Scroller)
                    {
                        root = Root.RenderPosition - Root.ScrollValue;
                    }
                    else
                    {
                        root = Root.RenderPosition;
                    }
                    
                }

                return root + Position;
            }
        }

        public Maths.Size ContentSize
        {
            get
            {

                int max_y = 0;
                int max_x = 0;
                foreach(var item in Forms)
                {
                    if (item.Scroller)
                    {
                        if (item.Position.y > max_y)
                        {
                            max_y = item.Position.y;
                        }
                        if (item.Position.x > max_x)
                        {
                            max_x = item.Position.x;
                        }
                    }
                }
                return new Maths.Size(max_x, max_y);

            }
        }

        public bool Scroller
        {
            get;
            set;
        }

        public event FormAction OnClick;

        public event FormAction MouseDown;

        public event FormAction OnActivated;


        public event FormMove OnMove;

        public event FormAction OnDeactivated;

        public event FormAction OnClicked;

        public event ActionDrop OnDrop;

        private Texture2D BGTex
        {
            get;
            set;
        }

        public Maths.Size MinimumSize
        {
            get;
            set;
        }

        private static Vivid.Draw.SMDrawBlur2D BlurFX
        {
            get;
            set;
        }

        public Position ScrollValue
        {
            get;
            set;
        }

        public bool Scissor
        {
            get;
            set;
        }
        
        public bool ScissorSelf
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        public bool Static
        {
            get;
            set;
        }

        public IForm ContextForm
        {
            get;
            set;
        }

        public bool DrawOutline
        {
            get;
            set;
        }

        public IForm Override
        {
            get;
            set;
        }

        public string ToolTip
        {
            get;
            set;
        }

        public bool Highlight
        {
            get;
            set;
        }

        public bool DirectKeys
        {
            get;
            set;
        }

        public IForm()
        {
            
            Override = null;
            Scroller = true;
            if (BlurFX == null)
            {
                BlurFX = new Vivid.Draw.SMDrawBlur2D();
            }
            Highlight = false;
            ToolTip = "";
            Static = false;
            Scissor = false;
            Position = new Position(0, 0);
            Size = new Maths.Size(0, 0);
            Text = string.Empty;
            ScissorSelf = false;
            Active = false;
            DirectKeys = false;
            Forms = new List<IForm>();
            BGTex = null;
            Root = null;
            DrawOutline = false;
            MinimumSize = new Maths.Size(0, 0);
            Color = new Maths.Color(1, 1, 1, 1);
            OnClick = null;
            OnClicked = null;
            OnActivated = null;
            OnDeactivated = null;
            ScrollValue = new Position(0, 0);
        }
        public void Resized(int w,int h)
        { 

            int a = 5;
        }
        public void ResizeUI(int w,int h)
        {
            float xi, yi, wi, hi;

            //xi = Position.x 


            //int b = 5;
        }

        public IForm AddForm(IForm form)
        {
            Forms.Add(form);
            form.Root = this;
            return form;
        }

        public void AddForms(params IForm[] forms)
        {
            foreach(var form in forms)
            {
                AddForm(form);
            }

        }
        public IForm SetText(string text)
        {
            Text = text;
            return this;
        }
        public void AcceptDrop(DragObject drop)
        {
            OnDrop?.Invoke(this, drop);
        }
        public IForm Set(int x,int y,int w,int h,string text = "")
        {
            return Set(new Position(x, y), new Vivid.Maths.Size(w, h), text);

        }
        public IForm Set(Position position, Vivid.Maths.Size size, string text="")
        {
            if (MinimumSize.w > 0)
            {
                if (size.w < MinimumSize.w)
                {
                    size.w = MinimumSize.w;
                }
                if (size.h < MinimumSize.h)
                {
                    size.h = MinimumSize.h;
                }
            }
            Position = position;
            Size = size;
            Text = text;

            AfterSet();
            return this;
        }

        public virtual void DragOver(IForm form,int mx,int my)
        {

        }
        public virtual void OnKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys keys)
        {

        }

        public virtual void OnKeyUp(OpenTK.Windowing.GraphicsLibraryFramework.Keys key)
        {

        }

        public virtual void OnKey(OpenTK.Windowing.GraphicsLibraryFramework.Keys key)
        {

        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnLeave()
        {
        }

        public virtual void OnActivate()
        {
        }

        public virtual void OnDeactivate()
        {
        }

        public virtual void OnMouseMove(Position position, Delta delta)
        {
        }
        public virtual void OnMouseWheelMove(OpenTK.Mathematics.Vector2 delta)
        {

        }

        public virtual void OnDoubleClick(MouseID button)
        {

        }

        public virtual void OnMouseDown(MouseID button)
        {
        }

        public virtual void OnMouseUp(MouseID button)
        {
        }

        public virtual void OnMouseDrag(Position position, Delta delta)
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnRender()
        {
        }

        public void Draw(Texture2D image, int x = -1, int y = -1, int w = -1, int h = -1, Maths.Color col = null,ShaderModule shader=null)
        {
            if (x == -1)
            {
                x = RenderPosition.x;
                y = RenderPosition.y;
                w = Size.w;
                h = Size.h;
                if (col == null)
                {
                    col = Color;
                }
            }
            else
            {
               // x = RenderPosition.x + x;
               // y = RenderPosition.y + y;
            }

            if(col == null)
            {
                col = Color;
            }
            UI.Draw.Begin();
            UI.Draw.Draw(image,new Rect(x, y, w, h), new Vivid.Maths.Color(col.r, col.g,col.b, col.a));
            UI.Draw.End(shader);
            
           
        }

        public void Update()
        {
            OnUpdate();
            foreach (var form in Forms)
            {

                if (form.Override != null)
                {
                    form.Override.Update();

                }
                else
                {
                    form.Update();

                }
            }
        }

        public void Render()
        {
           

            int rx, ry, w, h;
           // if (x == -1)
         //   {
                rx = RenderPosition.x;
                ry = RenderPosition.y;
         
            w = Size.w;
            h = Size.h;
            int ty = Vivid.App.VividApp.FrameHeight - (ry + Size.h);
            if (DrawOutline)
            {

                Draw(UI.Theme.Frame, RenderPosition.x - 2, RenderPosition.y - 2, Size.w + 4, 1, new Maths.Color(3, 3, 3, 2));
                Draw(UI.Theme.Frame, RenderPosition.x - 2, RenderPosition.y - 2, 1,Size.h+4 , new Maths.Color(3, 3, 3, 2));
                Draw(UI.Theme.Frame, RenderPosition.x - 2 + Size.w+4, RenderPosition.y - 2, 1, Size.h + 4, new Maths.Color(3, 3, 3, 2));
                Draw(UI.Theme.Frame, RenderPosition.x - 2, RenderPosition.y - 2+Size.h+4, Size.w + 4,1, new Maths.Color(3, 3, 3, 2));
            }

            if (ScissorSelf)
            {
                GL.Enable(EnableCap.ScissorTest);
                GL.Scissor(rx, ty, w, h);
            }
            OnRender();
            if (ScissorSelf)
            {
                GL.Disable(EnableCap.ScissorTest);
            }
            foreach (var form in Forms)
            {
                if (Scissor)
                {
                    GL.Enable(EnableCap.ScissorTest);
                    GL.Scissor(rx, ty, w, h);
                }

                if (form.Override != null)
                {
                    form.Override.Render();
                }
                else
                {
                    form.Render();
                }
                
                if (Scissor)
                {
                    GL.Disable(EnableCap.ScissorTest);
                }
            }
            
        }

        public virtual bool InBounds(Position pos)
        {
            if (pos.x >= RenderPosition.x && pos.y >= RenderPosition.y && pos.x <= (RenderPosition.x + Size.w) && pos.y <= (RenderPosition.y + Size.h))
            {
                return true;
            }
            return false;
        }

        public virtual void AfterSet()
        {
            
        }

        public virtual void AfterSetChildren()
        {
            foreach(var form in Forms)
            {
                form.AfterSet();
            }
        }
        public void BlurBG(float blur=0.5f)
        {
            if (BGTex == null || BGTex.Width != Size.w || BGTex.Height != Size.h) 
            {
                BGTex = new Texture2D(Size.w, Size.h);
            }
            int ty = Vivid.App.VividApp.FrameHeight - (RenderPosition.y + Size.h);

            //int sy = App.AppInfo.Height - ty;



            BlurFX.Blur = blur;
            BGTex.Copybuffer(RenderPosition.x, ty);
            Draw(BGTex,RenderPosition.x,RenderPosition.y+Size.h,Size.w,-Size.h,new Maths.Color(1,1,1,1),BlurFX);
            //UI.Draw.Begin();
            //UI.Draw.Draw(BGTex, new Rect(x, y, w, h), new Vivid.Maths.Color(col.r, col.g, col.b, col.a));
            //UI.Draw.End();



        }

        //public void ScaleColor(float scale)
        //{

        //}


        //Drag & Drop

        public void BeginDragAndDrop()
        {

        }

        public void EndDragAndDrop()
        {

        }

        //Invokes

        public void InvokeMouseDown(IForm form,object data)
        {
            MouseDown?.Invoke(form, null);
        }
        public void InvokeMove(IForm form,int x,int y)
        {
            OnMove?.Invoke(this,x, y);
        }
        public void InvokeClicked(IForm form,object data)
        {
            OnClicked?.Invoke(this, data);
        }
        public void InvokeClick(IForm form,object data)
        {
            OnClick?.Invoke(form, data);
        }
    }
}