using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Vivid.IO;

namespace Vivid.Reflection
{
 

    public class ObjectState
    {
        private readonly object _sourceObject;
        public readonly Dictionary<PropertyInfo, object> _propertyValues;



        public ObjectState(object sourceObject)
        {
            _sourceObject = sourceObject;
            _propertyValues = new Dictionary<PropertyInfo, object>();

            foreach (var prop in _sourceObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    var prop_name = prop.Name;
                    if (prop_name == "ObjState")
                    {

                  
                    }
                    else
                    {
                        _propertyValues.Add(prop, prop.GetValue(_sourceObject));
                    }
                }
            }
        }

        public void ResetState()
        {
            foreach (var prop in _propertyValues.Keys)
            {
                prop.SetValue(_sourceObject, _propertyValues[prop]);
            }
        }
        public void Load(BinaryReader r)
        {

            //foreach(var prop in _propertyValues.Keys)
            //{

            int pc = r.ReadInt32();
            for(int i = 0; i < pc; i++)
            {
                string name = r.ReadString();
                string ptype = r.ReadString();

                var prop = GetProp(name);

                if (prop.PropertyType.ToString().Contains("Mathematics.Vector3"))
                {

                    prop.SetValue(_sourceObject, FileHelp.ReadVec3(r));
                    //var v = AddVector3((Vector3)prop.GetValue(mod), prop.Name);
                    //FileHelp.WriteVec3(w, (OpenTK.Mathematics.Vector3)prop.GetValue(_sourceObject));



                }

                if (prop.PropertyType.ToString().Contains("System.Single"))
                {
                    //var f = AddFloat((float)prop.GetValue(mod), prop.Name);

                    prop.SetValue(_sourceObject, r.ReadSingle());

             

                }

                if (prop.PropertyType.ToString().Contains("System.String"))
                {

                    //w.Write((string)prop.GetValue(_sourceObject));

                    prop.SetValue(_sourceObject, r.ReadString());


                }

                if (prop.PropertyType.ToString().Contains("System.Boolean"))
                {


                    //w.Write((bool)prop.GetValue(_sourceObject));
                    prop.SetValue(_sourceObject, r.ReadBoolean());

                }

                if (prop.PropertyType.ToString().Contains("Int32") || prop.PropertyType.ToString().Contains("Int64"))
                {


                    //w.Write((int)prop.GetValue(_sourceObject));

                    prop.SetValue(_sourceObject, r.ReadInt32());

                }


            }



        }

        public PropertyInfo GetProp(string name)
        {

            foreach (var prop in _propertyValues.Keys)
            {
                if (prop.Name == name)
                {
                    return prop;
                }

         
            }
            return null;
        }

        public void Save(BinaryWriter w)
        {
            w.Write(_propertyValues.Keys.Count);
            foreach(var prop in _propertyValues.Keys)
            {
                w.Write(prop.Name);
                w.Write(prop.PropertyType.ToString());
                if (prop.PropertyType.ToString().Contains("Mathematics.Vector3"))
                {

                    //var v = AddVector3((Vector3)prop.GetValue(mod), prop.Name);
                    FileHelp.WriteVec3(w,(OpenTK.Mathematics.Vector3)prop.GetValue(_sourceObject));
                  


                }

                if (prop.PropertyType.ToString().Contains("System.Single"))
                {
                    //var f = AddFloat((float)prop.GetValue(mod), prop.Name);



                    w.Write((float)prop.GetValue(_sourceObject));


                }

                if (prop.PropertyType.ToString().Contains("System.String"))
                {
                    
                    w.Write((string)prop.GetValue(_sourceObject));


                }

                if (prop.PropertyType.ToString().Contains("System.Boolean"))
                {
                    

                    w.Write((bool)prop.GetValue(_sourceObject));

                }

                if (prop.PropertyType.ToString().Contains("Int32") || prop.PropertyType.ToString().Contains("Int64"))
                {

                   
                    w.Write((int)prop.GetValue(_sourceObject));

                }

                //w.Write(prop.GetValue(_sourceObject));


            }
        }
    }

}
