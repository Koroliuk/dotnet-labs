using System;

namespace CustomGenericCollection
{
    public class CircularEventArgs<T> : EventArgs
    {
        public T Item { get; private set; }
        public string Message { get; private set; }

        public CircularEventArgs(T item, string message)
        {
            Item = item;
            Message = message;
        }
    }
}