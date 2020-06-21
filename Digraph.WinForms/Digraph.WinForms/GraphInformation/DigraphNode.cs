using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        [NonSerialized]
        public object Tag;

        public string Title;
        public Type DataType;
        public NodeDirection Direction;
        public bool ByRef;

        private readonly List<DigraphNode> _connections = new List<DigraphNode>();

        public ReadOnlyCollection<DigraphNode> Connections
        {
            get { return _connections.AsReadOnly(); }
        }

        public bool CanConnectTo(DigraphNode node)
        {
            if (node == this)
                return false;
            if ((DataType == null || node.DataType == null))
                return DataType == node.DataType;
            return Direction != node.Direction &&
                DataType.Name.Replace("&", "") == node.DataType.Name.Replace("&", "");
        }

        public void ConnectTo(DigraphNode node)
        {
            if (!CanConnectTo(node))
                throw new ArgumentException("Unable to connect to the specified node.");

            if (Direction == NodeDirection.Input && _connections.Count != 0)
                DisconnectFrom(_connections[0]);
            else if (node.Direction == NodeDirection.Input && node._connections.Count != 0)
                node.DisconnectFrom(node._connections[0]);

            _connections.Add(node);
            node._connections.Add(this);
        }

        public void DisconnectFrom(DigraphNode node)
        {
            if (!_connections.Contains(node))
                throw new ArgumentException("Unable to disconnec from the specified node. No connection exists.");

            node._connections.Remove(this);
            _connections.Remove(node);
        }
    }
}
