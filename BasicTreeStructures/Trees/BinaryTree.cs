using System;

public class BinaryTree<T>
{
    public BinaryTree(T value, 
        BinaryTree<T> leftChild = null, 
        BinaryTree<T> rightChild = null)
    {
        this.Value = value;

        this.Left = leftChild;
        this.Right = rightChild;
    }

    public T Value { get; private set; }

    public BinaryTree<T> Left { get; private set; }

    public BinaryTree<T> Right { get; private set; }

    public void PrintIndentedPreOrder(int indent = 0) // Root Left Right
    {
        //this.PrintIntendedPreOrder(this, 0);

        Console.WriteLine(new string(' ', indent)+this.Value);
        if (this.Left!=null)
        {
            this.Left.PrintIndentedPreOrder(indent + 2);
        }
        if (this.Right != null)
        {
            this.Right.PrintIndentedPreOrder(indent + 2);
        }
    }

    private void PrintIntendedPreOrder(BinaryTree<T> node, int indend)
    {
        if (node==null)
        {
            return;
        }
        Console.WriteLine($"{new string(' ', indend)}{node.Value}");
        this.PrintIntendedPreOrder(this.Left, indend+2);
        this.PrintIntendedPreOrder(this.Right, indend+2);
    }

    public void EachInOrder(Action<T> action) // Letf Root Right Ordered
    {
        //if (this.Left!=null)
        //{
        //    this.Left.EachInOrder(action);
        //}
        //action(this.Value);
        //if (this.Right != null)
        //{
        //    this.Right.EachInOrder(action);
        //}

        this.EachInOrderRecursive(this, action);
    }

    public void EachInOrderRecursive(BinaryTree<T> node, Action<T> action) 
    {
        if (node==null)
        {
            return;
        }
        this.EachInOrderRecursive(node.Left, action);
        action(node.Value);
        this.EachInOrderRecursive(node.Right, action);
    }

    public void EachPostOrder(Action<T> action) // Left Right Root
    {
        //if (this.Left != null)
        //{
        //    this.Left.EachInOrder(action);
        //}

        //if (this.Right != null)
        //{
        //    this.Right.EachInOrder(action);
        //}
        //action(node.Value);

        this.EachPostOrderRecursive(this, action);
    }

    public void EachPostOrderRecursive(BinaryTree<T> node ,Action<T> action)
    {
        if (node==null)
        {
            return;
        }
        this.EachPostOrderRecursive(node.Left, action);
        this.EachPostOrderRecursive(node.Right, action);
        action(node.Value);
    }
}
