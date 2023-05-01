using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Vivid.Anim
{

    public enum AnimType
    {
        Forward, Backward, PingPong, Once
    };

    public class ActorAnimation
    {

        public float StartTime;
        public float EndTime;
        public float CurTime = 0.0f;
        public float Speed = 0.0f;
        public string Name = "";
        AnimType Type;
        public ActorAnimation(string name, float start, float end, float speed, AnimType type)
        {
            Type = type;

            StartTime = start;
            EndTime = end;
            Name = name;
            Speed = speed;
        }

        public void Begin()
        {
            CurTime = StartTime;
        }

        public void Update()
        {

            CurTime = CurTime + Speed;
            if (CurTime >= EndTime)
            {

                switch (Type)
                {

                    case AnimType.Forward:

                        CurTime = StartTime;

                        break;
                    case AnimType.Once:

                        CurTime = EndTime;

                        break;
                }

            }


        }
    }
}