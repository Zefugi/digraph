using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Digraph.Dom
{
    public interface IPublishedChangeEventArgs
    {
        bool Is<T>();
    }
}
