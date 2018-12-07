using System;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 4;
    private T[] elements;
    private int startIndex;
    private int endIndex;

    public int Count { get; private set; }

    public CircularQueue(int capacity = DefaultCapacity)
    {
        this.elements = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count>=this.elements.Length)
        {
            this.Resize();
        }
        this.elements[this.endIndex] = element;
        this.endIndex = (this.endIndex + 1) % this.elements.Length;
        this.Count++;
    }

    private void Resize()
    {
        T[] newArr = new T[this.elements.Length * 2];
        this.CopyAllElements(newArr);

        this.elements = newArr;
        this.startIndex = 0;
        this.endIndex = this.Count;

    }

    private void CopyAllElements(T[] newArray)
    {
        int currentIndex = 0;
        int currentHead = this.startIndex;
        while (currentIndex<this.Count)
        {
            newArray[currentIndex] = this.elements[currentHead];
            currentHead = (currentHead + 1) % this.elements.Length;
            currentIndex++;
        }
    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {
        if (this.Count==0)
        {
            throw new InvalidOperationException();
        }

        T result = this.elements[startIndex];
        startIndex = (startIndex + 1) % this.elements.Length;
        this.Count--;

        return result;
    }

    public T[] ToArray()
    {
        T[] result = new T[this.Count];
        for (int i = 0; i < this.Count; i++)
        {
            result[i] = this.elements[startIndex];
            startIndex = (startIndex + 1) % this.elements.Length;
        }

        return result;
    }
}


public class Example
{
    public static void Main()
    {

        CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
