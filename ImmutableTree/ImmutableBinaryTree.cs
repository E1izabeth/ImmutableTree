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
            return new ActiveNode<T>(data, null, null);
        }

        public static IBinaryTreeActiveNode<T> Add<T>(this IBinaryTreeActiveNode<T> root, T data)
            where T : IComparable<T>
        {
            IBinaryTreeActiveNode<T> newRoot = root;
            IBinaryTreeActiveNode<T> currNode = root;
            while(true)
            {
                if(currNode.Data.CompareTo(data)>0)
                {
                    if(currNode.Left == null)
                    {
                        newRoot = currNode.CreateLeft(data);
                        break;
                    }
                    else
                    {
                        currNode = currNode.Left;
                    }
                }
                else
                {
                    if (currNode.Right == null)
                    {
                        newRoot = currNode.CreateRight(data);
                        break;
                    }
                    else
                    {
                        currNode = currNode.Right;
                    }
                }
            }
            
            return newRoot;
        }

        public static IEnumerable<T> Enumerate<T>(this IBinaryTreeActiveNode<T> root)
        {
            List<T> MyTree = new List<T>();

            if (root.Left == null && root.Right == null)
            {
                MyTree.Add(root.Data);
            }
            else
            {
                if (root.Left != null)
                {
                    root.Left.Enumerate();
                }
                if (root.Right != null)
                {
                    root.Right.Enumerate();
                }
                MyTree.Add(root.Data);
            }

            return MyTree;
        }
    }
}
