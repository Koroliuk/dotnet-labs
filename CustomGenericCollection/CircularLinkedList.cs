using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomGenericCollection
{
    /// <summary>
    /// The class that implements a circular linked list.
    /// </summary>
    /// <typeparam name="T">The class of list elements</typeparam>
    public class CircularLinkedList<T> : ICollection<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        
        /// <summary>
        /// This delegate handles events for adding and removing elements 
        /// </summary>
        /// <param name="sender">Instance of <see cref="CircularLinkedList{T}"/> that called the event</param>
        /// <param name="args">Arguments passed by sender for subscribers</param>
        public delegate void CircularListEventHandler(object sender, CircularEventArgs<T> args);

        /// <summary>
        /// This delegate handles events for clearing the list 
        /// </summary>
        /// <param name="sender">Instance of <see cref="CircularLinkedList{T}"/> that called the event</param>
        /// <param name="args">Arguments passed by sender for subscribers</param>
        public delegate void CircularListClearingEventHandler(object sender, MessageEventArgs args);
        
        /// <summary>
        /// Event of adding an element to the collection.
        /// </summary>
        public event CircularListEventHandler Addition;
        
        /// <summary>
        /// Event of deletion an element from the collection.
        /// </summary>
        public event CircularListEventHandler Deletion;
        
        /// <summary>
        /// Collection cleaning event.
        /// </summary>
        public event CircularListClearingEventHandler Clearing;

        /// <summary>
        /// This indexer get or set data at some position of the list. 
        /// </summary>
        /// <param name="index">Index of position</param>
        /// <exception cref="IndexOutOfRangeException">It throws when index is out of list range</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
                {
                    throw new IndexOutOfRangeException("Index is out of range");
                }

                return ElementAt(index);
            }
            set
            {
                if (index < 0 || index > Count - 1)
                {
                    throw new IndexOutOfRangeException("Index is out of range");
                }

                var node = NodeAt(index);
                node.Item = value;
            }
        }

        /// <summary>
        /// Adds an object to the end of the list.
        /// </summary>
        /// <param name="item">The object to be added to the end of the list. The value can be <b>null</b></param>
        public void Add(T item)
        {
            var node = new Node<T>(item);
            if (_head == null)
            {
                _head = node;
                _tail = node;
                node.Next = _tail;
                node.Prev = _head;
            }
            else
            {
                node.Prev = _tail;
                node.Next = _head;
                _tail.Next = node;
                _head.Prev = node;
                _tail = node;
            }

            Count++;
            Addition?.Invoke(this, new CircularEventArgs<T>(item, $"Added element: {item}"));
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the list.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the end of the list.
        /// The collection itself cannot be null, but it can contain elements that are null,
        /// if type T is a reference type</param>
        /// <exception cref="ArgumentNullException">Raises when collection is null</exception>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection), "Collection is null");
            }

            foreach (var item in collection)
            {
                Add(item);
            }
        }
        
        /// <summary>
        /// Adds a new value at the start of the list
        /// </summary>
        /// <param name="item">The object to be added to the start of the list</param>
        public void AddFirst(T item)
        {
            var node = new Node<T>(item);
            if (_head == null)
            {
                node.Next = node;
                node.Prev = node;
                _head = node;
                _tail = node;
            }
            else
            {
                node.Prev = _tail;
                node.Next = _head;
                _tail.Next = node;
                _head.Prev = node;
                _head = node;
            }
            
            Addition?.Invoke(this, new CircularEventArgs<T>(item ,$"Added element: {item} at the beginning of the list"));
            Count++;
        }
        
        /// <summary>
        /// Adds a new value at the end of the list.
        /// </summary>
        /// <param name="item">The object to be added to the start of the list</param>
        public void AddLast(T item)
        {
            var node = new Node<T>(item);
            if (_head == null)
            {
                node.Next = node;
                node.Prev = node;
                _head = node;
                _tail = node;
            }
            else
            {
                node.Prev = _tail;
                node.Next = _head;
                _tail.Next = node;
                _head.Prev = node;
                _tail = node;
            }

            Addition?.Invoke(this, new CircularEventArgs<T>(item, $"Added element: {item} at the end of the list"));
            Count++;
        }
        
        /// <summary>
        /// Adds a new value before an existing node in the list.
        /// </summary>
        /// <param name="node">The <see cref="Node{T}">Node{T}</see> before which to insert a new value</param>
        /// <param name="item">The object to be added to the start of the list</param>
        /// <exception cref="ArgumentException">Raises when node is null</exception>
        public void AddBefore(Node<T> node, T item)
        {
            var isNodeValid = ValidateNode(node);
            if (!isNodeValid)
            {
                throw new ArgumentException("An invalid given node", nameof(node));
            }

            var newNode = new Node<T>(item)
            {
                Next = node,
                Prev = node.Prev
            };
            node.Prev.Next = newNode;
            node.Prev = newNode;
            if (node == _head)
            {
                _head = node;
            }

            Addition?.Invoke(this, new CircularEventArgs<T>(item, $"Added element: {item} before element: {node.Item}"));
            Count++;
        }

        /// <summary>
        /// Adds a new value after an existing node in the list.
        /// </summary>
        /// <param name="node">The <see cref="Node{T}">Node{T}</see> after which to insert a new value</param>
        /// <param name="item">The object to be added to the start of the list</param>
        /// <exception cref="ArgumentException">Raises when node is null</exception>
        public void AddAfter(Node<T> node, T item)
        {
            var isNodeValid = ValidateNode(node);
            if (!isNodeValid)
            {
                throw new ArgumentException(null, nameof(node));
            }

            var newNode = new Node<T>(item)
            {
                Next = node.Next,
                Prev = node
            };
            node.Next = newNode;
            node.Next.Prev = newNode;
            if (node == _tail)
            {
                _tail = node;
            }

            Addition?.Invoke(this, new CircularEventArgs<T>(item, $"Added element: {item} before element: {node.Item}"));
            Count++;
        }

        /// <summary>
        /// Removes all nodes from the list
        /// </summary>
        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
            Clearing?.Invoke(this, new MessageEventArgs("The collection was cleared"));
        }

        /// <summary>
        /// Determines whether a value is in the list.
        /// </summary>
        /// <param name="item">The value to locate in the list</param>
        /// <returns>true if value is found in the list; otherwise, false.</returns>
        public bool Contains(T item)
        {
            var node = Find(item);
            return node != null;
        }

        /// <summary>
        /// Copies the entire list to a compatible one-dimensional Array,
        /// starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from list</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins</param>
        /// <exception cref="ArgumentNullException">Raises when array is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Raises when arrayIndex is less than zero</exception>
        /// <exception cref="ArgumentException">The number of elements in the source list is greater than
        /// the available space from index to the end of the destination array</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Given array is null");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Given index of array is less than zero");
            }

            if (arrayIndex > array.Length - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Given index of array is out of range");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Given index of array is out of possible range, so array can not " +
                                            "contain all elements of circular array list");
            }

            if (_head == null)
            {
                return;
            }

            var node = _head;
            do
            {
                array[arrayIndex++] = node.Item;
                node = node.Next;
            } while (node != _head);
        }

        /// <summary>
        /// Gives a value at some index of the list
        /// </summary>
        /// <param name="index">Index of list</param>
        /// <returns>A value at some index</returns>
        /// <exception cref="ArgumentException">Index is out of range of list</exception>
        public T ElementAt(int index)
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentException("Given index is out of range");
            }

            if (_head == null)
            {
                return default;
            }

            var current = _head;
            var i = 0;


            do
            {
                if (i == index)
                {
                    return current.Item;
                }

                i++;
                current = current.Next;
            } while (current != _head);

            return default;
        }

        /// <summary>
        /// Gives a node at some index of the list
        /// </summary>
        /// <param name="index">Index of the list</param>
        /// <returns>A node of the list</returns>
        /// <exception cref="ArgumentException">Index is out of range of list</exception>
        public Node<T> NodeAt(int index)
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentException("Given index is out of range");
            }

            if (_head == null)
            {
                return default;
            }

            var current = _head;
            var i = 0;

            do
            {
                if (i == index)
                {
                    return current;
                }

                i++;
                current = current.Next;
            } while (current != _head);

            return default;
        }
 
        /// <summary>
        /// Returns the zero-based index of the first occurrence of a value in the list or in a portion of it.
        /// </summary>
        /// <param name="item">The object to locate in the list. The value can be null for reference types</param>
        /// <returns>The zero-based index of the first occurrence of item</returns>
        public int IndexOf(T item)
        {
            if (_head == null)
            {
                return -1;
            }

            var current = _head;
            var i = 0;
            do
            {
                if (current.Item.Equals(item))
                {
                    return i;
                }

                i++;
                current = current.Next;
            } while (current != _head);

            return -1;
        }

        /// <summary>
        /// Inserts an element into the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted</param>
        /// <param name="item">The object to insert</param>
        /// <exception cref="ArgumentException">Index is out of range</exception>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentException("Given index is out of range");
            }

            if (index == 0)
            {
                AddFirst(item);
            }
            else
            {
                var node = NodeAt(index);
                var newNode = new Node<T>(item)
                {
                    Next = node,
                    Prev = node.Prev
                };
                node.Prev.Next = newNode;
                node.Prev = newNode;
                Count++;
                Addition?.Invoke(this, new CircularEventArgs<T>(item, $"Inserted element: {item} at the index: {index}"));
            }
        }

        /// <summary>
        /// Inserts the elements of a collection into the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted</param>
        /// <param name="collection">The collection whose elements should be inserted into the list</param>
        /// <exception cref="ArgumentNullException">Raises when collection is null</exception>
        /// <exception cref="ArgumentException">Index out of range</exception>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection), "Collection is null");
            }

            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentException("Given index of array is bigger than this circular array list");
            }

            foreach (var item in collection)
            {
                Insert(index, item);
                index++;
            }
        }

        /// <summary>
        /// Searches for an element that matches the item,
        /// and returns the first occurrence within the entire list.
        /// </summary>
        /// <param name="item">An item to be found</param>
        /// <returns>A first occurrence node that contains a node; if there is no node with a such item,
        /// null is returned</returns>
        public Node<T> Find(T item)
        {
            if (_head == null)
            {
                return default;
            }

            var current = _head;
            do
            {
                if (current.Item.Equals(item))
                {
                    return current;
                }

                current = current.Next;
            } while (current != _head);

            return default;
        }

        /// <summary>
        /// Searches for an element that matches the item,
        /// and returns the last occurrence within the entire list.
        /// </summary>
        /// <param name="item">An item to be found</param>
        /// <returns>A last occurrence node that contains a node; if there is no node with a such item,
        /// null is returned</returns>
        public Node<T> FindLast(T item)
        {
            if (_head == null)
            {
                return default;
            }

            var current = _head;
            Node<T> lastFound = null;
            do
            {
                if (current.Item.Equals(item))
                {
                    lastFound = current;
                }

                current = current.Next;
            } while (current != _head);

            return lastFound;
        }

        /// <summary>
        /// Retrieves all the elements that matches the item.
        /// </summary>
        /// <param name="item">An item to be found</param>
        /// <returns>A list containing all the elements that match item</returns>
        public IEnumerable<Node<T>> FindAll(T item)
        {
            if (_head == null)
            {
                return default;
            }

            var current = _head;
            var result = new List<Node<T>>();
            do
            {
                if (current.Item.Equals(item))
                {
                    result.Add(current);
                }

                current = current.Next;
            } while (current != _head);

            return result;
        }

        /// <summary>
        /// Retrieves a list of a range of elements in the source list.
        /// </summary>
        /// <param name="index">The zero-based list index at which the range starts</param>
        /// <param name="count">The number of elements in the range</param>
        /// <returns>A list of questioned range</returns>
        /// <exception cref="ArgumentException">Index out of range or index with count do not denote a valid range of
        /// elements in the list</exception>
        public List<T> GetRange(int index, int count)
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentException("Given index of array is bigger than this circular array list");
            }

            if (index + count > Count - 1)
            {
                throw new ArgumentException("Given range is bigger than this part of the circular array list");
            }

            var result = new List<T>();
            while (count > 0)
            {
                result.Add(ElementAt(index));
                count--;
                index++;
            }

            return result;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the list.
        /// </summary>
        /// <param name="item">The object to remove from the list</param>
        /// <returns>true if item is successfully removed; otherwise, false. This method also returns false if item was
        /// not found in the list.</returns>
        public bool Remove(T item)
        {
            if (_head == null)
            {
                Deletion?.Invoke(this, new CircularEventArgs<T>(item, "The list is empty"));
                return false;
            }

            var current = _head;
            do
            {
                if (current.Item.Equals(item))
                {
                    current.Prev.Next = current.Next;
                    current.Next.Prev = current.Prev;
                    Deletion?.Invoke(this, new CircularEventArgs<T>(item,$"First occurrence of element: {item} is deleted"));
                    Count--;
                    return true;
                }

                current = current.Next;
            } while (current != _head);

            Deletion?.Invoke(this, new CircularEventArgs<T>(item,$"There is no a such {item} in list"));
            return false;
        }

        /// <summary>
        /// Removes the node at the start of the list.
        /// </summary>
        public void RemoveFirst()
        {
            if (_head == null)
            {
                Deletion?.Invoke(this, new CircularEventArgs<T>(default,"The list is empty"));
                return;
            }

            var deleted = _head;
            _head = _head.Next;
            _tail.Next = _head;
            _head.Prev = _tail;
            Count--;
            Deletion?.Invoke(this, new CircularEventArgs<T>(deleted.Item,"First element is deleted"));
        }

        /// <summary>
        /// Removes the node at the end of the list.
        /// </summary>
        public void RemoveLast()
        {
            if (_tail == null)
            {
                Deletion?.Invoke(this, new CircularEventArgs<T>(default,"The list is empty"));
                return;
            }

            var deleted = _tail;
            _tail = _tail.Prev;
            _head.Prev = _tail;
            _tail.Next = _head;
            Count--;
            Deletion?.Invoke(this, new CircularEventArgs<T>(deleted.Item,"Last element is deleted"));
        }

        /// <summary>
        /// Removes the element at the specified index of the list.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove</param>
        /// <exception cref="ArgumentException">Index is out of range</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentException("Given index of array is bigger than this circular array list");
            }

            if (_head == null)
            {
                Deletion?.Invoke(this, new CircularEventArgs<T>(default,"The list is empty"));
                return;
            }

            if (index == 0)
            {
                RemoveFirst();
            }

            if (index == Count - 1)
            {
                RemoveLast();
            }

            var node = NodeAt(index);
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
            Count--;
            Deletion?.Invoke(this, new CircularEventArgs<T>(node.Item,$"Element at index: {index} is deleted"));
        }

        /// <summary>
        /// Removes a range of elements from the list.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove</param>
        /// <param name="count">The number of elements to remove</param>
        /// <exception cref="ArgumentException">index and count do not denote a valid range of elements in the list</exception>
        public void RemoveRange(int index, int count)
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentException("Given index of array is bigger than this circular array list");
            }

            if (index + count > Count - 1)
            {
                throw new ArgumentException("Given range is bigger than this part of the circular array list");
            }

            while (count > 0)
            {
                RemoveAt(index);
                count--;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>A list enumerator</returns>
        public IEnumerator<T> GetEnumerator() => new Enumerator<T>(this);

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>A list enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            var current = _head;
            do
            {
                if (current == null) continue;
                yield return current.Item;
                current = current.Next;
            } while (current != _head);
        }

        /// <summary>
        /// Checks if some node are contained at this list
        /// </summary>
        /// <param name="node">A node to check</param>
        /// <returns>true if node are contained at list; otherwise false</returns>
        /// <exception cref="ArgumentException">Node is null</exception>
        private bool ValidateNode(Node<T> node)
        {
            if (node == null)
            {
                throw new ArgumentException("Node is null", nameof(node));
            }

            var nodes = FindAll(node.Item);
            return nodes.Contains(node);
        }

        /// <summary>
        /// The property that return an amount of elements in collection
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// The property that return a value of the first element of the list
        /// </summary>
        public T First => _head == null ? default : _head.Item;

        /// <summary>
        /// The property that return a value of the last element of the list
        /// </summary>
        public T Last => _tail == null ? default : _tail.Item;

        /// <summary>
        /// Indicate whether the list is read-only.
        /// Make it always false
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Class of custom implementation of enumerator
        /// </summary>
        /// <typeparam name="TItem">The class of list elements</typeparam>
        private class Enumerator<TItem> : IEnumerator<TItem>
        {
            private readonly Node<TItem> _head;
            private Node<TItem> _current;
            public Enumerator(CircularLinkedList<TItem> list)
            {
                _head = list.NodeAt(0);
            }
        
            /// <summary>
            /// Advances the enumerator to the next element of the <see cref="CircularLinkedList{TItem}"/>.
            /// </summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator
            /// has passed the end of the collection</returns>
            public bool MoveNext()
            {
                if (_current != null && _current.Next == _head)
                {
                    return false;
                }

                _current = _current == null ? _head : _current.Next;
                return true;
            }

            public void Reset()
            {
                _current = _head;
            }

            public TItem Current => _current.Item;

            object IEnumerator.Current
            {
                get
                {
                    if (_current == null)
                    {
                        throw new InvalidOperationException();
                    }
                    return _current.Item;
                }
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
