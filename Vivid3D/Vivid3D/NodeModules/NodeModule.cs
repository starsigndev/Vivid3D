using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Scene;
using Vivid.UI;
using Vivid.UI.Forms;

namespace Vivid.NodeModules
{
    public class NodeModule
    {

        public Node Node
        {
            get;
            set;
        }

        public Entity Entity
        {
            get;
            set;
        }

        public SkeletalEntity SkeletalEntity
        {
            get;
            set;
        }

        private Reflection.ObjectState State
        {
            get;
            set;
        }

        public virtual void Store()
        {
            State = new Reflection.ObjectState(this);
        }
        public virtual void Restore()
        {
            State.ResetState();
        }

        public virtual void Begin()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void CreateUI()
        {

        }

        public virtual void Render()
        {

        }


        public virtual void End()
        {

        }

    }
}
