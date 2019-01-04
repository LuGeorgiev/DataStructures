using System;
using System.Collections.Generic;

public class Tree<T>
{
    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Childeren = new List<Tree<T>>(children);
    }

    public T Value { get; private set; }

    public List<Tree<T>> Childeren { get; private set; }

    public void Print(int indent = 0)
    {
        this.Print(this, indent);
    }

    private void Print(Tree<T> node, int indent)
    {
        Console.WriteLine($"{new string(' ',indent)}{node.Value}");
        foreach (var child in node.Childeren)
        {            
            child.Print(indent + 1);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);
        foreach (var child in this.Childeren)
        {
            child.Each(action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
        List<T> result = new List<T>();
        this.DFS(this, result);

        return result;
    }

    private void DFS(Tree<T> node, List<T> result)
    {
        foreach (var child in node.Childeren)
        {
            this.DFS(child, result);
        }

        result.Add(node.Value);
    }

    public IEnumerable<T> OrderDFSStack()
    {
        Stack<T> result = new Stack<T>();
        Stack<Tree<T>> stack = new Stack<Tree<T>>();

        stack.Push(this);
        while (stack.Count>0)
        {
            Tree<T> curent = stack.Pop();
            foreach (var child in curent.Childeren)
            {
                stack.Push(child);
            }
            result.Push(curent.Value);
        }

        return result;
    }

    public IEnumerable<T> OrderBFS()
    {
        var queueu = new Queue<Tree<T>>();
        var result = new List<T>();
        queueu.Enqueue(this);
        while (queueu.Count>0)
        {
            var current = queueu.Dequeue();

            foreach (var child in current.Childeren)
            {
                queueu.Enqueue(child);
            }
            result.Add(current.Value);
        }

        return result;
    }
}
