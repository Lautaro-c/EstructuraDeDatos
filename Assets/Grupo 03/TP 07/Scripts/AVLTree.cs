using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVLTree<T> : MyBST<T> where T : IComparable<T>
{
    public AVLTree()
    {
        Root = null;
    }
    public Node<T> RightRotation(Node<T> y)
    {
        Node<T> x = y.Left;
        Node<T> t2 = x.Right;
        x.Right = y;
        y.Left = t2;
        return x;
    }

    public Node<T> LeftRotation(Node<T> y)
    {
        Node<T> x = y.Right;
        Node<T> t2 = x.Left;
        x.Left = y;
        y.Right = t2;
        return x;
    }

    public Node<T> LeftRightRotation(Node<T> node)
    {
        node.Left = LeftRotation(node.Left);
        return RightRotation(node);
    }

    public Node<T> RightLeftRotation(Node<T> node)
    {
        node.Right = RightRotation(node.Right);
        return LeftRotation(node);
    }

    public Node<T> BalanceTree(Node<T> actualNode, Node<T> insertedNode)
    {
        if (GetBalanceFactor() < -1 && insertedNode.Data.CompareTo(actualNode.Right.Data) > 0)
        {
            return LeftRotation(actualNode);
        }
        if (GetBalanceFactor() > 1 && insertedNode.Data.CompareTo(actualNode.Left.Data) < 0)
        {
            return RightRotation(actualNode);
        }
        if (GetBalanceFactor() < -1 && insertedNode.Data.CompareTo(actualNode.Left.Data) < 0)
        {
            return RightLeftRotation(actualNode);
        }
        if (GetBalanceFactor() > 1 && insertedNode.Data.CompareTo(actualNode.Right.Data) > 0)
        {
            return LeftRightRotation(actualNode);
        }
        return actualNode;
    }
}
