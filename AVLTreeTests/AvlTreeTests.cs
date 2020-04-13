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

            Assert.AreEqual(newTree.Root.Data, 14);
        }
    }
}
