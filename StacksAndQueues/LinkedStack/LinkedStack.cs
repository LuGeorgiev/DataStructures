using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LinkedStack
{
    class LinkedStack<T> : IEnumerable<T>
    {
        private class StackNode
        {
            public StackNode(T value, StackNode next)
            {
                this.Value = value;
                this.Next = next;
            }

            public T Value { get; set; }

            public StackNode Next { get; set; }
        }


        private StackNode top;

        public int Count { get; private set; }

        public void Push(T item)
        {
            this.top = new StackNode(item, this.top);

            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            T result = this.top.Value;
            this.top = this.top.Next;
            this.Count--;

            return result;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.top.Value;
        }

        public T[] ToAray()
        {
            var result = new T[this.Count];
            var current = this.top;
            int index = 0;
            while (current!=null)
            {
                result[index] = current.Value;
                current = current.Next;
                index++;
            }
            
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
