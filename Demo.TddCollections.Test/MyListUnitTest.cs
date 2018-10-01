using System;
using Demo.TddCollections.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.TddCollections.Test
{
    [TestClass]
    public class MyListUnitTest
    {

        [TestMethod]
        public void MyListCtorTest()
        {
            MyList<int> list = new MyList<int>();
            Assert.IsNotNull(list);
            Assert.AreEqual(0, list.Count);
            Assert.AreEqual(false, list.IsReadOnly);

            MyList<string> listA = new MyList<string>();
            Assert.IsNotNull(listA);
            Assert.AreEqual(0, listA.Count);
            Assert.AreEqual(false, listA.IsReadOnly);
        }

        [TestMethod]
        public void MyListAddTest()
        {
            MyList<int> list = new MyList<int>();
            Assert.IsNotNull(list);
            Assert.AreEqual(0, list.Count);
            list.Add(100);
            Assert.AreEqual(1, list.Count);
            for (int i = 0; i < 10000; ++i)
            {
                list.Add(i);
            }
            Assert.AreEqual(10001, list.Count);
        }

        [TestMethod]
        public void MyListItemTest_Get()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MyList<int> list;
            InitList(ref arr, out list);

            for (int i = 0; i < arr.Length; ++i)
            {
                Assert.AreEqual(arr[i], list[i]);
            }
        }
        [TestMethod]
        public void MyListItemTest_Set()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MyList<int> list;
            InitList(ref arr, out list);

            list[0] = 100;
            Assert.AreEqual(list[0], 100);
            list[9] = 100;
            Assert.AreEqual(list[9], 100);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void MyListItemTest_GetException_1()
        {
            MyList<int> list = new MyList<int>();
            Assert.AreEqual(list[0], 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void MyListItemTest_GetException_2()
        {
            MyList<int> list = new MyList<int>();
            Assert.AreEqual(list[-1], 1);
        }


        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void MyListItemTest_GetException_3()
        {
            MyList<int> list = new MyList<int>();
            list[-1] = 100;
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void MyListItemTest_GetException_4()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MyList<int> list;
            InitList(ref arr, out list);
            list[10] = 100;
        }

        private static void InitList(ref int[] arr, out MyList<int> list)
        {
            list = new MyList<int>();
            for (int i = 0; i < arr.Length; ++i)
            {
                list.Add(arr[i]);
            }
        }

        [TestMethod]
        public void MyListClearTest()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MyList<int> list;
            InitList(ref arr, out list);
            Assert.AreEqual(arr.Length, list.Count);
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }
    }
}
