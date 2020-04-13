using System;

namespace AVL_Tree
{
    public class AvlTree<T> where T : IComparable
    {
        public AvlTreeNode<T> Root { get; private set; }

        public void Add(T item)
        {
            if (Root == null)
            {
                Root = new AvlTreeNode<T>(item);
                return;
            }

            var insertedNode = Root.InsertChild(item);
            if (insertedNode != null)
            {
                CheckAndFixBalanceForTree(insertedNode, true);
            }
        }

        public void Remove(T item)
        {
            var node = Find(item);
            if (node != null)
            {
                // Node exists, delete it
                if (node.IsLeaf)
                {
                    if (node.Parent.LeftChild == node)
                    {
                        node.Parent.LeftChild = null;
                    }
                    else if (node.Parent.RightChild == node)
                    {
                        node.Parent.RightChild = null;
                    }
                    return;
                }

                if (node.OnlyOneChild)
                {
                    if (node.LeftChild != null)
                    {
                        if (node.Parent.LeftChild == node)
                        {
                            node.Parent.LeftChild = node.LeftChild;
                        }
                        else if (node.Parent.RightChild == node)
                        {
                            node.Parent.RightChild = node.LeftChild;
                        }
                        return;
                    }

                    if (node.RightChild != null)
                    {
                        if (node.Parent.LeftChild == node)
                        {
                            node.Parent.LeftChild = node.RightChild;
                        }
                        else if (node.Parent.RightChild == node)
                        {
                            node.Parent.RightChild = node.RightChild;
                        }
                    }
                }
                else
                {
                    var inOrderPredecessor = FindInOrderPredecessor(node);
                    SwapNodes(inOrderPredecessor, node);
                    Remove(node.Data);
                }
            }
        }

        public AvlTreeNode<T> FindInOrderPredecessor(AvlTreeNode<T> node)
        {
            var nextNodeToCheck = node.LeftChild;
            while (true)
            {
                if (nextNodeToCheck.RightChild != null)
                {
                    nextNodeToCheck = nextNodeToCheck.RightChild;
                }
                else
                {
                    return nextNodeToCheck;
                }
            }
        }

        private void SwapNodes(AvlTreeNode<T> node1, AvlTreeNode<T> node2)
        {
            var node1TempLeftChild = node1.LeftChild;
            var node1TempRightChild = node1.RightChild;
            var node2TempLeftChild = node2.LeftChild;
            var node2TempRigtChild = node2.RightChild;
            var node1TempParent = node1.Parent;
            var node2TempParent = node2.Parent;
        }

        public AvlTreeNode<T> Find(T item)
        {
            return FindInNode(Root, item);
        }

        public void CheckAndFixBalanceForTree(AvlTreeNode<T> node, bool recurse = false)
        {
            var leftNodeHeight = node.LeftChild?.GetHeight() ?? 0;
            var rightNodeHeight = node.RightChild?.GetHeight() ?? 0;
            var leftRightDifference = leftNodeHeight - rightNodeHeight;
            if (leftRightDifference > 1)
            {
                // Node's subtree is left heavy
                // Check if we need to do a right or left right rotation
                var leftGrandChildHeight = node.LeftChild?.LeftChild?.GetHeight() ?? 0;
                var rightGrandChildHeight = node.LeftChild?.RightChild?.GetHeight() ?? 0;
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
                var rightGrandchildHeight = node.RightChild?.RightChild?.GetHeight() ?? 0;
                var leftGrandChildHeight = node.RightChild?.LeftChild?.GetHeight() ?? 0;
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
            else
            {
                if (node.Parent != null)
                {
                    node = node.Parent;
                }
                else
                {
                    return;
                }
            }

            if (recurse)
            {
                CheckAndFixBalanceForTree(node, true);
            }
        }

        private AvlTreeNode<T> RotateRight(AvlTreeNode<T> node)
        {
            var temp = node.LeftChild;
            node.LeftChild = temp.RightChild;
            if (node.LeftChild != null)
            {
                node.LeftChild.Parent = node;
            }
            temp.RightChild = node;
            if (node.Parent != null)
            {
                temp.Parent = node.Parent;
                if (temp.Parent.Data.CompareTo(temp.Data) == 1)
                {
                    temp.Parent.LeftChild = temp;
                }
                else
                {
                    temp.Parent.RightChild = temp;
                }
            }
            else
            {
                temp.Parent = null;
                Root = temp;
            }
            node.Parent = temp;
            return node;
        }

        private AvlTreeNode<T> RotateLeft(AvlTreeNode<T> node)
        {
            var temp = node.RightChild;
            node.RightChild = temp.LeftChild;
            if (node.RightChild != null)
            {
                node.RightChild.Parent = node;
            }
            temp.LeftChild = node;
            if (node.Parent != null)
            {
                temp.Parent = node.Parent;
                if (temp.Parent.Data.CompareTo(temp.Data) == 1)
                {
                    temp.Parent.LeftChild = temp;
                }
                else
                {
                    temp.Parent.RightChild = temp;
                }
            }
            else
            {
                temp.Parent = null;
                Root = temp;
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

                if (comparisonResult.Equals(-1))
                {
                    if (node.RightChild != null)
                    {
                        node = node.RightChild;
                        continue;
                    }
                }
                else if (comparisonResult.Equals(1))
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
