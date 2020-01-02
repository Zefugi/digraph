using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zefugi.Digraph
{
    public class ObservablePolyNode : PolyNode
    {
        public event EventHandler<PolyNode> ChildNodeAdded;
        public event EventHandler<PolyNode> ChildNodeRemoved;
        public event EventHandler<PolyNode> ChildNodesCleared;
        public event EventHandler<PolyNode> ChildNodeUpdated;
        public event EventHandler<PolyNode> NodeRenamed;
        public event EventHandler<PolyNode> NodeUpdated;

        protected virtual void OnChildNodeAdded(PolyNode node) { }
        protected virtual void OnChildNodeRemoved(PolyNode node) { }
        protected virtual void OnChildNodesCleared(PolyNode node) { }
        protected virtual void OnChildNodeUpdated(PolyNode node) { }
        protected virtual void OnNodeRenamed(PolyNode node) { }
        protected virtual void OnNodeUpdated(PolyNode node) { }

        public new void Add(PolyNode node)
        {
            base.Add(node);
            OnChildNodeAdded(node);
            ChildNodeAdded(this, node);
        }

        public new void Remove(PolyNode node)
        {
            base.Remove(node);
            OnChildNodeRemoved(node);
            ChildNodeRemoved(this, node);
        }

        public new void Clear()
        {
            base.Clear();
            OnChildNodesCleared(this);
            ChildNodesCleared(this, this);
        }

        [JsonIgnore]
        public new string Name
        {
            get { return base.Name; }
            set
            {
                base.Name = value;
                OnNodeRenamed(this);
                NodeRenamed(this, this);
            }
        }

        internal void ChildUodated(PolyNode node)
        {
            OnChildNodeUpdated(node);
            ChildNodeUpdated(this, node);
        }

        public void Update()
        {
            OnNodeUpdated(this);
            NodeUpdated(this, this);
            if (Parent != null && Parent is ObservablePolyNode)
                (Parent as ObservablePolyNode).ChildUodated(this);
        }
    }
}
