using System;
using System.Collections.Generic;

public class BinaryHeap<T> 
    where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        //this.HipifyUp(item, this.heap.Count-1);
        this.HipyfyUpIterrative(this.heap.Count - 1);
    }

    private void HipyfyUpIterrative(int childIndex)
    {        
        T element = this.heap[childIndex];
        int parentIndex = (childIndex - 1) / 2;
        int compare = this.heap[parentIndex].CompareTo(element);
        while (parentIndex>=0 && compare<0)
        {
            this.Swap(parentIndex, childIndex);
            childIndex = parentIndex;
            parentIndex = (childIndex - 1) / 2;
            compare = this.heap[parentIndex].CompareTo(element);
        }
    }

    private void HipifyUp(T item, int index)
    {
        int parentIndex = (index - 1) / 2;
        if (parentIndex<0)
        {
            return;
        }
        int compare = this.heap[parentIndex].CompareTo(this.heap[index]);

        if (compare < 0)
        {
            //par is lower than child
            this.Swap(parentIndex, index);
            this.HipifyUp(this.heap[parentIndex], parentIndex);
        }
    }

    private void Swap(int parentIndex, int index)
    {
        T temp = this.heap[parentIndex];
        this.heap[parentIndex] = this.heap[index];
        this.heap[index] = temp;
    }

    public T Peek()
    {
        if (this.heap.Count==0)
        {
            throw new InvalidOperationException();
        }
        return this.heap[0];
    }

    public T Pull()
    {
        if (this.heap.Count==0)
        {
            throw new InvalidOperationException();
        }
        T element = this.heap[0];
        this.Swap(0, this.Count - 1);
        this.heap.RemoveAt(this.Count - 1);
        this.HeapifyDown( 0);

        return element;
    }

    private void HeapifyDown( int parentIndex)
    {        

        while (parentIndex < this.Count/2)
        {
            int childLeftIndex = (parentIndex * 2) + 1;

            //Check if there is right child && rightChild > leftChild
            if (childLeftIndex +1 < this.Count 
                && IsGreater(childLeftIndex + 1, childLeftIndex))
            {
                //Right Child
                childLeftIndex += 1;
            }

            int compare = this.heap[parentIndex].CompareTo(this.heap[childLeftIndex]);
            if (compare<0)
            {
                this.Swap(childLeftIndex, parentIndex);
            }
            parentIndex = childLeftIndex;
        }
    }

    private bool IsGreater(int right, int left)
    {
        return this.heap[left].CompareTo(this.heap[right]) < 0;
    }
}
