using System;
using NUnit.Framework;
using CustomGenericCollection;


namespace CustomerGenericCollection
{
    public class CustomGenericCollectionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add_ToEmptyList()
        {
            var circularList = new CircularLinkedList<int>();

            const int node = 1;
            const int expectedLength = 1;
            circularList.Add(node);
            
            Assert.AreEqual(node, circularList[0]);
            Assert.AreEqual(expectedLength, circularList.Count);
        }
        
        [Test]
        public void Add_ToList()
        {
            var circularList = new CircularLinkedList<int>(new []{12, 44, 2});

            const int node = 541;
            const int length = 4;
            circularList.Add(node);
            
            Assert.AreEqual(node, circularList[length-1]);
            Assert.AreEqual(length, circularList.Count);
        }
        
        [Test]
        public void AddRange_ToList()
        {
            var circularList = new CircularLinkedList<int>(new []{12, 44, 2});
            
            const int length = 6;
            circularList.AddRange(new []{3, 2, 1});
            
            Assert.AreEqual(length, circularList.Count);
        }
        
                
        [Test]
        public void AddRange_InputCollectionIsNull_ToFail()
        {
            var circularList = new CircularLinkedList<int>(new []{12, 44, 2});
            
            Assert.Throws<ArgumentNullException>(
                () => circularList.AddRange(null));
        }
    }
}