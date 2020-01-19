using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Digraph.Dom
{
    [Flags]
    public enum ConnectionResult
    {
        Success = 0x00,

        InvalidDirection = 0x01,
        InvalidType = 0x02,
        MaxConnectionsReached = 0x04,
    }
}
