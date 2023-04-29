using Vivid.Scene;
using Vivid.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Nodes
{
    public class FirstPersonController : Entity
    {

        public float TurnSpeed
        {
            get;
            set;
        }

        public float LookSpeed
        {
            get;
            set;
        }

        public float RunSpeed
        {
            get;
            set;
        }

        public float WalkSpeed
        {
            get;
            set;
        }

        public float StrafeSpeed
        {
            get;
            set;
        }

        public float JumpForce
        {
            get;
            set;
        }

        public Camera Cam
        {
            get
            {
                return _Cam;
            }
            set
            {
                _Cam = value;
                AddNode(_Cam);
               // _Cam.Position = new Vec3(0, 6, 0);
                //_Cam = value;
            }
        }
        Camera _Cam = null;

        float camPitch = 0.0f;
        bool Jumped = false;
        public FirstPersonController()
        {

            TurnSpeed = 0.75f;
            LookSpeed = 0.1f;
            BodyKind = Physx.BodyType.FPS;
            RunSpeed = 0.2f;
            WalkSpeed = 0.1f;
            StrafeSpeed = 0.15f;
            JumpForce = 7.5f;

        }
        public override void OnKeyPressed(KeyID key)
        {
            float run_mod = 1.0f;
            switch (key)
            {
                case KeyID.KeyW:
                    MoveBody(0, 0, -RunSpeed * 60 * run_mod);
                    break;
                case KeyID.KeyS:
                    MoveBody(0, 0, RunSpeed * 50f * run_mod);
                    break;
                case KeyID.KeyA:
                    MoveBody(-StrafeSpeed * 60.0f * run_mod, 0, 0);
                    break;
                case KeyID.KeyD:
                    MoveBody(StrafeSpeed * 60.0f * run_mod, 0, 0);
                    break;
            break;
            }

        }
        public override void OnKeyDown(KeyID key)
        {
            //base.OnKeyDown(key);
            if (key == KeyID.Space)
            {

                if (BodyOnGround())
                {

                    MoveBody(0, JumpForce * 100, 0);

                }
            }

        }

      
        public override void OnMuseMove(float x, float y)
        {
 
            TurnBody(0, -x * TurnSpeed, 0);
            camPitch -= y * LookSpeed;
            Cam.SetRotation(180+camPitch, 0, 0);
 

        }

        public override void OnKeyUp(KeyID key)
        {
            //base.OnKeyUp(key);

        }

        public override void UpdateNode()
        {

         
        }

    }
}
