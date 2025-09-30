using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class BinarySearchTree<T> : MonoBehaviour where T : IComparable<T>
{
    public Node<T> Root;
    MyQueue<Node<T>> queue = new MyQueue<Node<T>>();
    public BinarySearchTree()
    {
        Root = new Node<T>(default);
    }
    public void Insert(T value)
    {
        Root = InsertRecursive(Root, value);
    }
    private Node<T> InsertRecursive(Node<T> current, T value)
    {
        if (current == null)
        {
            current = new Node<T>(value);
        }
        if (value.CompareTo(current.Data) < 0)
        {
            current.Left = InsertRecursive(current.Left, value);
        } else if (value.CompareTo(current.Data) >= 0)
        {
            current.Right = InsertRecursive(current.Right, value);
        }
        return current;
    }

    public int GetHeight()
    {
        return GetHeightRecursive(Root);
    }
    private int GetHeightRecursive(Node<T> current)
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

    public SimpleList<Node<T>> InOrder()
    {
        SimpleList<Node<T>> inOrderList = new SimpleList<Node<T>>();
        InOrderRecursive(Root, inOrderList);
        return inOrderList;
    }
    private void InOrderRecursive(Node<T> current, SimpleList<Node<T>> list)
    {
        if (current == null)
        {
            return;
        }
        InOrderRecursive(current.Left, list);
        list.Add(current);
        InOrderRecursive(current.Right, list);
    }

    public SimpleList<Node<T>> PreOrder()
    {
        SimpleList<Node<T>> preOrderList = new SimpleList<Node<T>>();
        PreOrderRecursive(Root, preOrderList);
        return preOrderList;
    }

    private void PreOrderRecursive(Node<T> current, SimpleList<Node<T>> list)
    {
        if (current == null)
        {
            return;
        }
        list.Add(current);
        PreOrderRecursive(current.Left, list);
        PreOrderRecursive(current.Right, list);
    }

    public SimpleList<Node<T>> PostOrder()
    {
        SimpleList<Node<T>> postOrderList = new SimpleList<Node<T>>();
        PostOrderRecursive(Root, postOrderList);
        return postOrderList;
    }

    private void PostOrderRecursive(Node<T> current, SimpleList<Node<T>> list)
    {
        if (current == null)
        {
            return;
        }

        PostOrderRecursive(current.Left, list);
        PostOrderRecursive(current.Right, list);
        list.Add(current);
    }

    public SimpleList<Node<T>> LevelOrder()
    {
        SimpleList<Node<T>> levelOrderList = new SimpleList<Node<T>>();
        queue.Clear();

        if (Root == null || Root.Data.Equals(default(T))) return levelOrderList;

        BFS(Root);

        for (int i = 0; i < queue.Count; i++)
        {
            levelOrderList.Add(queue.Dequeue());
        }

        return levelOrderList;
    }

    public void BFS(Node<T> current)
    {
        queue.Enqueue(current);
        while (queue.Count > 0)
        {
            current = queue.Dequeue();
            Debug.Log(current.Data.ToString());
            if(current.Left != null)
            {
                queue.Enqueue(current.Left);
            }
            if (current.Right != null)
            {
                queue.Enqueue(current.Right);
            }
        }

    }


}