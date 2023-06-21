using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Vivid.Reflection
{
 

    public class ObjectState
    {
        private readonly object _sourceObject;
        private readonly Dictionary<PropertyInfo, object> _propertyValues;

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
    }

}
