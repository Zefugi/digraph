using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zefugi.Digraph
{
    [JsonObject(IsReference = true)]
    public class PolyNode
    {
        public static PolyNode FromJson(string json)
        {
            if (json == null)
                throw new ArgumentNullException("json");
            try
            {
                return JsonConvert.DeserializeObject<PolyNode>(json);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid JSON string.", ex);
            }
        }

        [JsonProperty]
        private PolyNode _parent;

        [JsonProperty]
        private string _name = "Unnamed";

        [JsonProperty]
        private Dictionary<string, PolyNode> _nodes = new Dictionary<string, PolyNode>();

        [JsonIgnore]
        public PolyNode Parent
        {
            get { return _parent; }
            internal set
            {
                _parent?.Remove(this);
                _parent = value;
            }
        }

        [JsonIgnore]
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
                    if (_parent != null && _parent.Contains(lowName))
                        throw new Exception("A node with that name already exiists.");
                }
                _name = value;
            }
        }

        [JsonIgnore]
        public int Count { get { return _nodes.Count; } }

        [JsonIgnore]
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

        [JsonIgnore]
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
            if (node == null)
                throw new ArgumentNullException("node");
            string lowName = node.Name.ToLower();
            if (_nodes.ContainsKey(lowName))
                throw new Exception("A node with that name alread exists.");
            _nodes.Add(lowName, node);
            node.Parent = this;
        }

        public void Remove(PolyNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            string lowName = node.Name.ToLower();
            if (!_nodes.ContainsKey(lowName))
                throw new Exception("No such node found.");
            _nodes.Remove(lowName);
            node._parent = null;
        }

        public void Clear()
        {
            using (var ptr = _nodes.GetEnumerator())
                while (ptr.MoveNext())
                    ptr.Current.Value._parent = null;
            _nodes.Clear();
        }

        public bool Contains(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            return _nodes.ContainsKey(name.ToLower());
        }

        public IEnumerator<PolyNode> GetEnumerator()
        {
            return _nodes.Values.GetEnumerator();
        }

        [JsonIgnore]
        public PolyNode this[int index]
        {
            get
            {
                var ptr = _nodes.GetEnumerator();
                for(int i = 0; i <= index; i++)
                {
                    if (!ptr.MoveNext())
                        return null;
                }
                return ptr.Current.Value;
            }
        }

        [JsonIgnore]
        public PolyNode this[string name]
        {
            get
            {
                if (name == null)
                    throw new ArgumentNullException("name");
                string lowName = name.ToLower();
                if(!_nodes.ContainsKey(lowName))
                    throw new Exception("No such node found.");
                return _nodes[lowName];
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
