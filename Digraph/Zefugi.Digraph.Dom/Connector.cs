using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Digraph.Dom
{
    public class Connector
    {
        public Node Node { get; internal set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public ConnectorDirection Direction { get; set; }

        internal ObservableList<Connection> _connections = new ObservableList<Connection>();
    }
}
