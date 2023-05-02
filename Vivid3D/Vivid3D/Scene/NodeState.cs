using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Scene
{
    public class NodeState
    {

        public string Name
        {
            get;
            set;
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }

        public virtual void Stop()
        {

        }

    }
}
