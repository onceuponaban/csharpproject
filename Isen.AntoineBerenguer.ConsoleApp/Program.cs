using System;

namespace Isen.AntoineBerenguer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Library.Node root = new Library.Node(null, "I am Root.");
            Library.Node child1 = new Library.Node(root, "This is the first child of root.");
            Console.WriteLine($"Node child1 of guid {child1.Id} and of value \"{child1.Value}\" has a depth of {child1.Depth} and its parent has a guid of {child1.Parent.Id} and a value of \"{child1.Parent.Value}\".");
            Library.Node child11 = new Library.Node(child1, "This is the first child of child1.");
            Console.WriteLine($"Node child11 of guid {child11.Id} and of value \"{child11.Value}\" has a depth of {child11.Depth} and its parent has a guid of {child11.Parent.Id} and a value of \"{child11.Parent.Value}\".");
            Console.WriteLine($"Node root equal to parent of child1? {root.Equals(child1.Parent)}"); // True
            Console.WriteLine($"Node root equal to parent of child11? {root.Equals(child11.Parent)}"); // False
            Console.WriteLine($"Node child1 equal to first child of root? {child1.Equals(root.Children[0])}"); // True
        }
    }
}
