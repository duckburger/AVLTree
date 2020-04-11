using System;

namespace AVL_Tree
{
    public class AvlTree<T> where T : IComparable
    {
        private AvlTreeNode<T> _root;

        public void Add(T item)
        {
            if (_root == null)
            {
                _root = new AvlTreeNode<T>(item);
                return;
            }

            var insertedNode = _root.InsertChild(item);
            if (insertedNode != null)
            {

            }
        }

        private void CheckTreeBalance(AvlTreeNode<T> node)
        {
            
        }

        public void Remove()
        {

        }

        public AvlTreeNode<T> Find(T item)
        {
            return FindInNode(_root.RightChild, item);
        }

        public void CheckAndFixBalance(AvlTreeNode<T> node)
        {
            var leftNodeHeight = node.LeftChild?.GetHeight() ?? 0;
            var rightNodeHeight = node.RightChild?.GetHeight() ?? 0;
            var leftRightDifference = leftNodeHeight - rightNodeHeight;
            if (leftRightDifference > 1)
            {
                // Node's subtree is left heavy
                // Check if we need to do a right or left right rotation
                var leftGrandChildHeight = node.LeftChild.LeftChild?.GetHeight() ?? 0;
                var rightGrandChildHeight = node.RightChild.RightChild?.GetHeight() ?? 0;
                if (leftGrandChildHeight > rightGrandChildHeight)
                {
                    // Doing a right rotation only
                    node = RotateRight(node);
                }
                else
                {
                    // Doing a left right rotation
                    node = RotateLeftRight(node);
                }
            }
            else if (leftRightDifference < -1)
            {
                // Node's subtree is right heavy
                // Check if we need to do a right or left right rotation
                var rightGrandchildHeight = node.RightChild.RightChild?.GetHeight() ?? 0;
                var leftGrandChildHeight = node.RightChild.LeftChild?.GetHeight() ?? 0;
                if (leftGrandChildHeight < rightGrandchildHeight)
                {
                    // Doing a left rotation only
                    node = RotateLeft(node);
                }
                else
                {
                    // Doing a right left rotation
                    node = RotateRightLeft(node);
                }
            }
        }

        private AvlTreeNode<T> RotateRight(AvlTreeNode<T> node)
        {
            var temp = node.LeftChild;
            node.LeftChild = temp.RightChild;
            temp.RightChild = node;
            if (node.Parent != null)
            {
                temp.Parent = node.Parent;
                temp.Parent.LeftChild = temp;
            }
            else
            {
                temp.Parent = null;
                _root = temp;
            }
            node.Parent = temp;
            return node;
        }

        private AvlTreeNode<T> RotateLeft(AvlTreeNode<T> node)
        {
            var temp = node.RightChild;
            node.RightChild = temp.LeftChild;
            temp.LeftChild = node;
            if (node.Parent != null)
            {
                temp.Parent = node.Parent;
                temp.Parent.RightChild = temp;
            }
            else
            {
                temp.Parent = null;
                _root = temp;
            }
            node.Parent = temp;
            return node;
        }

        private AvlTreeNode<T> RotateLeftRight(AvlTreeNode<T> node)
        {
            // First, rotate the right grandchild to the left
            RotateLeft(node.LeftChild);
            // Second, rotate myself to the right
            return RotateRight(node);
        }

        private AvlTreeNode<T> RotateRightLeft(AvlTreeNode<T> node)
        {
            // First, rotate the left grandchild to the right
            RotateRight(node.RightChild);
            // Second, rotate myself to the left
            return RotateLeft(node);
        }

        private AvlTreeNode<T> FindInNode(AvlTreeNode<T> node, T item)
        {
            while (true)
            {
                int comparisonResult = node.Data.CompareTo(item);
                if (comparisonResult == 0)
                {
                    return node;
                }

                if (comparisonResult.Equals(1))
                {
                    if (node.RightChild != null)
                    {
                        node = node.RightChild;
                        continue;
                    }
                }
                else if (comparisonResult.Equals(-1))
                {
                    if (node.LeftChild != null)
                    {
                        node = node.LeftChild;
                        continue;
                    }
                }

                break;
            }

            return null;
        }
    }
}
