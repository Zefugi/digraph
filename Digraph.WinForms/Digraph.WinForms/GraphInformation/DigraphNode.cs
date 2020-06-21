using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digraph.WinForms.GraphInformation
{
    public enum NodeDirection
    {
        Input,
        Output,
    }

    public class DigraphNode
    {
        public string Title;
        public Type DataType;
        public NodeDirection Direction;
    }
}
