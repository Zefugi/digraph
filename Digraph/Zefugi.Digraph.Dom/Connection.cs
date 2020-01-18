using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Digraph.Dom
{
    public class Connection
    {
        public Connector Source { get; set; }
        public Connector Sink { get; set; }
    }
}
