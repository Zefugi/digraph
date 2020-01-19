using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Zefugi.Digraph.Dom.Tests
{
    [TestFixture]
    public class Connector_Tests
    {
        [Test]
        [TestCase(ConnectorDirection.Output, "data", 2, ConnectorDirection.Input, "data", 2, null, ConnectionResult.Success)]
        [TestCase(ConnectorDirection.Output, "data", 0, ConnectorDirection.Input, "data", 2, null, ConnectionResult.Success)]
        [TestCase(ConnectorDirection.Output, "data", 2, ConnectorDirection.Input, "data", 0, null, ConnectionResult.Success)]
        [TestCase(ConnectorDirection.Output, "data", 2, ConnectorDirection.Output, "data", 2, ConnectionResult.InvalidDirection, null)]
        [TestCase(ConnectorDirection.Output, "data", 2, ConnectorDirection.Input, "flow", 2, ConnectionResult.InvalidType, null)]
        [TestCase(ConnectorDirection.Output, "data", 2, ConnectorDirection.Output, "flow", 2, ConnectionResult.InvalidDirection | ConnectionResult.InvalidType, null)]
        [TestCase(ConnectorDirection.Output, "data", 1, ConnectorDirection.Input, "data", 2, null, ConnectionResult.MaxConnectionsReached)]

        public void TryConnect_Test(
            ConnectorDirection dirA, string typeA, int maxA,
            ConnectorDirection dirB, string typeB, int maxB,
            ConnectionResult? expectedResultA = null, ConnectionResult? expectedResultB = null)
        {
            var a = new Connector()
            {
                Direction = dirA,
                Type = typeA,
                MaxConnections = maxA,
            };
            var b = new Connector()
            {
                Direction = dirB,
                Type = typeB,
                MaxConnections = maxB,
            };
            var c = new Connector()
            {
                Direction = dirB,
                Type = typeB,
                MaxConnections = maxB,
            };
            var resA = a.TryConnect<Connection>(b);
            var resB = a.TryConnect<Connection>(c);
            Assert.IsFalse(expectedResultA == null && expectedResultB == null);
            if (expectedResultA != null)
                Assert.AreEqual(expectedResultA, resA);
            if(expectedResultB != null)
                Assert.AreEqual(expectedResultB, resB);
        }
    }
}
