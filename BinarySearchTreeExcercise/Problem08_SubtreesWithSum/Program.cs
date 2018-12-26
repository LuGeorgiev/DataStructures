using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem08_SubtreesWithSum
{
    class Program
    {
        static Dictionary<int, Tree<int>> tree = new Dictionary<int, Tree<int>>();

        static void Main(string[] args)
        {
            int numberOfNodes = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfNodes - 1; i++)
            {
                int[] nodes = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                int parent = nodes[0];
                int child = nodes[1];

                if (!tree.ContainsKey(parent))
                {
                    tree.Add(parent, new Tree<int>(parent));
                }
                if (!tree.ContainsKey(child))
                {
                    tree.Add(child, new Tree<int>(child));
                }

                Tree<int> parentNode = tree[parent];
                Tree<int> childNode = tree[child];
                parentNode.Children.Add(childNode);
                childNode.Parent = parentNode;
            }

            Tree<int> root = tree
                .FirstOrDefault(x => x.Value.Parent == null)
                .Value;

            int sumSubTree = int.Parse(Console.ReadLine());
            Console.WriteLine($"Subtrees of sum {sumSubTree}:");
            SubTreeDFS(root, sumSubTree);
        }

        static int SubTreeDFS(Tree<int> node, int sum)
        {
            int currentSum = node.Value;
            int subtreeSum = 0;

            foreach (var child in node.Children)
            {
                currentSum += child.Value;
                subtreeSum+=SubTreeDFS(child,sum);
            }
            if (currentSum ==sum)
            {
                List<int> subtree = new List<int>();
                GetSubTree(node, subtree);
                Console.WriteLine(string.Join(" ", subtree));
            }
            return currentSum;
        }

        private static void GetSubTree(Tree<int> node, List<int> result)
        {
            result.Add(node.Value);
            foreach (var child in node.Children)
            {
                GetSubTree(child,result);
            }
        }
    }

    class Tree<T>
    {
        public T Value { get; set; }

        public List<Tree<T>> Children { get; set; }

        public Tree<T> Parent { get; set; }

        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>(children);
        }

        public void Print(int indent = 0)
        {
            Console.WriteLine($"{new string(' ', indent)}{this.Value}");
            foreach (var child in this.Children)
            {
                child.Print(indent + 2);
            }
        }
    }
}
