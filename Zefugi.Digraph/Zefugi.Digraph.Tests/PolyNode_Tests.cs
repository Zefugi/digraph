using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Zefugi.Digraph.Tests
{
    [TestFixture]
    public class PolyNode_Tests
    {
        [Test]
        public void ToString_Test()
        {
            var root = new PolyNode();
            root.Add(new PolyNode() { Name = "Root" });
            Debug.WriteLine(root);
        }
    }
}
