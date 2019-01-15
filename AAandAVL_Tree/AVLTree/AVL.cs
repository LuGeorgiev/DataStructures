﻿using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }
        node.Height=1+ Math.Max(this.Height(node.Left), this.Height(node.Right));

        // balancing
        int balance = this.Height(node.Left) - this.Height(node.Right);
       
        if (balance > 1)
        {
            int childBalance = this.Height(node.Left.Left) - this.Height(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = this.RotateLeft(node.Left);
            }
            node = this.RotateRight(node);
        }
        else if(balance < 1)
        {

        }

        return node;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        Node<T> newRoot = node.Left;
        newRoot.Left = newRoot.Right;
        newRoot.Right = node;

        newRoot.Height = Math.Max(this.Height(newRoot.Left), this.Height(newRoot.Right)) + 1;

        return newRoot;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        Node<T> newRoot = node.Right;
        newRoot.Right = newRoot.Left;
        newRoot.Left = node;

        newRoot.Height =Math.Max( this.Height(newRoot.Left) , this.Height(newRoot.Right)) + 1;

        return newRoot;
    }

    private int Height(Node<T> node)
    {
        if (node==null)
        {
            return 0;
        }

        return node.Height;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    //private class Node<T> 
    //    where T : IComparable<T>
    //{
    //    public T Value;
    //    public Node<T> Left;
    //    public Node<T> Right;
    //    public int Height;

    //    public Node(T value)
    //    {
    //        this.Value = value;
    //        this.Height = 1;
    //    }
    //}
}
