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
            Enumerable.Range(0, 100).Select(n => rnd.Next(max))
                      .ForEach(n => root = root.Add(n));

            root.Enumerate().ForEach(n => Console.WriteLine(n));
            //var root = ImmutableBinaryTree.Create<int>(0);
            //var t = root.CreateLeft(1)
            //            .CreateLeft(2)
            //            .CreateLeft(3)
            //            .GoUp()
            //            .CreateRight(4)
            //            .GoUp()
            //            .GoUp()
            //            .CreateRight(5)
            //            .CreateRight(6)
            //            .GoUp()
            //            .CreateLeft(7)
            //            .GoUp()
            //            .GoUp()
            //            .GoUp()
            //            .CreateRight(8)
            //            .CreateLeft(9)
            //            .GoUp()
            //            .CreateRight(10)
            //            .GoUp()
            //            .GoUp();

            INormalNode<int> r2 = root;
            var s = r2.CollectTree(n => new[] { n.Left, n.Right }.Where(x => x != null), n => n.Data.ToString());
            Console.WriteLine(s);
        }
    }
}
