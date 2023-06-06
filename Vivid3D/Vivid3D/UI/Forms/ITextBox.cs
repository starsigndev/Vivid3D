using Microsoft.VisualBasic.FileIO;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vivid.Maths;
using static System.Net.Mime.MediaTypeNames;

namespace Vivid.UI.Forms
{
    public class ITextBox : IForm
    {

        public override string Text
        {
            get
            {
                return _val;
            }
            set
            {
                if (Numeric)
                {
                    if (value == "")
                    {
                        _val = "";
                        return;
                    }
                   float val = float.Parse(value);
                    if (val < MinValue)
                    {
                        val = MinValue;
                    }
                    if (val > MaxValue)
                    {
                        val = MaxValue;
                    }
                    _val = val.ToString();
                    return;
                }
                _val = value;
            }
        }
        string _val;

        public bool Numeric
        {
            get;
            set;
        }

        public bool Password
        {
            get;
            set;
        }

        public int TextStart
        {
            get;
            set;
        }

        public int EditX
        {
            get;
            set;
        }

        public float Value
        {
            get
            {
                return float.Parse(Text);
            }
            set
            {
                Text = value.ToString();
            }
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

        public bool ShiftDown
        {
            get;
            set;
        }

        public float MinValue
        {
            get;
            set;
        }

        public float MaxValue
        {
            get;
            set;
        }

        public ITextBox()
        {
            TextStart = 0;
            ScissorSelf = true;
            EditX = 0;
            ShiftDown = false;
            MinValue = -10;
            MaxValue = 10;
            CursorOn = true;
            CursorBlinkInterval = 500;
            NextBlink = Environment.TickCount + CursorBlinkInterval;
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

        private string GetActiveText()
        {
            if(TextStart > Text.Length)
            {
            //    return Text;
            }
            if (TextStart > Text.Length)
            {
                TextStart = Text.Length;
            }
            return Text.Substring(TextStart);
        }

        private int GetActiveCursorX()
        {
            int cx = 0;
            string text = Text;
            if (Password)
            {
                string replace = "";
                for (int i = 0; i < text.Length; i++)
                {
                    replace = replace + "*";
                }
                text = replace;
            }

            for (int i = TextStart; i < text.Length; i++)
            {
                if (i == EditX) return cx;
                cx = cx + UI.SystemFont.StringWidth(text.Substring(i, 1));
            }
            return cx;
        }
        private string numerics = "0123456789.";
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
            if (Numeric)
            {
                if (numerics.IndexOf(chr) >= 0)
                {
                    return chr;
                }
                else
                {
                    return "";
                }
            }
            return chr;
        }

        public override void OnKey(Keys key)
        {
            //base.OnKey(key);
            switch (key)
            {
                case Keys.Left:
                    EditX--;
                    if (EditX < 0)
                    {
                        EditX = 0;
                    }
                    if (EditX < TextStart)
                    {
                        TextStart--;
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
            if (chr == "")
            {
                return;
            }
            InsertChr(chr);
            //Text = Text + chr;
//            EditX++;


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
            if(EditX==Text.Length)
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
                Text = Text.Substring(0,Text.Length - 1);
                EditX--;
                if (EditX < TextStart)
                {
                    TextStart--;
                    TextStart--;
                    if (TextStart < 0)
                    {
                        TextStart = 0;
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
                if (EditX < TextStart)
                {
                    TextStart--;
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

            if(keys == Keys.LeftShift)
            {
                ShiftDown = true;
            }
        
        }

        public override void OnKeyUp(Keys key)
        {
            //base.OnKeyUp(key);
            if(key == Keys.LeftShift)
            {
                ShiftDown = false;
            }
        }

        int mx = 0;
        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            string txt = GetActiveText();
            int cx = 0;

            if (Password)
            {
                string replace = "";
                for (int i = 0; i < txt.Length; i++)
                {
                    replace = replace + "*";
                }
                txt = replace;
            }

            for (int i = 0; i < txt.Length; i++)
            {
                if (cx > mx)
                {
                    EditX = TextStart + i - 1;
                    if (EditX < 0) EditX = 0;
                    return;
                }
                cx = cx + UI.SystemFont.StringWidth(txt[i].ToString());
            }
        }

        public override void OnMouseMove(Position position, Delta delta)
        {
            //base.OnMouseMove(position, delta);
            mx = position.x - RenderPosition.x;
        }

        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.Frame,RenderPosition.x, RenderPosition.y, Size.w,Size.h, new Maths.Color(2, 2, 2, 2));
            Draw(UI.Theme.Frame, RenderPosition.x + 2, RenderPosition.y + 2, Size.w - 4, Size.h - 4, new Maths.Color(0.2f, 0.2f, 0.2f, 1.0f));

            string text = GetActiveText();

            if (Password)
            {
                string replace = "";
                for(int i = 0; i < text.Length; i++)
                {
                    replace = replace + "*";
                }
                text = replace;
            }

            UI.DrawString(text, RenderPosition.x + 7, RenderPosition.y + 8, new Maths.Color(1, 1, 1, 1));

            int CursorX = GetActiveCursorX();
            CursorX += 4;
            if (CursorX >= Size.w)
            {
                TextStart++;
            }

            if (Active)
            {
                if (CursorOn)
                {
                    Draw(UI.Theme.Frame, RenderPosition.x + CursorX+3, RenderPosition.y + 5, 2, Size.h - 10, new Maths.Color(2, 2, 2, 2));
                }
            }

        }

    }
}
