using OpenTK.Graphics.OpenGL;

namespace Vivid.State
{
    public enum CurrentGLState
    {
        LightFirstPass, LightSecondPass, Depth, Fullbright, None,Draw
    }

    public class GLState
    {
        public static CurrentGLState State
        {
            get
            {
                return _CurrentState;
            }
            set
            {
                if (_CurrentState != value)
                {
                    _CurrentState = value;
                    StateBound = false;
                    BindState();
                }
            }
        }

        private static CurrentGLState _CurrentState = CurrentGLState.None;
        private static bool StateBound = false;

        public static void BindState()
        {
            if (StateBound == true) return;
            switch (_CurrentState)
            {
                case CurrentGLState.Draw:
                    GL.Disable(EnableCap.DepthTest);
                    GL.Disable(EnableCap.CullFace);
                    GL.Disable(EnableCap.Blend);
                    GL.Disable(EnableCap.ScissorTest);
                    break;
                case CurrentGLState.LightFirstPass:
                    GL.Disable(EnableCap.Blend);
                    GL.Enable(EnableCap.CullFace);
                    GL.CullFace(TriangleFace.Back);
                    GL.Enable(EnableCap.DepthTest);
                    GL.DepthFunc(DepthFunction.Lequal);
                    break;

                case CurrentGLState.LightSecondPass:
                    GL.Enable(EnableCap.DepthTest);
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
                    GL.Enable(EnableCap.CullFace);
                    GL.CullFace(TriangleFace.Back);
                    GL.DepthFunc(DepthFunction.Lequal);
                    break;

                case CurrentGLState.Depth:

                    GL.Disable(EnableCap.CullFace);
                    GL.Enable(EnableCap.DepthTest);
                    GL.Disable(EnableCap.Blend);
                    GL.DepthFunc(DepthFunction.Lequal);
                    GL.Enable(EnableCap.CullFace);
                    GL.CullFace(TriangleFace.Back);

                    break;

                case CurrentGLState.Fullbright:

                    break;
            }
            StateBound = true;
        }
    }
}