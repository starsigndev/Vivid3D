using Vivid.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneEditor
{
    public class NodeTreeNode : TreeNode
    {
        public Node Node
        {
            get;
            set;
        }
        public override string ToString()
        {
            return Node.Name;
        }


    }
}
