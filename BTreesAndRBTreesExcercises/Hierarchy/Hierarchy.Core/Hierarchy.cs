namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Node root;
        private Dictionary<T, Node> nodesByValue;

        public Hierarchy(T root)
        {
            this.root = new Node(root);
            this.nodesByValue = new Dictionary<T, Node>();
            this.nodesByValue.Add(root, this.root);
        }

        public int Count
        {
            get
            {
                return this.nodesByValue.Count;
            }
        }

        public void Add(T parent, T child)
        {
            if (!this.nodesByValue.ContainsKey(parent))
            {
                throw new ArgumentException();
            }

            if (this.nodesByValue.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            Node parentNode = this.nodesByValue[parent];
            Node childNode = new Node(child, parentNode);
            parentNode.Children.Add(childNode);
            this.nodesByValue.Add(child, childNode);
        }

        public void Remove(T element)
        {
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }
            var current = this.nodesByValue[element];
            if (current.Parent==null)
            {
                throw new InvalidOperationException();
            }

            foreach (var child in current.Children)
            {
                child.Parent = current.Parent;
                current.Parent.Children.Add(child);
            }
            //remove reference to current 
            current.Parent.Children.Remove(current);
            //remove from Dictionarry
            this.nodesByValue.Remove(element);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.nodesByValue.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            Node parent = this.nodesByValue[item];

            return parent.Children.Select(x=>x.Value);
        }

        public T GetParent(T item)
        {
            if (!this.nodesByValue.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            Node child = this.nodesByValue[item];

            return child.Parent != null ? child.Parent.Value : default(T);
        }

        public bool Contains(T value)
        {
            return this.nodesByValue.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            List<T> collection = new List<T>();
            foreach (var kvp in this.nodesByValue)
            {
                if (other.Contains(kvp.Key))
                {
                    collection.Add(kvp.Key);
                }
            }
            return collection;

            //var result = new HashSet<T>(this.nodesByValue.Keys);
            //result
            //    .IntersectWith(new HashSet<T>(this.nodesByValue.Keys));
            //return result;
        } 

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Node>();
            Node curret = this.root;
            queue.Enqueue(curret);
            while (queue.Count>0)
            {
                curret = queue.Dequeue();
                yield return curret.Value;

                foreach (var child in curret.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node
        {
            public Node(T value, Node parent=null)
            {
                this.Value = value;
                this.Children = new List<Node>();
                this.Parent = parent;
            }

            public T Value { get; set; }

            public Node Parent { get; set; }

            public List<Node> Children { get; set; }
        }
    }
}