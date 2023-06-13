using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.UI.Forms
{
    public class IDateSelector : IForm
    {
        public DateTime Date
        {
            get;
            set;
        }
        public bool InEnglish
        {
            get;
            set;
        }

        private IFrame SelectFrame
        {
            get;set;
        }

        public IDateSelector()
        {
            Date = DateTime.Now;
            DrawOutline = true;
            InEnglish = true;
            SelectFrame = null;
            Months[0] = "Jan";
            Months[1] = "Feb";
            Months[2] = "Mar";
            Months[3] = "Apr";
            Months[4] = "May";
            Months[5] = "Jun";
            Months[6] = "Jul";
            Months[7] = "Aug";
            Months[8] = "Sep";
            Months[9] = "Oct";
            Months[10] = "Nov";
            Months[11] = "Dec";
        }
        private string[] Months = new string[12];
        ILabel month_lab = null;
        List<IButton> day_buts = new List<IButton>();
        private void UpdateDate()
        {
            month_lab.Text = Months[Date.Month-1];

            
            AddDays();
        }
        private void AddDays()
        {

            int days = DateTime.DaysInMonth(Date.Year, Date.Month);

            int dx = 5;
            int dy = 45;
            foreach(var but in day_buts)
            {
                SelectFrame.Forms.Remove(but);
            }

            day_buts.Clear();
            for(int i = 0; i < days; i++)
            {
                int day_num = i + 1; 
                var day_but = new IButton().Set(dx, dy, 24, 24, "") as IButton;
                day_but.Text = day_num.ToString();
                dx = dx + 28;
                if(dx+22>=SelectFrame.Size.w)
                {
                    dx = 5;
                    dy = dy + 28;
                }
                SelectFrame.AddForm(day_but);
                day_buts.Add(day_but);
                day_but.OnClick += (form, data) =>
                {
                    Date = new DateTime(Date.Year, Date.Month, day_num);
                };

            }


        }
        public override void OnMouseDown(MouseID button)
        {
            //base.OnMouseDown(button);
            if (SelectFrame == null)
            {

                SelectFrame = new IFrame().Set(0, 33, 220, 200, "") as IFrame;
                SelectFrame.DrawOutline = true;

                var monthl = new IButton().Set(5, 5, 22, 25, "") as IButton;

                monthl.Icon = UI.Theme.ArrowLeft;

                var monthr = new IButton().Set(73, 5, 22, 25, "") as IButton;
                monthr.Icon = UI.Theme.ArrowRight;

                var month = new ILabel().Set(34, 8, 80, 30, Months[Date.Month - 1]) as ILabel;

                month_lab = month;

                monthl.OnClick += (form, data) =>
                {

                    int new_month = Date.Month - 1;
                    if (new_month < 1)
                    {
                        new_month = 12;
                    }
                    Date = new DateTime(Date.Year,new_month, Date.Day);
                    UpdateDate();

                };

                monthr.OnClick += (form, data) =>
                {

                    int new_month = Date.Month + 1;
                    if (new_month > 12)
                    {
                        new_month = 1;
                    }
                    Date = new DateTime(Date.Year, new_month, Date.Day);
                    UpdateDate();
                };

                var year = new INumericBox().Set(103, 5, 115, 24, "") as INumericBox;
                year.Number.MinValue = 1000;
                year.Number.MaxValue = 3000;
                year.Number.Text = Date.Year.ToString();


                year.Number.OnChange = (form, val) =>
                {
                    int valc = (int)year.Number.Value;
                    Date = new DateTime(valc, Date.Month, Date.Day);
                    UpdateDate();

                };

                SelectFrame.AddForm(year);

                //var year = new INumericBox().Set(60, 2, 140, 30, "") as INumericBox;
                //year.Number.Text = Date.Year.ToString();


                SelectFrame.AddForms(monthl,month,monthr);

                AddDays();

                AddForm(SelectFrame);
            }
            else
            {
                Forms.Remove(SelectFrame);
                SelectFrame = null; 
            }
        }

        public override void OnRender()
        {
            //base.OnRender();
            Draw(UI.Theme.Frame);


            if (InEnglish)
            {
                string day = Date.DayOfWeek.ToString();
                string month = Date.ToString("MMMM");
                int year = Date.Year;

                string text = day + "/" + month + "/" + year;
                UI.DrawString(text, RenderPosition.x + 3, RenderPosition.y + 5, UI.Theme.TextColor);
            }
            else
            {
                string day = Date.Day.ToString();
                string month = Date.Month.ToString();
                string year = Date.Year.ToString();
                string text = day + "/" + month + "/" + year;
                UI.DrawString(text, RenderPosition.x + 3, RenderPosition.y + 5, UI.Theme.TextColor);

            }


        }

    }
}
