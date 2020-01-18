using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Digraph.Dom
{
    public class Node
    {
        public Graph Graph { get; internal set; }

        public string Title { get; set; }

        public readonly List<Connector> Connectors = new List<Connector>();
    }
}
