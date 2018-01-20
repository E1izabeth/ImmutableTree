using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableTree
{
    public abstract class ImmutableBinaryTreeNode<T>
    {
        public T Data { get; private set; }

        public ImmutableBinaryTreeNode(T data)
        {
            this.Data = data;
        }


    }

    public abstract class ActiveParentNode<T> : ImmutableBinaryTreeNode<T>, INode<T>
    {
        public ActiveParentNode<T> Up { get; private set; }

        protected ActiveParentNode(T data, ActiveParentNode<T> up)
            : base(data)
        {
            this.Up = up;
        }

        public abstract ActiveNode<T> RecreateAsActive(NormalNode<T> secondChild);
    }

    public class ActiveParentLeftNode<T> : ActiveParentNode<T>
    {
        public NormalNode<T> Left { get; private set; }

        public ActiveParentLeftNode(T data, ActiveParentNode<T> up, NormalNode<T> left)
            : base(data, up)
        {
            this.Left = left;
        }

        public override ActiveNode<T> RecreateAsActive(NormalNode<T> secondChild)
        {
            return new ActiveNode<T>(this.Data, this.Up, this.Left, secondChild);
        }
    }

    public class ActiveParentRightNode<T> : ActiveParentNode<T>
    {
        public NormalNode<T> Right { get; private set; }

        public ActiveParentRightNode(T data, ActiveParentNode<T> up, NormalNode<T> right)
            : base(data, up)
        {
            this.Right = right;
        }

        public override ActiveNode<T> RecreateAsActive(NormalNode<T> secondChild)
        {
            return new ActiveNode<T>(this.Data, this.Up, secondChild, this.Right);
        }
    }

    public class ActiveNode<T> : ImmutableBinaryTreeNode<T>, IBinaryTreeActiveNode<T>
    {
        public ActiveParentNode<T> Up { get; private set; }
        public NormalNode<T> Left { get; private set; }
        public NormalNode<T> Right { get; private set; }

        INode<T> IBinaryTreeActiveNode<T>.Parent { get { return this.Up; } }
        INormalNode<T> INormalNode<T>.Left { get { return this.Left; } }
        INormalNode<T> INormalNode<T>.Right { get { return this.Right; } }

        public ActiveNode(T data, ActiveParentNode<T> up, NormalNode<T> left, NormalNode<T> right)
            : base(data)
        {
            this.Up = up;
            this.Left = left;
            this.Right = right;
        }

        private NormalNode<T> RecreateAsNormalNode()
        {
            return new NormalNode<T>(this.Data, this.Left, this.Right);
        }

        private ActiveParentRightNode<T> RecreateAsParentLeftNode()
        {
            return new ActiveParentRightNode<T>(this.Data, this.Up, this.Right);
        }

        private ActiveParentLeftNode<T> RecreateAsParentRightNode()
        {
            return new ActiveParentLeftNode<T>(this.Data, this.Up, this.Left);
        }

        public IBinaryTreeActiveNode<T> GoUp()
        {
            return this.Up.RecreateAsActive(this.RecreateAsNormalNode());
        }

        public IBinaryTreeActiveNode<T> GoLeft()
        {
            var recreatedCurrent = this.RecreateAsParentLeftNode();
            return new ActiveNode<T>(this.Left.Data, recreatedCurrent, this.Left.Left, this.Left.Right);
        }

        public IBinaryTreeActiveNode<T> GoRight()
        {
            var recreatedCurrent = this.RecreateAsParentRightNode();
            return new ActiveNode<T>(this.Right.Data, recreatedCurrent, this.Right.Left, this.Right.Right);
        }

        public IBinaryTreeActiveNode<T> CreateLeft(T data)
        {
            var recreatedParent = this.RecreateAsParentLeftNode();
            return new ActiveNode<T>(data, recreatedParent, null, null);
        }

        public IBinaryTreeActiveNode<T> CreateRight(T data)
        {
            var recreatedParent = this.RecreateAsParentRightNode();
            return new ActiveNode<T>(data, recreatedParent, null, null);
        }

        public IBinaryTreeActiveNode<T> DropLeft()
        {
            return new ActiveNode<T>(this.Data, this.Up, null, this.Right);
        }

        public IBinaryTreeActiveNode<T> DropRight()
        {
            return new ActiveNode<T>(this.Data, this.Up, this.Right, null);
        }
    }

    public class NormalNode<T> : ImmutableBinaryTreeNode<T>, INormalNode<T>
    {
        public NormalNode<T> Left { get; private set; }
        public NormalNode<T> Right { get; private set; }

        INormalNode<T> INormalNode<T>.Left { get { return this.Left; } }
        INormalNode<T> INormalNode<T>.Right { get { return this.Right; } }

        public NormalNode(T data, NormalNode<T> left, NormalNode<T> right)
            : base(data)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}
