using System;
using System.Linq;
using NUnit.Framework;
using CustomGenericCollection;


namespace CustomerGenericCollection
{
    public class CustomGenericCollectionTests
    {
        [Test]
        public void Add_ToEmptyList()
        {
            var circularList = new CircularLinkedList<int>();
            const int node = 1;
            var expectedLength = circularList.Count + 1;

            circularList.Add(node);

            Assert.AreEqual(node, circularList[expectedLength - 1]);
            Assert.AreEqual(expectedLength, circularList.Count);
        }

        [Test]
        public void Add_ToList()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 44, 2});
            const int node = 541;
            var length = circularList.Count + 1;

            circularList.Add(node);

            Assert.AreEqual(node, circularList[length - 1]);
            Assert.AreEqual(length, circularList.Count);
        }

        [Test]
        public void AddRange_ToList()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 44, 2});
            var length = circularList.Count + 3;
            var array = new[] {3, 2, 1};

            circularList.AddRange(array);

            Assert.AreEqual(length, circularList.Count);
        }


        [Test]
        public void AddRange_InputCollectionIsNull_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 44, 2});

            Assert.Throws<ArgumentNullException>(
                () => circularList.AddRange(null));
        }

        [Test]
        public void AddFirst_ToEmptyList([Values("abraac", "assfasf")] string node)
        {
            var circularList = new CircularLinkedList<string>();
            var expectedLength = circularList.Count + 1;

            circularList.AddFirst(node);

            Assert.AreEqual(node, circularList[0]);
            Assert.AreEqual(expectedLength, circularList.Count);
        }

        [Test]
        public void AddFirst_ToList()
        {
            var circularList = new CircularLinkedList<string>(new[] {"box", "table", "pen"});
            const string node = "pencil";
            var length = circularList.Count + 1;

            circularList.AddFirst(node);

            Assert.AreEqual(node, circularList[0]);
            Assert.AreEqual(length, circularList.Count);
        }

        [Test]
        public void AddLast_ToEmptyList()
        {
            var circularList = new CircularLinkedList<string>();
            const string node = "Joshua";
            var length = circularList.Count + 1;

            circularList.AddLast(node);

            Assert.AreEqual(node, circularList[length - 1]);
            Assert.AreEqual(length, circularList.Count);
        }

        [Test]
        public void AddLast_ToList()
        {
            var circularList = new CircularLinkedList<string>(new[] {"1", "2", "3"});
            const string node = "4";
            var length = circularList.Count + 1;

            circularList.AddLast(node);

            Assert.AreEqual(node, circularList[length - 1]);
            Assert.AreEqual(length, circularList.Count);
        }

        [Test]
        public void AddBefore_WithValidNode()
        {
            var circularList = new CircularLinkedList<int>(new[] {1});
            var node = circularList.NodeAt(0);
            const int item = 2;
            var expectedLength = circularList.Count + 1;

            circularList.AddBefore(node, item);

            Assert.AreEqual(item, circularList[0]);
            Assert.AreEqual(expectedLength, circularList.Count);
        }

        [Test]
        public void AddBefore_WithInvalidNode_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {1});
            var node = new Node<int>(12);
            const int item = 2;

            Assert.Throws<ArgumentException>(() => circularList.AddBefore(node, item));
        }

        [Test]
        public void AddAfter_WithValidNode()
        {
            var circularList = new CircularLinkedList<int>(new[] {3, 1, 0});
            const int index = 0;
            const int item = 2;
            var node = circularList.NodeAt(index);
            var expectedLength = circularList.Count + 1;

            circularList.AddAfter(node, item);

            Assert.AreEqual(item, circularList[index + 1]);
            Assert.AreEqual(expectedLength, circularList.Count);
        }

        [Test]
        [TestCase(2)]
        public void AddAfter_WithInvalidNode_ToFail(int item)
        {
            var circularList = new CircularLinkedList<int>(new[] {3});
            var node = new Node<int>(11);

            Assert.Throws<ArgumentException>(() => circularList.AddAfter(node, item));
        }

        [Test]
        public void Clear()
        {
            var circularList = new CircularLinkedList<int>(new[] {3, 4, 3, 2, 1});
            const int expectedLength = 0;

            circularList.Clear();

            Assert.AreEqual(expectedLength, circularList.Count);
        }

        [Test]
        public void Find()
        {
            var circularList = new CircularLinkedList<int>(new[] {3, 4, 3, 2});

            Assert.AreEqual(true, circularList.Contains(3));
            Assert.AreEqual(false, circularList.Contains(12));
        }


        [Test]
        public void CopyTo()
        {
            var circularList = new CircularLinkedList<int>(new[] {3, 3, 2});
            var array = new int[5];
            array[0] = 21;
            array[1] = 12;
            var expectedArray = new int[] {21, 12, 3, 3, 2};

            circularList.CopyTo(array, 2);

            CollectionAssert.AreEqual(expectedArray, array);
        }

        [Test]
        public void CopyTo_WithNullArray_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {3, 3, 2});

            Assert.Throws<ArgumentNullException>(() => circularList.CopyTo(null, 2));
        }

        [Test]
        public void CopyTo_WithNegativeIndex_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {3, 3, 2});
            var array = new int[5];
            array[0] = 21;
            array[1] = 12;
            const int arrayIndex = -2;

            Assert.Throws<ArgumentOutOfRangeException>(() => circularList.CopyTo(array, arrayIndex));
        }

        [Test]
        public void CopyTo_WithIndexOutOfArrayRange_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {3, 3, 2});
            var array = new int[5];
            array[0] = 21;
            array[1] = 12;
            const int arrayIndex = 123;

            Assert.Throws<ArgumentOutOfRangeException>(() => circularList.CopyTo(array, arrayIndex));
        }

        [Test]
        public void CopyTo_WithInvalidIndexAndArrayLength_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {3, 3, 2});
            var array = new int[5];
            array[0] = 21;
            array[1] = 12;
            const int arrayIndex = 123;

            Assert.Throws<ArgumentOutOfRangeException>(() => circularList.CopyTo(array, arrayIndex));
        }


        [Test]
        public void ElementAt_WithCorrectIndex()
        {
            var circularList = new CircularLinkedList<int>(new[] {3, 1, 0});
            const int index = 1;
            var expected = circularList[index];

            var actual = circularList.ElementAt(index);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ElementAt_WithIncorrectIndex()
        {
            var circularList = new CircularLinkedList<int>(new[] {3});

            const int index = 23;

            Assert.Throws<ArgumentException>(() => circularList.ElementAt(index));
        }

        [Test]
        public void NodeAt_WithCorrectIndex()
        {
            var circularList = new CircularLinkedList<int>(new[] {2, 1, 0});
            const int index = 1;
            var expectedItem = circularList[index];

            var actual = circularList.NodeAt(index);

            Assert.NotNull(actual);
            Assert.AreEqual(expectedItem, actual.Item);
        }

        [Test]
        public void NodeAt_WithIncorrectIndex_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {2});

            const int index = 23;

            Assert.Throws<ArgumentException>(() => circularList.NodeAt(index));
        }

        [Test]
        public void IndexOf_ToEmptyList()
        {
            var circularList = new CircularLinkedList<int>();
            const int item = 12;
            const int expected = -1;

            var actual = circularList.IndexOf(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IndexOf_WithCorrectItem()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 32, 3});
            const int item = 32;
            const int expected = 1;

            var actual = circularList.IndexOf(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IndexOf_WithIncorrectItem()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 32, 322});
            const int item = 20;
            const int expected = -1;

            var actual = circularList.IndexOf(item);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Insert_WithCorrectIndex()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 32, 3});
            const int item = 1;

            circularList.Insert(1, item);
            circularList.Insert(0, item);

            Assert.AreEqual(item, circularList[2]);
            Assert.AreEqual(item, circularList[0]);
        }

        [Test]
        public void Insert_WithIncorrectIndex_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 302, 3});
            const int index = 5;
            const int item = 1;

            Assert.Throws<ArgumentException>(() => circularList.Insert(index, item));
        }

        [Test]
        public void InsertRange_ToList()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 44, 2});
            const int index = 1;
            var array = new[] {3, 2, 1};
            var expectedLength = circularList.Count + 3;
            var expectedArray = new[] {12, 3, 2, 1, 44, 2};

            circularList.InsertRange(index, array);

            Assert.AreEqual(expectedLength, circularList.Count);
            CollectionAssert.AreEqual(expectedArray, circularList.ToArray());
        }

        [Test]
        public void InsertRange_InputCollectionIsNull_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {120, 44, 20});

            const int index = 51;

            Assert.Throws<ArgumentNullException>(() => circularList.InsertRange(index, null));
        }

        [Test]
        public void InsertRange_WithInvalidIndex_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 44, 2});

            const int index = 51;
            var array = new[] {1, 2, 3};

            Assert.Throws<ArgumentException>(() => circularList.InsertRange(index, array));
        }

        [Test]
        public void Find_WithCorrectItem()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 32, 3, 320});
            const int item = 32;
            const int expectedPrevItem = 12;

            var actual = circularList.Find(item);

            Assert.NotNull(actual);
            Assert.AreEqual(item, actual.Item);
            Assert.AreEqual(expectedPrevItem, actual.Prev.Item);
        }

        [Test]
        public void Find_WithIncorrectItem()
        {
            var circularList = new CircularLinkedList<int>(new[] {1112, 32, 3});
            const int item = 3223;

            var result = circularList.Find(item);

            Assert.IsNull(result);
        }

        [Test]
        public void FindLast_WithCorrectItem()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 32, 3, 32});
            const int item = 32;
            const int expectedPrevItem = 3;

            var actual = circularList.FindLast(item);

            Assert.NotNull(actual);
            Assert.AreEqual(item, actual.Item);
            Assert.AreEqual(expectedPrevItem, actual.Prev.Item);
        }

        [Test]
        public void FindLast_WithIncorrectItem()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 32, 3});
            const int item = 3223;

            var result = circularList.FindLast(item);

            Assert.IsNull(result);
        }

        [Test]
        public void FindAll_WithCorrectItem()
        {
            var circularList = new CircularLinkedList<int>(new[] {120, 32, 3, 32});
            const int item = 32;
            const int expectedLength = 2;

            var actualArray = circularList.FindAll(item);

            Assert.AreEqual(expectedLength, actualArray.Count());
            Assert.AreEqual(item, actualArray.ElementAt(0).Item);
        }


        [Test]
        public void FindAll_WithIncorrectItem()
        {
            var circularList = new CircularLinkedList<int>(new[] {124, 324, 34, 43});

            var result = circularList.FindLast(3223);

            Assert.IsNull(result);
        }

        [Test]
        public void GetRange_WithCorrectIndexAndCount()
        {
            var circularList = new CircularLinkedList<int>(new[] {2, 2, 32, 3});
            const int index = 1;
            const int count = 2;
            var expectedRange = new[] {2, 32};

            var actualRange = circularList.GetRange(index, count);

            CollectionAssert.AreEqual(expectedRange, actualRange);
        }

        [Test]
        public void GetRange_WithIncorrectIndex_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {120, 32, 30, 3});
            const int index = -1;
            const int count = 200;

            Assert.Throws<ArgumentException>(() => circularList.GetRange(index, count));
        }

        [Test]
        public void GetRange_WithIncorrectIndexAndCount_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 32, 213, 3453});
            const int index = -1;
            const int count = 200;

            Assert.Throws<ArgumentException>(() => circularList.GetRange(index, count));
        }

        [Test]
        public void Remove_WithCorrectItem_TrueReturned()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 32, 3, 3});
            const int item = 3;

            Assert.True(circularList.Remove(item));
        }


        [Test]
        public void Remove_WithIncorrectItem_FalseReturned()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 32, 13, 13});
            const int item = 1000;

            Assert.False(circularList.Remove(item));
        }


        [Test]
        public void Remove_FromEmptyList_FalseReturned()
        {
            var circularList = new CircularLinkedList<int>();
            const int item = 2;

            Assert.False(circularList.Remove(item));
        }

        [Test]
        public void RemoveFirst()
        {
            var circularList = new CircularLinkedList<int>(new[] {1, 2, 3, 4});
            var expectedLength = circularList.Count - 1;

            circularList.RemoveFirst();

            Assert.AreEqual(expectedLength, circularList.Count);
        }

        [Test]
        public void RemoveLast()
        {
            var circularList = new CircularLinkedList<int>(new[] {1, 2323, 3, 4});
            var expectedLength = circularList.Count - 1;

            circularList.RemoveLast();

            Assert.AreEqual(expectedLength, circularList.Count);
        }

        [Test]
        public void RemoveAt_WithCorrectIndex()
        {
            var circularList = new CircularLinkedList<int>(new[] {3, 4, 5});
            const int index = 1;
            var expectedArray = new[] {3, 5};

            circularList.RemoveAt(index);

            CollectionAssert.AreEqual(expectedArray, circularList.ToArray());
        }


        [Test]
        public void RemoveAt_WithIncorrectIndex_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {7, 3, 1});
            const int index = 123;

            Assert.Throws<ArgumentException>(() => circularList.RemoveAt(index));
        }

        [Test]
        public void RemoveRange_WithCorrectIndexAndCount()
        {
            var circularList = new CircularLinkedList<int>(new[] {122, 32, 3, 4});
            const int index = 1;
            const int count = 2;
            var expectedLength = circularList.Count - count;

            circularList.RemoveRange(index, count);

            Assert.AreEqual(expectedLength, circularList.Count);
        }

        [Test]
        public void RemoveRange_WithIncorrectIndex_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {11, 22, 3, 3});
            const int index = -1;
            const int count = 2;

            Assert.Throws<ArgumentException>(() => circularList.RemoveRange(index, count));
        }

        [Test]
        public void RemoveRange_WithIncorrectIndexAndCount_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new[] {12, 32, 3, 3});
            const int index = 1;
            const int count = 200;

            Assert.Throws<ArgumentException>(() => circularList.RemoveRange(index, count));
        }

        [Test]
        public void GetEnumerator_List_ReturnsCorrectList()
        {
            var expected = new CircularLinkedList<int>(new[] {12, 32, 3, 21, 44});
            var enumerator = expected.GetEnumerator();
            var actual = new CircularLinkedList<int>();
            
            while (enumerator.MoveNext())
            {
                actual.Add(enumerator.Current);
            }
            
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}