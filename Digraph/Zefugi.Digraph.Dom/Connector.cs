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

        public ConnectionResult TryConnect<T>(Connector target, out Connection connection)
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

            OnTryConnect<T>(target, ref result);

            if (result == ConnectionResult.Success)
            {
                connection = new T();
                connection.Source = Direction == ConnectorDirection.Output ? this : target;
                connection.Sink = Direction == ConnectorDirection.Input ? this : target;
                _connections.Add(connection);
                target._connections.Add(connection);
            }
            else
                connection = null;

            return result;
        }

        protected virtual void OnTryConnect<T>(Connector target, ref ConnectionResult result) { }
    }
}
