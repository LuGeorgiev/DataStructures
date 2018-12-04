using System;

public class ArrayList<T>
{
    private const int initialCapacity = 2;
    private T[] elements;

    public ArrayList()
    {
        this.elements = new T[initialCapacity];
        this.Count = 0;
    }


    public int Count { get; private set; }

    public T this[int index]
    {
        get
        {
            if(index>=this.Count|| index<0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return this.elements[index];
        }

        set
        {
            if (index >= this.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.elements[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count == this.elements.Length)
        {
            this.Resize();
        }
        this.elements[Count] = item;
        this.Count++;
    }

    public T RemoveAt(int index)
    {
        if (index<0||index>=this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        T element = this.elements[index];
        this.elements[index] = default(T);
        this.Shift(index);
        this.Count--;

        if (this.Count<=this.elements.Length/4)
        {
            this.Shrink();
        }
        return element;
    }

    private void Shrink()
    {
        T[] copy = new T[this.elements.Length/2];
        for (int i = 0; i < this.Count; i++)
        {
            copy[i] = this.elements[i];
        }
        this.elements = copy;
    }

    private void Shift(int index)
    {
        for (int i = index; i < this.Count; i++)
        {
            this.elements[i] = this.elements[i + 1];
        }
    }

    private void Resize()
    {
        T[] copy = new T[this.elements.Length * 2];
        for (int i = 0; i < this.elements.Length; i++)
        {
            copy[i] = this.elements[i];
        }
        this.elements = copy;
    }
}
