using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Zefugi.Digraph.Dom.Tests
{
    [TestFixture]
    public class Graph_Tests
    {
        [Test]
        public void Nodes_Add_Test()
        {
            var n = new Node();

            var graph = new Graph();

            graph.Nodes.Add(n);

            Assert.AreEqual(n.Graph, graph);
        }

        [Test]
        public void Nodes_Remove_Test()
        {
            var n = new Node();

            var graph = new Graph();

            graph.Nodes.Add(n);
            graph.Nodes.Remove(n);

            Assert.IsNull(n.Graph);
        }

        [Test]
        public void Nodes_Clear_Test()
        {
            var n1 = new Node();
            var n2 = new Node();

            var graph = new Graph();

            graph.Nodes.Add(n1);
            graph.Nodes.Add(n2);
            graph.Nodes.Clear();

            Assert.IsNull(n1.Graph);
            Assert.IsNull(n2.Graph);
        }
    }
}
