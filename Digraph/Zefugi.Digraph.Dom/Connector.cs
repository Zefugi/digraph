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

        public int MaxConnections { get; set; }

        public ConnectorDirection Direction { get; set; }

        internal ObservableList<Connection> _connections = new ObservableList<Connection>();

        public ConnectionResult TryConnect<T>(Connector target)
            where T : Connection, new()
        {
            ConnectionResult result = ConnectionResult.Success;

            if (Direction == target.Direction)
                result |= ConnectionResult.InvalidDirection;
            if (Type != target.Type)
                result |= ConnectionResult.InvalidType;
            if ((MaxConnections != 0 && _connections.Count == MaxConnections) ||
                (target.MaxConnections != 0 && target._connections.Count == target.MaxConnections))
                result |= ConnectionResult.MaxConnectionsReached;

            Connection conn = new T();
            conn.Source = Direction == ConnectorDirection.Output ? this : target;
            conn.Sink = Direction == ConnectorDirection.Input ? this : target;
            _connections.Add(conn);
            target._connections.Add(conn);

            return result;
        }
    }
}
