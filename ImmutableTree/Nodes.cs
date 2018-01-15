using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableTree
{
    public abstract class ImmutableBinaryTreeNode<T> : INode<T>
    {
        public T Data { get; private set; }

        public ImmutableBinaryTreeNode(T data)
        {
            this.Data = data;
        }
    }

    public class NormalNode<T> : ImmutableBinaryTreeNode<T>
    {
        public NormalNode<T> Left { get; private set; }
        public NormalNode<T> Right { get; private set; }

        public NormalNode(T data, NormalNode<T> left, NormalNode<T> right)
            : base(data)
        {
            this.Left = left;
            this.Right = right;
        }
    }

    public abstract class ActiveNode<T> : ImmutableBinaryTreeNode<T>, IBinaryTreeActiveNode<T>
    {
        public NormalNode<T> Left { get; private set; }
        public NormalNode<T> Right { get; private set; }
        public ActiveParentNode<T> Parent { get; private set; }

        public ActiveNode(T data, NormalNode<T> left, NormalNode<T> right, ActiveParentNode<T> parent)
            : base(data)
        {
            this.Left = left;
            this.Right = right;
            this.Parent = parent;
        }

        protected ActiveParentNode<T> RecreateToActiveParentRightBranch() { return this.RecreateToActiveParentRightBranchImpl(); }

        protected abstract ActiveParentNode<T> RecreateToActiveParentRightBranchImpl();

        protected ActiveParentNode<T> RecreateToActiveParentLeftBranch() { return this.RecreateToActiveParentLeftBranchImpl(); }

        protected abstract ActiveParentNode<T> RecreateToActiveParentLeftBranchImpl();

        IBinaryTreeActiveNode<T> IBinaryTreeActiveNode<T>.CreateLeft(T data)
        {
            return new ActiveNodeLeftBranch<T>(data, null, null, this.RecreateToActiveParent());
        }

        IBinaryTreeActiveNode<T> IBinaryTreeActiveNode<T>.CreateRight(T data)
        {
            return new ActiveNodeRightBranch<T>(data, null, null, this.RecreateToActiveParent());
        }

        IBinaryTreeActiveNode<T> IBinaryTreeActiveNode<T>.GoUp()
        {
            throw new NotImplementedException();
        }

        IBinaryTreeActiveNode<T> IBinaryTreeActiveNode<T>.GoLeft()
        {
            throw new NotImplementedException();
        }

        IBinaryTreeActiveNode<T> IBinaryTreeActiveNode<T>.GoRight()
        {
            throw new NotImplementedException();
        }

        IBinaryTreeActiveNode<T> IBinaryTreeActiveNode<T>.DropLeft()
        {
            throw new NotImplementedException();
        }

        IBinaryTreeActiveNode<T> IBinaryTreeActiveNode<T>.DropRight()
        {
            throw new NotImplementedException();
        }
    }

    public class ActiveNodeLeftBranch<T> : ActiveNode<T>
    {
        public ActiveNodeLeftBranch(T data, NormalNode<T> left, NormalNode<T> right, ActiveParentNode<T> parent)
            : base(data, left, right, parent) { }

        protected override ActiveParentNode<T> RecreateToActiveParentRightBranchImpl()
        {
            return new ActiveParentRightBranch<T>(this.Data, this.left, this.Parent);
        }
    }

    public class ActiveNodeRightBranch<T> : ActiveNode<T>
    {
        public ActiveNodeRightBranch(T data, NormalNode<T> left, NormalNode<T> right, ActiveParentNode<T> parent)
            : base(data, left, right, parent) { }


        protected override ActiveParentNode<T> RecreateToActiveParentImpl()
        {
            return new ActiveParentLeftBranch<T>(this.Data, this.Right, this.Parent);
        }
    }

    public class ActiveParentNode<T> : ImmutableBinaryTreeNode<T>
    {
        public ActiveParentNode(T data)
            : base(data)
        {
        }
    }

    public class ActiveParentLeftBranch<T> : ActiveParentNode<T>
    {
        public NormalNode<T> Right { get; private set; }
        public ActiveParentLeftBranch<T> Parent { get; private set; }

        public ActiveParentLeftBranch(T data, NormalNode<T> right, ActiveParentNode<T> parent)
            : base(data)
        {
            this.data = data;
            this.right = right;
            this.parent = parent;
        }
    }

    public class ActiveParentRightBranch<T> : ActiveParentNode<T>
    {
        public NormalNode<T> Left { get; private set; }
        public ActiveParentNode<T> Parent { get; private set; }

        public ActiveParentRightBranch(T data, NormalNode<T> left, ActiveParentNode<T> parent)
            : base(data)
        {
            this.Left = right;
            this.Parent = parent;
        }
    }
}
