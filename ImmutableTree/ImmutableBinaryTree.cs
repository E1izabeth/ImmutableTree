using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableTree
{
    public static class ImmutableBinaryTree
    {
        public static IBinaryTreeActiveNode<T> Create<T>(T data)
        {
            return new ActiveNode<T>(data, null, null, null);
        }

        public static IBinaryTreeActiveNode<T> Add<T>(this IBinaryTreeActiveNode<T> root, T data)
            where T : IComparable<T>
        {
            IBinaryTreeActiveNode<T> newActive = root;
            IBinaryTreeActiveNode<T> currNode = root;
            while (true)
            {
                if (currNode.Data.CompareTo(data) < 0)
                {
                    if (currNode.Left == null)
                    {
                        newActive = currNode.CreateLeft(data);
                        break;
                    }
                    else
                    {
                        currNode = currNode.GoLeft();
                    }
                }
                else if (currNode.Data.CompareTo(data) > 0)
                {
                    if (currNode.Right == null)
                    {
                        newActive = currNode.CreateRight(data);
                        break;
                    }
                    else
                    {
                        currNode = currNode.GoRight();
                    }
                }
                else
                {
                    break;
                }
            }

            while (newActive.Parent != null)
                newActive = newActive.GoUp();

            return newActive;
        }

        public static IEnumerable<T> Enumerate<T>(this INormalNode<T> root)
        {
            return root.Enumerate(new List<T>());
        }

        private static IEnumerable<T> Enumerate<T>(this INormalNode<T> root, List<T> list)
        {
            List<T> items = list; 

            if (root.Left != null)
            {
                items.AddRange(root.Left.Enumerate());
            }

            items.Add(root.Data);

            if (root.Right != null)
            {
                items.AddRange(root.Right.Enumerate());
            }

            return items;
        }
    }
}
