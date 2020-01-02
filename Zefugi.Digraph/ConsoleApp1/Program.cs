using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new Zefugi.Digraph.PolyNode()
            {
                Name = "Root",
            };
            var alpha = new Zefugi.Digraph.PolyNode()
            {
                Name = "Alpha",
            };
            root.Add(alpha);
            Console.WriteLine(root);
            Console.WriteLine();

            var copy = Zefugi.Digraph.PolyNode.FromJson(root.ToString());
            Console.WriteLine(copy);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
