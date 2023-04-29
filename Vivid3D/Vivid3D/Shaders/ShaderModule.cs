using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;
using PhysX;

namespace Vivid.Shaders
{
    public class ShaderModule
    {
        public ProgramHandle Program
        {
            get;
            set;
        }

        public ShaderHandle VertexShader
        {
            get;
            set;
        }

        public ShaderHandle FragmentShader
        {
            get;
            set;
        }

        private bool NotInit = true;

        static Dictionary<string,ShaderModule> Modules = new Dictionary<string, ShaderModule> ();
        public ShaderModule(string vertex_path,string fragment_path)
        {

            string full = vertex_path + fragment_path;

            if (Modules.ContainsKey(full))
            {

                Program = Modules[full].Program;
                return;

            }

            VertexShader = LoadShader(vertex_path, ShaderType.VertexShader);
            FragmentShader = LoadShader(fragment_path,ShaderType.FragmentShader);

            Program = GL.CreateProgram();

            GL.AttachShader(Program, VertexShader);
            GL.AttachShader(Program, FragmentShader);

            GL.LinkProgram(Program);

            GL.DetachShader(Program,VertexShader);
            GL.DetachShader(Program,FragmentShader);
            GL.DeleteShader(VertexShader);
            GL.DeleteShader(FragmentShader);
            Modules.Add(full, this);
            InitUniforms();

        }

        private ShaderHandle LoadShader(string path,ShaderType type)
        {

            var res = GL.CreateShader(type);
            
            GL.ShaderSource(res, File.ReadAllText(path));
            GL.CompileShader(res);

            int[] status = new int[1];
            GL.GetShaderi(res,ShaderParameterName.CompileStatus, status);

            if (status[0] == 1)
            {
                // Shader compiled successfully
                Console.WriteLine("Shader compiled successfully.");
            }
            else
            {
                // Shader compilation failed
                int[] infoLogLength = new int[1];
                GL.GetShaderi(res,ShaderParameterName.InfoLogLength, infoLogLength);

                string infoLog = string.Empty;
                if (infoLogLength[0] > 0)
                {
                    string info = "";
                    GL.GetShaderInfoLog(res,out infoLog);
                }

                Console.WriteLine("Shader compilation failed: " + infoLog);
            }

            return res;

        }

        public int GetLocation(string name)
        {

            //if (Cache.ContainsKey(name))
            {
                //return Cache[name];
            }
            return GL.GetUniformLocation(Program, name);
         

        }

        public void SetFloat(string name,float value)
        {
            GL.Uniform1f(GetLocation(name), value);
        }

        public void SetUni(string name,int value)
        {
            GL.Uniform1i(GetLocation(name), value);
        }

        public void SetUni(string name,float value)
        {

            GL.Uniform1f(GetLocation(name), value);

        }

        public void SetUni(string name,Vector2 value)
        {
            GL.Uniform2f(GetLocation(name), value);
        }

        public void SetUni(string name,Vector3 value)
        {
            GL.Uniform3f(GetLocation(name), value);
        }

        public void SetUni(string name,Vector4 value)
        {
            GL.Uniform4f(GetLocation(name), value);

        }

        public void SetUni(string name,Matrix4 value)
        {

            GL.UniformMatrix4f(GetLocation(name), false, in value);

        }

        public void SetUni(int id,int value)
        {
            GL.Uniform1i(id, value);
        }

        public void SetUni(int id,float v)
        {
            GL.Uniform1f(id, v);
        }

        public void SetUni(int id,Vector3 v)
        {
            GL.Uniform3f(id, v);
        }
        public void SetUni(int id,Vector4 v)
        {
            GL.Uniform4f(id, v);
        }

        public void SetUni(int id,Matrix4 m)
        {
            GL.UniformMatrix4f(id, false, m);
        }

        public void Bind(bool set_uni=true)
        {

            GL.UseProgram(Program);

            //    if (set_uni)
            //      {
            if (NotInit)
            {
                InitUniforms();
                NotInit = false;
            }
                SetUniforms();
      

        }

        public void Unbind()
        {
            GL.UseProgram(ProgramHandle.Zero);
        }

        public virtual void SetUniforms()
        {


        }

        public virtual void InitUniforms()
        {

        }

        private Dictionary<string, int> Cache = new Dictionary<string, int>();

    }
}
