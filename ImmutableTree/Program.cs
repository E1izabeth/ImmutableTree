using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableTree
{
    class Program
    {
        static void Main()
        {
            var rnd = new Random();
            var max = 100;
            var root = ImmutableBinaryTree.Create(max / 2);
            Enumerable.Range(0, 100).Select(n => rnd.Next())
                      .ForEach(n => root = root.Add(n));

            root.Enumerate().ForEach(n => Console.WriteLine(n));
        }
    }
}
