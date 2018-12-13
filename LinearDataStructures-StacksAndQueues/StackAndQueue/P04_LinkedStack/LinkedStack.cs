using System;
using System.Collections.Generic;
using System.Text;

namespace P04_LinkedStack
{
    public class LinkedStack<T>
    {
        private class Node<T>
        {
            private T value;

            public Node(T value, Node<T> next=null)
            {
                this.Value = value;
                this.NextNode = next;
            }
            public Node<T> NextNode { get; set; }
            public T Value { get; set; }
        }

        private Node<T> firstNode;

        public int Count { get;private set; }

        public void Push(T element)
        {
            this.firstNode = new Node<T>(element, this.firstNode);
            this.Count++;
        }
    

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            T result = this.firstNode.Value;
            this.firstNode = this.firstNode.NextNode;
            this.Count--;
            return result;
        }

        public T[] ToArray()
        {
            var result = new T[this.Count];
            var current = this.firstNode;
            int index = 0;
            while (current != null)
            {
                result[index] = current.Value;
                current = current.NextNode;
                index++;
            }

            return result;
        }
    }
}
