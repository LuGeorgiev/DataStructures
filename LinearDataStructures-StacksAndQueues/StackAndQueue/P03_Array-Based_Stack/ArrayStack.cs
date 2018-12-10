using System;

namespace P03_ArrayBased_Stack
{
    public class ArrayStack<T>
    {
        private T[] elements;
        private const int InitialCapacity = 16;

        public ArrayStack()
        {
            this.elements = new T[InitialCapacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Push(T element)
        {
            this.elements[this.Count] = element;
            this.Count++;
            if (this.Count==this.elements.Length)
            {
                this.Grow();
            }
        }

        public T Pop()
        {
            if (this.Count==0)
            {
                throw new InvalidOperationException();
            }

            T element = this.elements[this.Count-1];
            this.Count--;

            return element;
        }

        public T[] ToArray()
        {
            var result = new T[this.Count];
            for (int i = 0; i <= this.Count-1; i++)
            {
                result[this.Count-i-1] = this.elements[i];
            }
            return result;
        }

        private void Grow()
        {
            var copy = new T[this.elements.Length*2];
            for (int i = 0; i < elements.Length; i++)
            {
                copy[i] = elements[i];
            }
            this.elements = copy;
        }
    }
}
