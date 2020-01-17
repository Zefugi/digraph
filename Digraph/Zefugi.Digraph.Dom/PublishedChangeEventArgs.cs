using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Digraph.Dom
{
    public class PublishedChangeEventArgs<T> : IPublishedChangeEventArgs
    {
        public readonly string Name;
        public readonly T OldValue;
        public readonly T NewValue;

        public PublishedChangeEventArgs(string name, T oldValue, T newValue)
        {
            Name = name;
            oldValue = OldValue;
            newValue = NewValue;
        }

        public bool Is<MatchType>() { return OldValue is MatchType; }
    }
}
