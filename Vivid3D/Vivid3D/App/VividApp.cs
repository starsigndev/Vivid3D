using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;
using Vivid.Physx;
using Vivid.RenderTarget;

namespace Vivid.App
{
    public class VividApp : GameWindow
    {
        public static AppMetrics Metrics
        {
            get;
            set;
        }

        public static int FrameWidth
        {
            get
            {
                if (BoundRTC != null)
                {
                    return BoundRTC.Width;
                }
                else
                {
                    if (BoundRT2D != null)
                    {
                        return BoundRT2D.Width;
                    }
                    return _FW;
                }
            }
            set
            {
                _FW = value;
            }
        }

        public static int FrameHeight
        {
            get
            {
                if (BoundRTC != null)
                {
                    return BoundRTC.Height;
                }
                else
                {
                    if (BoundRT2D != null)
                    {
                        return BoundRT2D.Height;
                    }
                    return _FH;
                }
            }
            set
            {
                _FH = value;
            }
        }

        public static RenderTargetCube BoundRTC = null;
        public static RenderTarget2D BoundRT2D = null;
        public static int _FW, _FH;

        public static Vivid.Scene.Scene CurrentScene
        {
            get;
            set;
        }

        public static Stack<AppState> States
        {
            get;
            set;
        }

        public static AppState InitialState
        {
            get;
            set;
        }

        public static void PushState(AppState state)
        {
            if (States.Count > 0)
            {
                var prev = States.Peek();
                prev.Stop();
            }
            States.Push(state);

            //CurrentScene = state.StateScene;
            state.InitState();
            CurrentScene = state.StateScene;
            state.Init();
        }

        public static void PopState()
        {
            if (States.Count > 0)
            {
                States.Peek().Stop();
                States.Pop();
                if (States.Count > 0)
                {
                    CurrentScene = States.Peek().StateScene;
                }
            }
        }

        public static string ContentPath
        {
            get;
            set;
        }

        // public GeminiApp(int w,int h)
        //{
        //Metrics = new AppMetrics(w, h, "", false);
        // ContentPath = "c:/content/content/";
        // FrameWidth = w;
        //  FrameHeight = h;
        //}

        public VividApp(GameWindowSettings game_window, NativeWindowSettings native_window) : base(game_window, native_window)
        {
            VSync = VSyncMode.Off;
            ContentPath = "C:/content/content/";
            // Metrics = metrics;
            // _FW = metrics.WindowWidth;
            // _FH = metrics.WindowHeight;
            _FW = native_window.Size.X;
            _FH = native_window.Size.Y;
            States = new Stack<AppState>();
            CursorVisible = false;
        }


        public void CreateWindow()

        {
        }

        public virtual void Init()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Render()
        {
        }

        private double lastTime;
        private Stopwatch stop;

        private double getDeltaTime()
        {
            double currentTime = stop.Elapsed.TotalSeconds;
            double deltaTime = currentTime - lastTime;
            lastTime = currentTime;
            return deltaTime;
        }

        public static int FPS = 0;
        private int last_fps = 0;
        private int frames = 0;

        protected override void OnResize(ResizeEventArgs e)
        {
            //base.OnResize(e);
            _FW = e.Width;
            _FH = e.Height;
        }

        protected override void OnLoad()
        {
            //base.OnLoad();
            GL.ClearColor(1, 0, 0, 1);
            Physics.QPhysics.InitPhysics();
            Vivid.Audio.AudioSys.Init();

            if (InitialState != null)
            {
                PushState(InitialState);
            }
            if (States.Count > 0)
            {
                //   States.Peek().Init();
            }
            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Blend);
            Scene.ShaderModules.Shaders.InitShaders();
            int aa = 5;
            //  GL.Disable(EnableCap.)
        
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            //base.OnKeyDown(e);
            GameInput.mKeyDown[(int)e.Key] = true; ;
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            //base.OnKeyUp(e);
            GameInput.mKeyDown[(int)e.Key] = false;
        }

        private bool first = true;
        private int fps, fframes, tick;

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            Vector2 mp = this.MouseState.Position;// - new OpenTK.Mathematics.Vector2(Bounds.X, Bounds.Y);

            //base.OnUpdateFrame(args)
            //;
            var delta = mp - GameInput.MousePosition;
            if (first)
            {
                delta = new Vector2(0, 0);
                first = false;
            }

            GameInput.MousePosition = mp;
            GameInput.MouseDelta = delta;
            if (States.Count > 0)
            {
                States.Peek().Update();
            }
            Physics.QPhysics.Simulate(1.0f / 60.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            //base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            int time = Environment.TickCount;

            if (time > tick)
            {
                tick = time + 1000;
                fps = fframes;
                fframes = 0;
                Console.WriteLine("FPS:" + fps);
            }
            fframes++;

            if (States.Count > 0)
            {
                States.Peek().Render();
            }

            SwapBuffers();
        }
    }
}