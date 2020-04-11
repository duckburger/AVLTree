using System;

namespace AVL_Tree
{
    public class AvlTreeNode<T> where T : IComparable
    {
        public T Data { get; set; }
        public AvlTreeNode<T> LeftChild { get; set; }
        public AvlTreeNode<T> RightChild { get; set; }
        public AvlTreeNode<T> Parent { get; set; }

        public AvlTreeNode(T data)
        {
            Data = data;
        }

        public int GetHeight()
        {
            return Math.Max(LeftChild?.GetHeight() ?? 0, RightChild?.GetHeight() ?? 0) + 1;
        }

        internal AvlTreeNode<T> InsertChild(T item)
        {
            int comparisonResult = Data.CompareTo(item);
            // This node is larger
            if (comparisonResult == 1)
            {
                if (LeftChild == null)
                {
                    LeftChild = new AvlTreeNode<T>(item);
                    LeftChild.Parent = this;
                    return LeftChild;
                }
                return LeftChild.InsertChild(item);
            }
            else if (comparisonResult == -1)
            {
                if (RightChild == null)
                {
                    RightChild = new AvlTreeNode<T>(item);
                    RightChild.Parent = this;
                    return RightChild;
                }
                return RightChild.InsertChild(item);
            }

            return null;
        }
    }
}
