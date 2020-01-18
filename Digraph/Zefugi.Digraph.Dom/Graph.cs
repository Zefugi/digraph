using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Digraph.Dom
{
    public class Graph
    {
        public string Name { get; set; }

        public readonly List<Node> Nodes = new List<Node>();
    }
}
