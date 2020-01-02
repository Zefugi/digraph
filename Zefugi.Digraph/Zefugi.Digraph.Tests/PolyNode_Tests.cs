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
        public void FromJson_Test() { ToString_Test(); }

        [Test]
        public void Parent_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };
            var beta = new PolyNode() { Name = "Beta" };

            root.Add(alpha);
            alpha.Add(beta);

            Assert.AreEqual(beta.Parent, alpha);
            Assert.AreEqual(alpha.Parent, root);
            Assert.IsNull(root.Parent);
        }

        [Test]
        public void Name_Test()
        {
            var alpha = new PolyNode();
            var beta = new PolyNode() { Name = "Beta" };

            Assert.AreNotEqual(alpha.Name, beta.Name);
            alpha.Name = beta.Name;
            Assert.AreEqual(alpha.Name, beta.Name);
        }

        [Test]
        public void Count_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };
            var beta = new PolyNode() { Name = "Beta" };

            Assert.AreEqual(root.Count, 0);
            root.Add(alpha);
            Assert.AreEqual(root.Count, 1);
            root.Add(beta);
            Assert.AreEqual(root.Count, 2);
        }

        [Test]
        public void Path_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };
            var beta = new PolyNode() { Name = "Beta" };

            root.Add(alpha);
            root.Add(beta);

            Assert.AreEqual(alpha.Path, "Root/Alpha");
            Assert.AreEqual(beta.Path, "Root/Beta");
        }

        [Test]
        public void Root_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };
            var beta = new PolyNode() { Name = "Beta" };

            root.Add(alpha);
            alpha.Add(beta);

            Assert.AreEqual(root.Root, root);
            Assert.AreEqual(alpha.Root, root);
            Assert.AreEqual(beta.Root, root);
            Assert.AreNotEqual(alpha.Root, alpha);
            Assert.NotNull(alpha.Root);
            Assert.NotNull(root.Root);
        }

        [Test]
        public void Add_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };
            var beta = new PolyNode() { Name = "Beta" };

            root.Add(alpha);
            root.Add(beta);

            Assert.AreEqual(root.Count, 2);
            Assert.AreEqual(alpha.Parent, root);
            Assert.AreEqual(beta.Parent, root);
            Assert.AreEqual(alpha, root["Alpha"]);
            Assert.AreEqual(beta, root["Beta"]);
            Assert.AreEqual(alpha.Path, "Root/Alpha");
        }

        [Test]
        public void Remove_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };

            root.Add(alpha);
            root.Remove(alpha);

            Assert.AreEqual(root.Count, 0);
            Assert.AreEqual(alpha.Parent, null);
            Assert.IsFalse(root.Contains("Alpha"));
            Assert.AreEqual(alpha.Path, "Alpha");
        }

        [Test]
        public void Clear_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };
            var beta = new PolyNode() { Name = "Beta" };

            root.Add(alpha);
            root.Add(beta);

            root.Remove(alpha);

            Assert.AreEqual(root.Count, 1);
        }

        [Test]
        public void Contains_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };

            root.Add(alpha);

            Assert.IsTrue(root.Contains(alpha.Name));
        }

        [Test]
        public void GetEnumerator_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };
            var beta = new PolyNode() { Name = "Beta" };

            root.Add(alpha);
            root.Add(beta);

            List<PolyNode> list = new List<PolyNode>();
            using (var ptr = root.GetEnumerator())
                while (ptr.MoveNext())
                    list.Add(ptr.Current);

            Assert.AreEqual(list.Count, 2);
            Assert.IsTrue(list.Contains(alpha));
            Assert.IsTrue(list.Contains(beta));
        }

        [Test]
        public void Indexer_Int32_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };
            var beta = new PolyNode() { Name = "Beta" };

            root.Add(alpha);
            root.Add(beta);

            List<PolyNode> list = new List<PolyNode>();
            list.Add(root[0]);
            list.Add(root[1]);

            Assert.AreEqual(list.Count, 2);
            Assert.IsTrue(list.Contains(alpha));
            Assert.IsTrue(list.Contains(beta));
        }

        [Test]
        public void Indexer_String_Test()
        {
            var root = new PolyNode() { Name = "Root" };
            var alpha = new PolyNode() { Name = "Alpha" };
            var beta = new PolyNode() { Name = "Beta" };

            root.Add(alpha);
            root.Add(beta);

            List<PolyNode> list = new List<PolyNode>();
            list.Add(root["Alpha"]);
            list.Add(root["Beta"]);

            Assert.AreEqual(list.Count, 2);
            Assert.IsTrue(list.Contains(alpha));
            Assert.IsTrue(list.Contains(beta));
        }

        [Test]
        public void ToString_Test()
        {
            var root = new PolyNode();
            var a = root.ToString();
            root.Add(new PolyNode() { Name = "Alpha" });
            var b = root.ToString();

            var reA = PolyNode.FromJson<PolyNode>(a);
            var aa = reA.ToString();
            var reB = PolyNode.FromJson<PolyNode>(b);
            var bb = reB.ToString();

            Assert.AreEqual(a, aa);
            Assert.AreEqual(b, bb);
            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(a, "");
        }
    }
}
