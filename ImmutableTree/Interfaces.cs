using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableTree
{
    public interface IBinaryTreeActiveNode<T>
    {
        T Data { get; }

        //IBinaryTreeActiveNode<T> Left { get; }
        //IBinaryTreeActiveNode<T> Right { get; }

        IBinaryTreeActiveNode<T> GoUp();
        IBinaryTreeActiveNode<T> GoLeft();
        IBinaryTreeActiveNode<T> GoRight();

        IBinaryTreeActiveNode<T> CreateLeft(T data);
        IBinaryTreeActiveNode<T> CreateRight(T data);

        IBinaryTreeActiveNode<T> DropLeft();
        IBinaryTreeActiveNode<T> DropRight();
    }

    public interface INode<T>
    {
        T Data { get; }
    }
}
