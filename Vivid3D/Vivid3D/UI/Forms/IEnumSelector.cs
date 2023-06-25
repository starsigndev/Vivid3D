using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vivid.UI.Forms;
using Vivid.UI;

namespace Vivid.UI.Forms
{
    public delegate void EnumSelected(string value);
    public class IEnumSelector : IForm
    {

        public System.Type EType
        {
            get;
            set;
        }

        public List<string> Values = new List<string>();

        public int CurrentSelection
        {
            get;
            set;
        }

        private bool Open
        {
            get;
            set;
        }

        public event EnumSelected OnSelected;

        private IList Selector;

        public IEnumSelector(Type etype)
        {

            System.Type enumUnderlyingType = System.Enum.GetUnderlyingType(etype);
            System.Array enumValues = System.Enum.GetValues(etype);

            foreach (object enumValue in enumValues)
            {
                Values.Add(enumValue.ToString());

                //  Console.WriteLine("V:" + Values[Values.Count - 1]);
            }

            EType = etype;

            CurrentSelection = 0;
            Open = false;
            DrawOutline = true;

        }

        public override void OnRender()
        {
            //           base.RenderForm();
            //DrawFrameRounded();
            Draw(UI.Theme.Frame);


            UI.DrawString(Values[CurrentSelection], RenderPosition.x + 6, RenderPosition.y + 4, UI.Theme.TextColor);
            UI.DrawString(EType.Name, RenderPosition.x + Size.w + 8, RenderPosition.y + 3, UI.Theme.TextColor);
        }

        public override void OnMouseDown(MouseID button)
        {
            
            //base.OnMouseDown(button);
            if (button == MouseID.Left)
            {

                Open = Open ? false : true;

                if (Open)
                {
                    Selector = new IList();
                    foreach (var v in Values)
                    {
                        var item = Selector.AddItem(v);
                        item.Action += (item, index, data) =>
                        {
                            int i = 0;
                            foreach (var sel in Values)
                            {
                                if (sel == item.Name)
                                {
                                    CurrentSelection = i;
                                    //Child.Remove(Selector);
                                    //Forms.Remove(Selector);
                                    UI.This.Overlay.Forms.Remove(Selector);
                                    Open = false;
                                    OnSelected?.Invoke(Values[i]);
                                    break;
                                }
                                i++;
                            }
                        };
                    }
                    Selector.CalculateHeight();
                    //AddForm(Selector);
                    UI.This.Overlay.AddForms(Selector);
                    Selector.Set(RenderPosition.x+0, RenderPosition.y+35, Size.w, Selector.Size.h);
                   
                }
                else
                {
                    UI.This.Overlay.Forms.Remove(Selector);
                    //C//hild.Remove(Selector);
                }

            }
        }

        private void Selector_OnSelected(ListItem item)
        {
            int i = 0;
            foreach (var sel in Values)
            {
                if (sel == item.Name)
                {
                    CurrentSelection = i;
                    Forms.Remove(Selector);

                    //Child.Remove(Selector);
                    Open = false;
                    OnSelected?.Invoke(Values[i]);
                    break;
                }
                i++;
            }
        }
    }
}