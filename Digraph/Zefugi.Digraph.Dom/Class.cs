using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Digraph.Dom
{
    public class Class
    {
        public readonly Publisher Publisher = new Publisher();

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
