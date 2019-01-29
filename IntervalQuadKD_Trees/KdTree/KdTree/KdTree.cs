using System;

public class KdTree
{
    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(Point2D point)
    {
        throw new NotImplementedException();
    }

    public void Insert(Point2D point)
    {
        this.root = this.Insert(this.root, point, 0);
    }

    private Node Insert(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return new Node(point);
        }

        int compare = depth % 2;
        if (compare==0)
        {
            if (node.Point.X.CompareTo(point.X)>0)
            {
                node.Left = this.Insert(node.Left, point, depth++);
            }
            else
            {
                node.Right = this.Insert(node.Right, point, depth++);
            }
        }
        else
        {
            if (node.Point.Y.CompareTo(point.Y) > 0)
            {
                node.Left = this.Insert(node.Left, point, depth++);
            }
            else
            {
                node.Right = this.Insert(node.Right, point, depth++);
            }
        }

        return node;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }
}
