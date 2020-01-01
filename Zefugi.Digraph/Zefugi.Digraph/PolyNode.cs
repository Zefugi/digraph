using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zefugi.Digraph
{
    public class PolyNode : IEnumerable<PolyNode>
    {
        private PolyNode _parent;

        private string _name = "Unnamed";

        private Dictionary<string, PolyNode> _nodes = new Dictionary<string, PolyNode>();

        public PolyNode Parent
        {
            get { return _parent; }
            internal set
            {
                _parent?.Remove(this);
                _parent = value;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == null)
                    throw new Exception("Name can not have a null value.");
                if(_parent != null)
                {
                    string lowName = value.ToLower();
                    if (_parent.Contains(lowName))
                        throw new Exception("A node with that name already exiists.");
                }
                _name = value;
            }
        }

        public int Count { get { return _nodes.Count; } }

        public string Path
        {
            get
            {
                Stack<PolyNode> stack = new Stack<PolyNode>();
                StringBuilder path = new StringBuilder();
                var node = this;
                while(node != null)
                {
                    stack.Push(node);
                    node = node._parent;
                }
                path.Append(stack.Pop().Name);
                while (stack.Count > 0)
                {
                    path.Append("/");
                    path.Append(stack.Pop().Name);
                }
                return path.ToString();
            }
        }

        public PolyNode Root
        {
            get
            {
                var node = this;
                while (node.Parent != null)
                    node = node.Parent;
                return node;
            }
        }

        public void Add(PolyNode node)
        {
            string lowName = node.Name.ToLower();
            if (_nodes.ContainsKey(lowName))
                throw new Exception("A node with that name alread exists.");
            _nodes.Add(lowName, node);
            node.Parent = this;
        }

        public void Remove(PolyNode node)
        {
            string lowName = node.Name.ToLower();
            if (!_nodes.ContainsKey(lowName))
                throw new Exception("No such node found.");
            _nodes.Remove(lowName);
            node.Parent = null;
        }

        public void Clear()
        {
            using (var ptr = _nodes.GetEnumerator())
                while (ptr.MoveNext())
                    ptr.Current.Value.Parent = null;
            _nodes.Clear();
        }

        public bool Contains(string name)
        {
            return _nodes.ContainsKey(name.ToLower());
        }

        public IEnumerator<PolyNode> GetEnumerator()
        {
            return _nodes.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nodes.Values.GetEnumerator();
        }

        public PolyNode this[int index]
        {
            get
            {
                var ptr = _nodes.GetEnumerator();
                for(int i = 0; i < index; i++)
                {
                    if (!ptr.MoveNext())
                        return null;
                }
                return ptr.Current.Value;
            }
        }

        public PolyNode this[string name]
        {
            get
            {
                string lowName = name.ToLower();
                if(!_nodes.ContainsKey(lowName))
                    throw new Exception("No such node found.");
                return _nodes[lowName];
            }
        }

        public override string ToString()
        {
            return Name + " as Json:\n" + JsonConvert.SerializeObject(this);
        }
    }
}
