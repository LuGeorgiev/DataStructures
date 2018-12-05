using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ReversedList
{
    class ReversedList<T> :IEnumerable<T>
    {
        private readonly int initialCapacity=2;
        private T[] elements;

        public ReversedList()
        {
            this.Count = 0;
            this.elements = new T[this.initialCapacity];
        }

        public T this[int index]
        {
            get
            {
                if (index<0||index>=this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return this.elements[this.Count - index - 1];
            }

            set
            {
                this.elements[this.Count - index - 1] = value;
            }
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get
            {
                return this.elements.Length;
            }
        }

        public void Add(T item)
        {
            this.elements[this.Count] = item;
            this.Count++;
            if (this.Count==this.Capacity)
            {
                this.Expand();
            }
        }

        public T RemoveAt(int index)
        {
            if (index<0||index>=this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            T item = this.elements[this.Count - index - 1];
            this.Shift(this.Count - index - 1);
            this.Count--;

            return item;
        }

        private void Shift(int index)
        {
            for (int i = index; i < this.Count; i++)
            {
                this.elements[i] = this.elements[i + 1];
            }
        }

        private void Expand()
        {
            var copy = new T[this.Capacity * 2];
            for (int i = 0; i < elements.Length;i++)
            {
                copy[i] = elements[i];
            }
            this.elements = copy;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count-1; i >=0;  i--)
            {
                yield return this.elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
