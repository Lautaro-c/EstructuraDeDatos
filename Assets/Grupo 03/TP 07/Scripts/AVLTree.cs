using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que extiende MyBST y representa un árbol AVL genérico
public class AVLTree<T> : MyBST<T> where T : IComparable<T>
{
    //Constructor que inicializa el árbol AVL con la raíz nula
    public AVLTree()
    {
        Root = null;
    }

    //Realiza una rotación simple a la derecha
    public Node<T> RightRotation(Node<T> y)
    {
        //x es el hijo izquierdo de y
        Node<T> x = y.Left;
        //t2 es el hijo derecho de x
        Node<T> t2 = x.Right;
        //Se realiza la rotación: x pasa a ser la nueva raíz del subárbol
        x.Right = y;
        y.Left = t2;
        //Devuelve el nuevo nodo raíz del subárbol
        return x;
    }

    //Realiza una rotación simple a la izquierda
    public Node<T> LeftRotation(Node<T> y)
    {
        //x es el hijo derecho de y
        Node<T> x = y.Right;
        //t2 es el hijo izquierdo de x
        Node<T> t2 = x.Left;
        //Se realiza la rotación: x pasa a ser la nueva raíz del subárbol
        x.Left = y;
        y.Right = t2;
        //Devuelve el nuevo nodo raíz del subárbol
        return x;
    }

    //Realiza una rotación doble: primero izquierda en el hijo izquierdo, luego derecha en el nodo actual
    public Node<T> LeftRightRotation(Node<T> node)
    {
        node.Left = LeftRotation(node.Left);
        return RightRotation(node);
    }

    //Realiza una rotación doble: primero derecha en el hijo derecho, luego izquierda en el nodo actual
    public Node<T> RightLeftRotation(Node<T> node)
    {
        node.Right = RightRotation(node.Right);
        return LeftRotation(node);
    }

    //Balancea el árbol AVL después de una inserción
    public Node<T> BalanceTree(Node<T> actualNode, Node<T> insertedNode)
    {
        //Caso de desbalance hacia la derecha (Right-Right)
        if (GetBalanceFactor() < -1 && insertedNode.Data.CompareTo(actualNode.Right.Data) > 0)
        {
            return LeftRotation(actualNode);
        }
        //Caso de desbalance hacia la izquierda (Left-Left)
        if (GetBalanceFactor() > 1 && insertedNode.Data.CompareTo(actualNode.Left.Data) < 0)
        {
            return RightRotation(actualNode);
        }
        //Caso de desbalance hacia la derecha del hijo izquierdo (Left-Right)
        if (GetBalanceFactor() < -1 && insertedNode.Data.CompareTo(actualNode.Left.Data) < 0)
        {
            return RightLeftRotation(actualNode);
        }
        //Caso de desbalance hacia la izquierda del hijo derecho (Right-Left)
        if (GetBalanceFactor() > 1 && insertedNode.Data.CompareTo(actualNode.Right.Data) > 0)
        {
            return LeftRightRotation(actualNode);
        }
        //Si no hay desbalance, devuelve el nodo actual sin modificar
        return actualNode;
    }

    //Devuelve una lista con los datos ordenados usando recorrido InOrder
    public SimpleList<T> InOrderTraversal()
    {
        //Crea la lista que va a devolver
        SimpleList<T> result = new SimpleList<T>();
        //Llama a la función recursiva para llenar la lista
        InOrderTraversal(Root, result);
        //Devuelve la lista con los datos ordenados
        return result;
    }

    //Función recursiva para recorrer el árbol en orden
    private void InOrderTraversal(Node<T> node, SimpleList<T> result)
    {
        //Si el nodo actual es nulo, termina la ejecución
        if (node == null)
        {
            return;
        }
        //Recorre primero el hijo izquierdo
        InOrderTraversal(node.Left, result);
        //Agrega el dato del nodo actual a la lista
        result.Add(node.Data);
        //Recorre luego el hijo derecho
        InOrderTraversal(node.Right, result);
    }
}