using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Digraph.Dom
{
    public class Publisher
    {
        public event EventHandler<IPublishedChangeEventArgs> ChangePublished;

        public void PublishChange<T>(object sender, string name, T oldValue, T newValue)
        {
            ChangePublished?.Invoke(sender, new PublishedChangeEventArgs<T>(name, oldValue, newValue));
        }
    }
}
