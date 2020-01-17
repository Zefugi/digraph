using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zefugi.Digraph.Dom
{
    public class Module
    {
        public readonly Publisher Publisher = new Publisher();
        public readonly List<Class> Classes = new List<Class>();

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                var oldValue = _name;
                _name = value;
                Publisher.PublishChange<string>(this, nameof(Name), oldValue, value);
            }
        }
    }
}