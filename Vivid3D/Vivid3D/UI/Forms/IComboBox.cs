using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public delegate void ItemSelected(string value);
    public class IComboBox : IForm
    {
        public List<ListItem> Items
        {
            get;
            set;
        }

        public ListItem SelectedItem
        {
            get;
            set;
        }

        private bool Open = false;
        private IList Selector;
        public event ItemSelected OnSelected;

        public IComboBox()
        {
            Items = new List<ListItem>();
            DrawOutline = true;
            SelectedItem = null;
        }

        public ListItem AddItem(string text)
        {
            ListItem item = new ListItem();
            item.Name = text;
            Items.Add(item);
            if(SelectedItem == null)
            {
                SelectedItem = item;
            }
            return item;
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
                    foreach (var v in Items)
                    {
                        var item = Selector.AddItem(v.Name);
                        item.Action = (item, index, data) =>
                        {
                            int i = 0;
                            foreach (var sel in Items)
                            {
                                if (sel.Name == item.Name)
                                {
                                    //CurrentSelection = i;
                                    SelectedItem = Items[i];
                                    //Child.Remove(Selector);
                                    Forms.Remove(Selector);
                                    Open = false;
                                    OnSelected?.Invoke(item.Name);
                                    break;
                                }
                                i++;
                            }
                        };
                    }
                    Selector.CalculateHeight();
                    AddForm(Selector);
                    Selector.Set(0, 35, Size.w, Selector.Size.h);

                }
                else
                {
                    Forms.Remove(Selector);
                    //C//hild.Remove(Selector);
                }

            }
        }

        public override void OnRender()
        {
            //.base.OnRender();
            Draw(UI.Theme.Frame);
            if (SelectedItem != null)
            {
                UI.DrawString(SelectedItem.Name, RenderPosition.x + 6, RenderPosition.y + 8, UI.Theme.TextColor);
            }
        }

    }
}
