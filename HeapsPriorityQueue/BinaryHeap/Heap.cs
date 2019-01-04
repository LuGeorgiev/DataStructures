using System;

public static class Heap<T> 
    where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        ConstructHeap(arr);
        HeapSort(arr);
    }

    private static void HeapSort(T[] arr)
    {
        for (int i = arr.Length-1; i >=0; i--)
        {
            Swap(arr, 0, i);
            HeapifyDown(arr, 0, i);
        }
    }

    private static void ConstructHeap(T[] arr)
    {
        for (int i = arr.Length/2; i >=0; i--)
        {
            HeapifyDown(arr, i, arr.Length);
        }
    }

    private static void HeapifyUpIterrative(T[] arr, int childIndex)
    {
        T element = arr[childIndex];
        int parentIndex = (childIndex - 1) / 2;
        int compare = arr[parentIndex].CompareTo(element);
        while (parentIndex >= 0 && compare < 0)
        {
            Swap(arr, parentIndex, childIndex);
            childIndex = parentIndex;
            parentIndex = (childIndex - 1) / 2;
            compare = arr[parentIndex].CompareTo(element);
        }
    }

    private static void HeapifyDown(T[] arr, int parentIndex, int length)
    {

        while (parentIndex < length / 2)
        {
            int childLeftIndex = (parentIndex * 2) + 1;

            //Check if there is right child && rightChild > leftChild
            if (childLeftIndex + 1 < length
                && IsGreater(arr,childLeftIndex + 1, childLeftIndex))
            {
                //Right Child
                childLeftIndex += 1;
            }

            int compare = arr[parentIndex].CompareTo(arr[childLeftIndex]);
            if (compare < 0)
            {
                Swap(arr,childLeftIndex, parentIndex);
            }
            parentIndex = childLeftIndex;
        }
    }

    private static bool IsGreater(T[] arr, int right, int left)
    {
        return arr[left].CompareTo(arr[right]) < 0;
    }

    private static void Swap(T[] arr,int parentIndex, int index)
    {
        T temp = arr[parentIndex];
        arr[parentIndex] = arr[index];
        arr[index] = temp;
    }
}
