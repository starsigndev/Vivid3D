using Assimp.Configs;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;

namespace Vivid.UI.Forms
{
    public enum TextTag
    {
        Color,Size,Font,Link,Image
    }
    public class ITextArea : IForm
    {
        public override string Text
        {
            get
            {
                return Lines[EditY];
            }
            set
            {
                if(Lines == null)
                {
                    return;
                }
                if (Lines.Length < EditY)
                {
                    return;
                }
                Lines[EditY] = value;
            }
        }
        
            public int EditX
        {
            get;set;
        }
        public int EditY
        {
            get;
            set;
        }

        public int TextStartX
        {
            get;
            set;
        }

        public int TextStartY
        {
            get;
            set;
        }

        public string[] Lines
        {
            get;
            set;
        }

        private bool CursorOn
        {
            get;
            set;
        }

        public int CursorBlinkInterval
        {
            get;
            set;
        }

        private int NextBlink
        {
            get; set;
        }

        private bool ShiftDown
        {
            get;
            set;
        }

        public IVerticalScroller Scroller
        {
            get;
            set;
        }

        public IHorizontalScroller HScroller
        {
            get;
            set;
        }

        public bool CanEdit
        {
            get;
            set;
        }

        public ITextArea(bool scrollers=true)
        {

            CanEdit = false;
            EditX = EditY = 0;
            TextStartX = 0;
            TextStartY = 0;
            Lines = new string[1];
            ScissorSelf = true;
            ShiftDown = false;
            CursorBlinkInterval = 500;
            NextBlink = Environment.TickCount + CursorBlinkInterval;
            Scroller = new IVerticalScroller();
            HScroller = new IHorizontalScroller();
            if (scrollers)
            {
                AddForm(Scroller);
                AddForm(HScroller);
            }
            Scroller.MaxValue = 20;
            HScroller.MaxValue = 20;
            HScroller.OnMove += (form, x, y) =>
            {
                x = x / 100;
                TextStartX = x;
                EditX = x;
                
               

            };
            Scroller.OnMove += (form, x, y) =>
            {
            
                y = y / 100;
                TextStartY = y;
                if (TextStartY > Lines.Length - 1)
                {
                    TextStartY = Lines.Length - 1;
                }
                if(EditY<TextStartY)
                {
                    EditY = TextStartY;
                }
                EditY = TextStartY;
            };
        }

        public void AddLine(string text)
        {

            string[] new_lines = new string[Lines.Length + 1];
            for(int i = 0; i < Lines.Length; i++)
            {
                new_lines[i] = Lines[i];
            }

            new_lines[Lines.Length] = text;
            Lines = new_lines;
            UpdateScrollers();

        }

        public override void AfterSet()
        {
            //base.AfterSet();
            Scroller.Set(Size.w, 0, 12, Size.h, "");
            HScroller.Set(0, Size.h, Size.w, 12, "");
        }
        public override void OnKey(Keys key)
        {
            if (!CanEdit) return;
            //base.OnKey(key);
            switch (key)
            {
                case Keys.Left:
                    EditX--;
                    if (EditX < 0)
                    {
                        EditX = 0;
                    }
                    if (EditX < TextStartX)
                    {
                        TextStartX--;
                    }

                    return;
                    break;
                case Keys.Right:
                    EditX++;
                    if (EditX > Text.Length)
                    {
                        EditX = Text.Length;
                    }
                    return;
                    break;
                case Keys.Up:
                    EditY--;
                    if (EditY < 0) EditY = 0;
                    if (EditX > Lines[EditY].Length-1)
                    {
                        EditX = Lines[EditY].Length-1;
                    }
                    return;
                    break;
                case Keys.Down:
                    EditY++;
                    if (EditY > Lines.Length - 1)
                    {
                        EditY = Lines.Length - 1;
                    }
                    if (EditX > Lines[EditY].Length-1)
                    {
                        EditX = Lines[EditY].Length-1;
                    }
                    return;
                    break;
                case Keys.Backspace:
                    Backspace();
                    return;
                    break;
                case Keys.Delete:
                    Delete();
                    return;
                    break;

            }
            string chr = "";
            chr = KeyToChr(key);
            InsertChr(chr);
            //Text = Text + chr;
            //            EditX++;


        }
        public string KeyToChr(Keys key)
        {
            string chr = "";
            switch (key)
            {
                case Keys.A:
                    chr = "a";
                    break;
                case Keys.B:
                    chr = "b";
                    break;
                case Keys.C:
                    chr = "c";
                    break;
                case Keys.D:
                    chr = "d";
                    break;
                case Keys.E:
                    chr = "e";
                    break;
                case Keys.F:
                    chr = "f";
                    break;
                case Keys.G:
                    chr = "g";
                    break;
                case Keys.H:
                    chr = "h";
                    break;
                case Keys.I:
                    chr = "i";
                    break;
                case Keys.J:
                    chr = "j";
                    break;
                case Keys.K:
                    chr = "k";
                    break;
                case Keys.L:
                    chr = "l";
                    break;
                case Keys.M:
                    chr = "m";
                    break;
                case Keys.N:
                    chr = "n";
                    break;
                case Keys.O:
                    chr = "o";
                    break;
                case Keys.P:
                    chr = "p";
                    break;
                case Keys.Q:
                    chr = "q";
                    break;
                case Keys.R:
                    chr = "r";
                    break;
                case Keys.S:
                    chr = "s";
                    break;
                case Keys.T:
                    chr = "t";
                    break;
                case Keys.U:
                    chr = "u";
                    break;
                case Keys.V:
                    chr = "v";
                    break;
                case Keys.W:
                    chr = "w";
                    break;
                case Keys.X:
                    chr = "x";
                    break;
                case Keys.Y:
                    chr = "y";
                    break;
                case Keys.Z:
                    chr = "z";
                    break;
                case Keys.Space:
                    chr = " ";
                    break;
                case Keys.D0:
                    chr = "0";
                    break;
                case Keys.D1:
                    chr = "1";
                    break;
                case Keys.D2:
                    chr = "2";
                    break;
                case Keys.D3:
                    chr = "3";
                    break;
                case Keys.D4:
                    chr = "4";
                    break;
                case Keys.D5:
                    chr = "5";
                    break;
                case Keys.D6:
                    chr = "6";
                    break;
                case Keys.D7:
                    chr = "7";
                    break;
                case Keys.D8:
                    chr = "8";
                    break;
                case Keys.D9:
                    chr = "9";
                    break;
                case Keys.Comma:
                    chr = ",";
                    break;
                case Keys.Period:
                    chr = ".";
                    break;
                case Keys.Slash:
                    chr = "/";
                    break;
                case Keys.LeftBracket:
                    chr = "[";
                    break;
                case Keys.RightBracket:
                    chr = "]";
                    break;
                case Keys.Semicolon:
                    chr = ";";
                    break;
                case Keys.Apostrophe:
                    chr = "'";
                    break;
                case Keys.Backslash:
                    chr = "\\";
                    break;
                case Keys.Minus:
                    chr = "-";
                    break;
                case Keys.Equal:
                    chr = "=";
                    break;
                default:
                    chr = "";
                    break;
            }

            if (ShiftDown)
            {
                switch (chr)
                {
                    case "-":
                        chr = "_";
                        break;
                    case "=":
                        chr = "+";
                        break;
                    case "\\":
                        chr = "|";
                        break;
                    case "'":
                        chr = "\"";
                        break;
                    case ";":
                        chr = ":";
                        break;
                    case "[":
                        chr = "{";
                        break;
                    case "]":
                        chr = "}";
                        break;
                    case ",":
                        chr = "<";
                        break;
                    case ".":
                        chr = ">;";
                        break;
                    case "/":
                        chr = "?";
                        break;
                    case "0":
                        chr = ")";
                        break;
                    case "1":
                        chr = "!";
                        break;
                    case "2":
                        chr = "@";
                        break;
                    case "3":
                        chr = "#";
                        break;
                    case "4":
                        chr = "$";
                        break;
                    case "5":
                        chr = "%";
                        break;
                    case "6":
                        chr = "^";
                        break;

                    case "7":
                        chr = "&";
                        break;
                    case "8":
                        chr = "*";
                        break;
                    case "9":
                        chr = "(";
                        break;
                    default:
                        chr = chr.ToUpper();
                        break;

                }

            }

            return chr;
        }

        private void Delete()
        {
            if (EditX == 0)
            {
                if (Text.Length > 1)
                {
                    Text = Text.Substring(1);
                }
                else if (Text.Length == 1)
                {
                    Text = "";
                }
                return;
            }
            if (EditX == Text.Length)
            {
                return;
            }
            if (EditX > 0)
            {
                string t1 = Text.Substring(EditX + 1);
                Text = Text.Substring(0, EditX) + t1;
                return;


            }

        }
        private void Backspace()
        {

            //Console.Write("BS: EX:" + EditX + " TL:" + Text.Length);
            if (EditX == 0) return;
            if (EditX == Text.Length)
            {
                Text = Text.Substring(0, Text.Length - 1);
                EditX--;
                if (EditX < TextStartX)
                {
                    TextStartX--;
                    TextStartX--;
                    if (TextStartX < 0)
                    {
                        TextStartX = 0;
                    }
                    // EditX--;
                    //TextStart--;

                }

                return;
            }

            if (EditX > 0)
            {
                string t1 = Text.Substring(0, EditX - 1);
                Text = t1 + Text.Substring(EditX);
                EditX--;
                if (EditX < TextStartX)
                {
                    TextStartX--;
                    //TextStart--;

                }

                return;
            }
        }

        private void InsertChr(string chr)
        {
            if (EditX == Text.Length)
            {
                Text = Text + chr;
                EditX = EditX + 1;
                return;
            }
            if (EditX == 0)
            {
                Text = chr + Text;
                EditX++;
                return;
            }
            if (EditX < 0)
            {
                EditX = 0;
                Text = chr;
                return;
            }
            if (EditX < Text.Length)
            {

                string t1 = Text.Substring(0, EditX);
                t1 = t1 + chr;
                Text = t1 + Text.Substring(EditX);
                EditX++;
                return;

            }
        }


        public override void OnKeyDown(Keys keys)
        {
            //base.OnKeyDown(keys);
            //            Console.WriteLine("Key:" + keys.ToString());

            if (keys == Keys.LeftShift)
            {
                ShiftDown = true;
            }

        }

        public override void OnKeyUp(Keys key)
        {
            //base.OnKeyUp(key);
            if (key == Keys.LeftShift)
            {
                ShiftDown = false;
            }
        }

        public void UpdateScrollers()
        {
            int mw = 0;
            Scroller.MaxValue = Lines.Length * 100;
            for (int i = 0; i < Lines.Length; i++)
            {
                if (Lines[i] == null)
                {
                    continue;
                }
                if (Lines[i].Length > mw)
                {
                    mw = Lines[i].Length;
                }
            }
            HScroller.MaxValue = mw * 100;

            int a = 5;
        }

        public void SetText(string text)
        {

            int num = text.Count(c => c == '\n');

            Lines = new string[num + 1];

            int line = 0;
            for(int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    line++;
                    continue;
                }
                Lines[line] = Lines[line] + text[i];

            }
            int mw = 0;
            Scroller.MaxValue = Lines.Length * 100;
            for(int i = 0; i < Lines.Length; i++)
            {
                if (Lines[i] == null)
                {
                    continue;
                }
                if (Lines[i].Length > mw)
                {
                    mw = Lines[i].Length;
                }
            }
            HScroller.MaxValue = mw * 100;

            int a = 5;

        }

        public string GetActiveText(int line)
        {
            if (TextStartX >= Lines[line].Length)
            {
                return "";
            }
            return Lines[line].Substring(TextStartX);
        }
        public override void OnUpdate()
        {
            //base.OnUpdate();
            if (EditX > Text.Length)
            {
                EditX = Text.Length;
            }
            int tick = Environment.TickCount;
            if (tick >= NextBlink)
            {
                CursorOn = CursorOn ? false : true;
                NextBlink = tick + CursorBlinkInterval;
            }

            //string view_text = Text.Substring(TextStart);
            //string cur_Tex = view_text.Substring(0, EditX);


        }
        private int GetActiveCursorY()
        {
            return (UI.SystemFont.StringHeight() + 8) * (EditY-TextStartY);

        }
        private int GetActiveCursorX()
        {
            int cx = 0;
            for (int i = TextStartX; i < Text.Length; i++)
            {
                if (i == EditX) return cx;
                cx = cx + UI.SystemFont.StringWidth(Text.Substring(i, 1));
            }
            return cx;
        }
        int mx = 0;
        int my = 0;
        public override void OnMouseMove(Position position, Delta delta)
        {
            //base.OnMouseMove(position, delta);
            mx = position.x - RenderPosition.x;
            my = position.y;
        }
        public override void OnMouseDown(MouseID button)
        {

            int dy = RenderPosition.y + 8;

            for (int i = TextStartY; i < Lines.Length; i++)
            {
                if (Lines[i] == null) continue;
                string text = GetActiveText(i);
                if(my>=dy  && my<=dy+UI.SystemFont.StringHeight()+8)
                {
                    EditY = i;
                    break;
                }
               // UI.DrawString(text, dx, dy, new Maths.Color(1, 1, 1, 1));
                dy = dy + UI.SystemFont.StringHeight() + 8;
            }

            //base.OnMouseDown(button);
            string txt = GetActiveText(EditY);
            int cx = 0;
            bool done = false;
            for (int i = 0; i < txt.Length; i++)
            {
                if (cx > mx)
                {
                    EditX = TextStartX + i - 1;
                    if (EditX < 0) EditX = 0;
                    done = true;
                    break;
                }
                cx = cx + UI.SystemFont.StringWidth(txt[i].ToString());
            }
            if (!done)
            {
                if(mx>RenderPosition.x)
                {
                    EditX = Text.Length - 1;
                }
            }
        }
        public Vivid.Maths.Color NextColor(string text,int start)
        {
            Maths.Color col = new Maths.Color(0, 0, 0, 1);
            int next = text.IndexOf(":", start + 1);
            string r  = text.Substring(start + 1, next - start - 1);
            int pv = next;
            next = text.IndexOf(":", next + 1);
            string g = text.Substring(pv + 1, next - pv - 1);
            pv = next;
            next = text.IndexOf("#", next + 1);
            string b = text.Substring(pv + 1, next - pv - 1);
            // int a = 5;
            col.r = float.Parse(r);
            col.g = float.Parse(g);
            col.b = float.Parse(b);
            return col;
            
        }
        private Maths.Color CurColor = new Maths.Color(1, 1, 1,1);
        public int ParseTag(string text,int i)
        {
            int next = text.IndexOf(':',i);
            string tag = text.Substring(i,(next-i));

            switch (tag)
            {
                case "COL":
                    Vivid.Maths.Color col = NextColor(text, next);
                    CurColor = col;
                    //int a = 5;
                    break;
            }
         

            return 0;
        }
        public override void OnRender()
        {
            CurColor = new Maths.Color(1, 1, 1, 1);
            //base.OnRender();
            Draw(UI.Theme.Frame, RenderPosition.x, RenderPosition.y, Size.w, Size.h, new Maths.Color(2, 2, 2, 2));
            Draw(UI.Theme.Frame, RenderPosition.x + 2, RenderPosition.y + 2, Size.w - 4, Size.h - 4, new Maths.Color(0.35f, 0.35f, 0.35f, 1.0f));

            int dx, dy;
            dx = RenderPosition.x + 8;
            dy = RenderPosition.y + 8;

            for(int i = TextStartY; i < Lines.Length; i++)
            {
                if (Lines[i] == null) continue;
                string text = GetActiveText(i);
                dx = RenderPosition.x + 8;
                for(int j = 0; j < text.Length; j++)
                {
                    if (text[j]=='#')
                    {
                        if (j + 1 >= text.Length)
                        {
                            continue;
                        }
                        if (text[j+1]=='#')
                        {
                            try
                            {
                                int control = ParseTag(text, j + 2);
                                int next = text.IndexOf("#", j + 2) + 1;
                                j = next;
                            }
                            catch (Exception e)
                            {
                                continue;
                                
                            }
                            continue;
                        }
                    }
                    UI.DrawString(text[j].ToString(), dx, dy, CurColor); ;
                    dx = dx + UI.SystemFont.StringWidth(text[j].ToString()); ;
                }
                //UI.DrawString(text, dx, dy, new Maths.Color(1, 1, 1, 1));
                dy = dy + UI.SystemFont.StringHeight() + 8;
            }


            int CursorX = GetActiveCursorX();
            int CursorY = GetActiveCursorY();
            CursorX += 4;
            if (CursorX >= Size.w)
            {
                TextStartX++;
            }
            if (CursorY >= Size.h)
            {
                TextStartY++;
            }
            if (CursorY < 0)
            {
                TextStartY--;
            }

            if (CanEdit)
            {
                if (Active)
                {
                    if (CursorOn)
                    {
                        Draw(UI.Theme.Frame, RenderPosition.x + CursorX + 3, RenderPosition.y + 5 + CursorY, 2, UI.SystemFont.StringHeight() + 2, new Maths.Color(2, 2, 2, 2));
                    }
                }
            }


        }

    }
}
