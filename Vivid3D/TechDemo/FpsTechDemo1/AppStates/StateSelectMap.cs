using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.App;
using Vivid.Content;
using Vivid.Texture;
using Vivid.UI.Forms;
using Vivid.UI;
using FpsTechDemo1.Maps;

namespace FpsTechDemo1.AppStates
{
    public class StateSelectMap : AppState
    {

        public StateSelectMap() : base("Select Map")
        {

        }
        Content GameContent = null;
        IImage background_image;
        IFrame MapInfo;
        IFrame InfoCon;
        public override void Init()
        {

            StateUI = new Vivid.UI.UI();
            StateUI = new UI();

            ContentItem bg_image_Content = FpsTechDemoApp.ImagesContent.Find("titleBg1.png");

            var bg_image = new Texture2D(bg_image_Content.GetStream(), bg_image_Content.Width, bg_image_Content.Height);


            background_image = new IImage(bg_image).Set(new Vivid.Maths.Position(0, 0), new Vivid.Maths.Size(VividApp.FrameWidth, VividApp.FrameHeight), "BGImage") as IImage;

            StateUI.AddForm(background_image);

            IFrame preview_Image = new IFrame().Set(new Vivid.Maths.Position(VividApp.FrameWidth - 630, 40), new Vivid.Maths.Size(580, 330), "") as IFrame;
            IFrame map_info = new IFrame().Set(new Vivid.Maths.Position(VividApp.FrameWidth - 640, 380), new Vivid.Maths.Size(600, 340), "") as IFrame;
            IFrame map_info_Contents = new IFrame().Set(new Vivid.Maths.Position(10, 10), new Vivid.Maths.Size(map_info.Size.w - 20, map_info.Size.h - 20), "") as IFrame;
            InfoCon = map_info_Contents;
            map_info_Contents.Color = new Vivid.Maths.Color(3, 3, 3, 1);

            map_info.AddForm(map_info_Contents);

            PickMap(FpsTechDemoApp.GameMaps[0]);


            ILabel MapListLab = new ILabel().Set(new Vivid.Maths.Position(130, 20), new Vivid.Maths.Size(5, 5), "Maps") as ILabel;

            IList MapList = new IList().Set(new Vivid.Maths.Position(40, 40), new Vivid.Maths.Size(350, VividApp.FrameHeight - 80), "") as IList;

            background_image.AddForm(MapListLab);
            background_image.AddForm(MapList);
            background_image.AddForm(preview_Image);
            background_image.AddForm(map_info);

            int ii = 0;
            foreach(var map in FpsTechDemoApp.Maps)
            {
                var name = Path.GetFileNameWithoutExtension(map);
                var item = MapList.AddItem(name);
                item.Data = FpsTechDemoApp.GameMaps[ii];
                item.Action = (item, index,data) =>
                {
                    PickMap(data as GameMap);
                };
                ii++;
            }

            IButton select = new IButton().Set(new Vivid.Maths.Position(VividApp.FrameWidth-200, VividApp.FrameHeight - 40), new Vivid.Maths.Size(140, 30), "Select") as IButton;
            IButton back = new IButton().Set(new Vivid.Maths.Position(50, VividApp.FrameHeight - 40), new Vivid.Maths.Size(140, 30), "Back") as IButton;

            background_image.AddForms(select, back);

            select.OnClick += (sender,args) =>
            {

            };

            back.OnClick += (sender, args) =>
            {
                VividApp.PopState();
            };

        }

        public void PickMap(GameMap map)
        {

            InfoCon.Forms.Clear();
            string line = "";
            int cc = 0;
            string info = map.MapInfo;
            int dy = 25;

            while (true)
            {
                if(cc>=info.Length)
                {
                    if (line.Length > 0)
                    {
                        ILabel lab = new ILabel().Set(new Vivid.Maths.Position(20, dy), new Vivid.Maths.Size(5, 5), line) as ILabel;
                        InfoCon.AddForm(lab);
                    }
                    break;
                }
                if (info[cc] == "\n"[0] || info[cc] == "\r"[0])
                {
                    ILabel lab = new ILabel().Set(new Vivid.Maths.Position(20, dy), new Vivid.Maths.Size(5, 5), line) as ILabel;
                    InfoCon.AddForm(lab);
                    line = "";
                    dy = dy + 20;
                    cc++;
                    continue;
                }
                line = line + info[cc];

                cc++;
              
            }

        }

        public override void Update()
        {
          
            StateUI.Update();
        }

        public override void Render()
        {
            StateUI.Render();
        }

    }
}
