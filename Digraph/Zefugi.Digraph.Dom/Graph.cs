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

        public readonly ObservableList<Node> Nodes = new ObservableList<Node>();

        public Graph()
        {
            Nodes.ListChanged += Nodes_ListChanged;
        }

        private void Nodes_ListChanged(object sender, ObservableList<Node>.ChangeEventArgs e)
        {
            if(e.OldItems != null)
                foreach (var item in e.OldItems)
                    item.Graph = null;
            if(e.NewItems != null)
                foreach (var item in e.NewItems)
                    item.Graph = this;
        }
    }
}
