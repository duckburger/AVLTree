using AVL_Tree;
using NUnit.Framework;

namespace AvlTreeTests
{
    [TestFixture]
    public class AvlTreeTests
    {
        [Test]
        public void InsertAndRebalance3ItemsTest()
        {
            AvlTree<int> newTree = new AvlTree<int>();
            newTree.Add(25);
            newTree.Add(12);
            newTree.Add(14);

            Assert.AreEqual(14, newTree.Root.Data);
        }

        [Test]
        public void InsertAndRebalance22ItemTest()
        {
            AvlTree<int> newTree = new AvlTree<int>();
            newTree.Add(40);
            newTree.Add(30);
            newTree.Add(35);
            newTree.Add(20);
            newTree.Add(50);
            newTree.Add(60);
            newTree.Add(70);
            newTree.Add(45);
            newTree.Add(46);
            newTree.Add(41);
            newTree.Add(42);

            Assert.AreEqual(45, newTree.Root.Data);
        }

        [Test]
        public void InsertAndFindTest()
        {
            AvlTree<int> newTree = new AvlTree<int>();
            newTree.Add(40);
            newTree.Add(30);
            newTree.Add(35);
            newTree.Add(20);
            newTree.Add(50);
            newTree.Add(60);
            newTree.Add(70);
            newTree.Add(45);
            newTree.Add(46);
            newTree.Add(41);
            newTree.Add(42);
            AvlTreeNode<int> node = newTree.Find(70);

            Assert.IsNotNull(node);
        }
    }
}
