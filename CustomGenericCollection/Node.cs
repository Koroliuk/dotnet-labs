namespace CustomGenericCollection
{
    /// <summary>
    /// A class that represents a node for two-linked lists.
    /// </summary>
    /// <typeparam name="T">The class of elements of a particular linked list and this node</typeparam>
    public class Node<T>
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }

        public Node(T item)
        {
            Item = item;
        }
    }
}