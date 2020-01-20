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

        public readonly ObservableList<Connector> Connectors = new ObservableList<Connector>();

        public Node()
        {
            Connectors.ListChanged += Connectors_ListChanged;
        }

        private void Connectors_ListChanged(object sender, ObservableList<Connector>.ChangeEventArgs e)
        {
            if(e.OldItems != null)
                foreach (var item in e.OldItems)
                    item.Node = null;
            if(e.NewItems != null)
                foreach (var item in e.NewItems)
                    item.Node = this;
        }
    }
}
