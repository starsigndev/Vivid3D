using Vivid.Maths;
using Vivid.Texture;

namespace Vivid.UI
{
    public delegate void Action(IForm form, object data = null);

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

        public string Text
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
                    root = Root.RenderPosition;
                }

                return root + Position;
            }
        }

        public Action OnClick
        {
            get;
            set;
        }

        public Action OnActivated
        {
            get;
            set;
        }

        public Action OnDeactivated
        {
            get;
            set;
        }

        public Action OnClicked
        {
            get;
            set;
        }

        public IForm()
        {
            Position = new Position(0, 0);
            Size = new Maths.Size(0, 0);
            Text = string.Empty;
            Forms = new List<IForm>();
            Root = null;
            Color = new Maths.Color(1, 1, 1, 1);
            OnClick = null;
            OnClicked = null;
            OnActivated = null;
            OnDeactivated = null;
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

        public IForm Set(Position position, Vivid.Maths.Size size, string text)
        {
            Position = position;
            Size = size;
            Text = text;
            AfterSet();
            return this;
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

        public void Draw(Texture2D image, int x = -1, int y = -1, int w = -1, int h = -1, Maths.Color col = null)
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
                x = RenderPosition.x + x;
                y = RenderPosition.y + y;
            }

            if(col == null)
            {
                col = Color;
            }
            UI.Draw.Begin();
            UI.Draw.Draw(image,new Rect(x, y, w, h), new Vivid.Maths.Color(col.r, col.g,col.b, col.a));
            UI.Draw.End();
            
        }

        public void Update()
        {
            OnUpdate();
            foreach (var form in Forms)
            {
                form.Update();
            }
        }

        public void Render()
        {
            OnRender();
            foreach (var form in Forms)
            {
                form.Render();
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
        //public void ScaleColor(float scale)
        //{

        //}

    }
}