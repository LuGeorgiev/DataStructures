using System;
using System.Collections.Generic;
using System.Text;

namespace P05_LinkedQueue
{
    public class LinkedQueue<T>
    {
        private int size;
        private Node<T> head;
        private Node<T> tail;

        public int Size { get; private set; }


        public void Enqueue(T element)
        {
            if (this.head==null)
            {
                this.head = new Node<T>();
                this.head.Value = element;
                this.tail = this.head;
            }
            else
            {
                var newNode = new Node<T>();
                newNode.Value = element;

                this.tail.Next = newNode;
                //newNode.Prev = this.tail;
                this.tail = newNode;
            }
            this.Size++;
        }

        public T Dequeue()
        {
            if (this.Size==0)
            {
                throw new InvalidOperationException();
            }
            T value = this.head.Value;
            this.head = this.head.Next;

            this.Size--;

            return value;
        }

        public T[] ToArray()
        {
            T[] toReturn = new T[this.Size];
            var tempHead = this.head;
            int index = 0;

            while (tempHead != null)
            {
                toReturn[index] = tempHead.Value;
                tempHead = tempHead.Next;
                index++;
            }
            return toReturn;
        }

        private class Node<T>
        {
            public Node()
            {               
            }

            public T Value { get; set; }

            public Node<T> Next { get; set; }
            //public Node<T> Prev { get; set; }
        }
    }
}
