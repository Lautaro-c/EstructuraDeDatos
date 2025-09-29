using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class BinarySearchTree
{
    public Node<int> Root;

    public BinarySearchTree()
    {
        Root = new Node<int>(default);
    }
    public void Insert(int value)
    {
        Root = InsertRecursive(Root, value);
    }
    private Node<int> InsertRecursive(Node<int> current, int value)
    {
        if (current == null)
        {
            current = new Node<int>(value);
        }
        if (value < current.Data)
        {
            current.Left = InsertRecursive(current.Left, value);
        } else if (value >= current.Data)
        {
            current.Right = InsertRecursive(current.Right, value);
        }
        return current;
    }

    public int GetHeight()
    {
        return GetHeightRecursive(Root);
    }
    private int GetHeightRecursive(Node<int> current)
    {
        if (current == null)
        {
            return -1;
        }
        int leftHeight = GetHeightRecursive(current.Left);
        int rightHeight = GetHeightRecursive(current.Right);

        return Math.Max(leftHeight, rightHeight) + 1;
    }

    public int GetBalanceFactor()
    {
        int subLeftTree = GetHeightRecursive(Root.Left) - 1;
        int subRightTree = GetHeightRecursive(Root.Right) - 1;
        int balanceFactor = subLeftTree - subRightTree;
        return balanceFactor;
    }

    public SimpleList<Node<int>> InOrder()
    {
        SimpleList<Node<int>> inOrderList = new SimpleList<Node<int>>();
        InOrderRecursive(Root, inOrderList);
        return inOrderList;
    }
    private void InOrderRecursive(Node<int> current, SimpleList<Node<int>> list)
    {
        if (current == null)
        {
            return;
        }
        InOrderRecursive(current.Left, list);
        list.Add(current);
        InOrderRecursive(current.Right, list);
    }

    public SimpleList<Node<int>> PreOrder()
    {
        SimpleList<Node<int>> preOrderList = new SimpleList<Node<int>>();
        PreOrderRecursive(Root, preOrderList);
        return preOrderList;
    }

    private void PreOrderRecursive(Node<int> current, SimpleList<Node<int>> list)
    {
        if (current == null)
        {
            return;
        }
        list.Add(current);
        PreOrderRecursive(current.Left, list);
        PreOrderRecursive(current.Right, list);
    }

    public SimpleList<Node<int>> PostOrder()
    {
        SimpleList<Node<int>> postOrderList = new SimpleList<Node<int>>();
        PostOrderRecursive(Root, postOrderList);
        return postOrderList;
    }

    private void PostOrderRecursive(Node<int> current, SimpleList<Node<int>> list)
    {
        if (current == null)
        {
            return;
        }

        PostOrderRecursive(current.Left, list);
        PostOrderRecursive(current.Right, list);
        list.Add(current);
    }
}