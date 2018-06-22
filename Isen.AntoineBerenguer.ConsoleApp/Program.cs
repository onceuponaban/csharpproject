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
            Library.Node child11 = new Library.Node(child1, "This is the first child of child1.");

            Console.WriteLine($"Node child1 of guid {child1.Id} and of value \"{child1.Value}\" has a depth of {child1.Depth} and its parent has a guid of {child1.Parent.Id} and a value of \"{child1.Parent.Value}\".");
            Console.WriteLine($"Node child11 of guid {child11.Id} and of value \"{child11.Value}\" has a depth of {child11.Depth} and its parent has a guid of {child11.Parent.Id} and a value of \"{child11.Parent.Value}\".");

            Console.WriteLine($"Node root equal to parent of child1? {root.Equals(child1.Parent)}"); // True
            Console.WriteLine($"Node root equal to parent of child11? {root.Equals(child11.Parent)}"); // False
            Console.WriteLine($"Node child1 equal to first child of root? {child1.Equals(root.Children[0])}"); // True
            Console.WriteLine($"Is child11 among child1's children? {child1.Children.Contains(child11)}"); // True
            Console.WriteLine($"Is child11's parent listed as child1? {child11.Parent.Equals(child1)}"); //True

            Console.WriteLine($"Trying to find child11 through GUID");
            Console.WriteLine($"Is child11 somewhere inside root's tree? {root.FindTraversing(child11.Id).Equals(child11)}"); //True

            Console.WriteLine($"Trying to find child11 through node detection");
            Console.WriteLine($"Is child11 somewhere inside root's tree? {root.FindTraversing(child11).Equals(child11)}"); //True

            Library.Node child2 = new Library.Node(root, "This is the second child of root.");
            Library.Node child3 = new Library.Node(root, "This is the third child of root.");

            Library.Node child21 = new Library.Node(child2, "This is the first child of child2.");
            Library.Node child22 = new Library.Node(child2, "This is the second child of child2.");

            Library.Node child221 = new Library.Node(child22, "This is the first child of child22.");

            Console.WriteLine($"Displaying root's tree:");
            Console.WriteLine(root);

            Console.WriteLine("Removal of child11 as child1's child through GUID");
            child1.RemoveChildNode(child11.Id);
            Console.WriteLine($"Is child11 still among child1's children? {child1.Children.Contains(child11)}"); // False
            Console.WriteLine($"Is child11 listed as not having a parent? {child11.Parent == null}"); //True

            Console.WriteLine("Removal of child1 as root's child through node detection");
            root.RemoveChildNode(child1);
            Console.WriteLine($"Is child1 still among root's children? {root.Children.Contains(child1)}"); // False
            Console.WriteLine($"Is child1 listed as not having a parent? {child1.Parent == null}"); //True
        }
    }
}
