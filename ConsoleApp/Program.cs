using System;
using System.Collections.Generic;
using System.Linq;
using CustomGenericCollection;

namespace ConsoleApp
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var list1 = new CircularLinkedList<string>
            {
                "Alla",
                "Tom",
                "Bob"
            };
            
            list1.Addition += DisplayMessage;
            list1.Deletion += DisplayMessage;
            list1.Clearing += DisplayMessage;
            
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));
            Console.WriteLine("Our initial list: {"+string.Join(", ", list1)+"}");
            
            list1.Add("Tito");
            list1.AddRange(new List<string>{"Maria", "Joshua", "Jack"});
            list1.AddFirst("First");
            list1.AddLast("Last");
            
            Console.WriteLine("Our list: {"+string.Join(", ", list1)+"}");
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));

            var node1 = list1.NodeAt(1);
            var node2 = list1.NodeAt(3);
            list1.AddBefore(node1, "Before");
            list1.AddAfter(node2, "After");
            
            Console.WriteLine("Our list: {"+string.Join(", ", list1)+"}");
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));
            
            Console.WriteLine(list1.Contains("Before"));
            Console.WriteLine(list1.Contains("3r3r3r3f3fdf"));
            
            Console.WriteLine($"Element of list1 at index 4 is {list1.ElementAt(4)}");
            Console.WriteLine($"Index of element: After is {list1.IndexOf("After")}");
            
            list1.Add("Bob");

            var bob1 = list1.Find("Bob");
            var bob2 = list1.FindLast("Bob");
            Console.WriteLine($"The first Bob with the next element: {bob1.Next.Item}");
            Console.WriteLine($"The last Bob with the next element: {bob2.Next.Item}");
            Console.WriteLine($"There is {list1.FindAll("Bob").Count()} Bob in list");
            Console.WriteLine("Some range: {"+string.Join(", ", list1.GetRange(2, 5))+"}");

            Console.WriteLine("Our list: {"+string.Join(", ", list1)+"}");
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));

            list1.Remove("After");
            list1.RemoveFirst();
            list1.RemoveLast();
            
            Console.WriteLine("Our list: {"+string.Join(", ", list1)+"}");
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));
            
            list1.Insert(1, "Inserted");
            list1.InsertRange(3, new List<string>{"Insr1", "Insr2", "Insr3"});
            
            Console.WriteLine("Our list: {"+string.Join(", ", list1)+"}");
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));
            
            list1.RemoveAt(3);
            list1.RemoveRange(1, 8);

            Console.WriteLine("Our list: {"+string.Join(", ", list1)+"}");
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));

            var array = new string[9];
            array[0] = list1[3];
            array[1] = "wd3d3d3";
            list1.CopyTo(array, 2);
            
            foreach (var item in array)
            {
                Console.Write(item+" ");
            }
            
            Console.WriteLine("\n"+string.Concat(Enumerable.Repeat("-", 80)));
            
            list1.Clear();
            
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 80)));

            var list2 = new CircularLinkedList<int>{1, 2, 3, 4, 5, 6};
            using var enumerator = list2.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.Write(enumerator.Current+" ");
            }
            Console.Write(list2.First);
            Console.Write(list2.Last);
            
        }
        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}


