using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class MyBST<T> where T : IComparable<T>
{
    //Crea referencia a un nodo raiz 
    public Node<T> Root;
    //Inicializa el BST con la raiz nula
    public MyBST()
    {
        Root = null;
    }
    //Inserta un valor al arbol llavando a InsertResursive en el root
    public void Insert(T value)
    {
        Root = InsertRecursive(Root, value);
    }
    private Node<T> InsertRecursive(Node<T> current, T value)
    {
        //Si el nodo actual esta vacio agrega el valor y deja de ejecutarse devolviendo el nuevo nodo
        if (current == null)
        {
            return new Node<T>(value);
        }
        //Si el nodo no esta vacio compara la data nueva con la del nodo actual
        //Si es menor intenta agregar la data al nodo hijo izquierdo del actual llamandose a si misma
        if (value.CompareTo(current.Data) < 0)
        {
            current.Left = InsertRecursive(current.Left, value);
          //Si el data es mayor o igual intenta agregarla al nodo hijo derecho del actual llamandose a si
          //misma
        } else if (value.CompareTo(current.Data) >= 0)
        {
            current.Right = InsertRecursive(current.Right, value);
        }
        //Devuelve el nodo actual sin modificarlo
        return current;
    }

    //Llama a la función recursiva para devolver un entero
    public int GetHeight()
    {
        return GetHeightRecursive(Root);
    }
    private int GetHeightRecursive(Node<T> current)
    {
        //si el actual es nulo devulve menos 2 (se interpreta como que no tiene altuna) 
        if (current == null)
        {
            return -1;
        }
        //llama recursivamente para medir la altura del subarvol izquierdo
        int leftHeight = GetHeightRecursive(current.Left);
        //llama recursivamente para medir la altura del subarvol derecho
        int rightHeight = GetHeightRecursive(current.Right);

        //Cuando esas alturas terminan de llamarse revisa cual es la mas grande y a esa le suma 1
        return Math.Max(leftHeight, rightHeight) + 1;
    }

    public int GetBalanceFactor()
    {
        if (Root == null)
        {
            return -1;
        }
        //recive la altura del subarbol izquierdo y le resta uno
        int subLeftTree = GetHeightRecursive(Root.Left) - 1;
        //recive la altura del subarbol derecho y le resta uno
        int subRightTree = GetHeightRecursive(Root.Right) - 1;
        //a la altura del subarbol izquierdo le resta la altura del subarbol derecho. 
        int balanceFactor = subLeftTree - subRightTree;
        //devuelve el resultado de la resta anterior
        return balanceFactor;
    }

    //devulve una lista de todos los nodos ordenados usando InOrder
    public SimpleList<Node<T>> InOrder()
    {
        //Crea la lista
        SimpleList<Node<T>> inOrderList = new SimpleList<Node<T>>();
        //Llama a una función recursiva
        InOrderRecursive(Root, inOrderList);
        //Devuelve la lista
        return inOrderList;
    }
    private void InOrderRecursive(Node<T> current, SimpleList<Node<T>> list)
    {
        //revisa que el nodo actual no sea nulo
        if (current == null)
        {
            return;
        }
        //llama recursivamente a la función para el nodo izquierdo
        InOrderRecursive(current.Left, list);
        //agrega el nodo actual a la lista
        list.Add(current);
        //llama recursivamente a la función para el nodo derecho
        InOrderRecursive(current.Right, list);
    }

    //devulve una lista de todos los nodos ordenados usando PreOrder
    public SimpleList<Node<T>> PreOrder()
    {
        SimpleList<Node<T>> preOrderList = new SimpleList<Node<T>>();
        PreOrderRecursive(Root, preOrderList);
        return preOrderList;
    }

    private void PreOrderRecursive(Node<T> current, SimpleList<Node<T>> list)
    {
        //revisa que el nodo actual no sea nulo
        if (current == null)
        {
            return;
        }
        //Agrega el nodo actual a la lista
        list.Add(current);
        //llama recursivamente a la función para el nodo izquierdo
        PreOrderRecursive(current.Left, list);
        //llama recursivamente a la función para el nodo derecho
        PreOrderRecursive(current.Right, list);
    }

    //devulve una lista de todos los nodos ordenados usando PostOrder
    public SimpleList<Node<T>> PostOrder()
    {
        SimpleList<Node<T>> postOrderList = new SimpleList<Node<T>>();
        PostOrderRecursive(Root, postOrderList);
        return postOrderList;
    }

    private void PostOrderRecursive(Node<T> current, SimpleList<Node<T>> list)
    {
        //revisa que el nodo actual no sea nulo
        if (current == null)
        {
            return;
        }
        //llama recursivamente a la función para el nodo izquierdo
        PostOrderRecursive(current.Left, list);
        //llama recursivamente a la función para el nodo derecho
        PostOrderRecursive(current.Right, list);
        //Agrega el nodo actual a la lista
        list.Add(current);
    }
    //devulve una lista de todos los nodos ordenados usando LevelOrder
    public SimpleList<Node<T>> LevelOrder()
    {
        //crea la lista
        SimpleList<Node<T>> levelOrderList = new SimpleList<Node<T>>();
        //Si el root esta vació o su data es igual a la default devulve la lista vacía 
        if (Root == null || Root.Data.Equals(default(T)))
        {
            return levelOrderList;
        }

        levelOrderList = BFS(Root);

        return levelOrderList;
    }

    public SimpleList<Node<T>> BFS(Node<T> start)
    {
        //Crea una queue
        MyQueue<Node<T>> queue = new MyQueue<Node<T>>();
        //Crea la lista que va a devolver
        SimpleList<Node<T>> levelOrderList = new SimpleList<Node<T>>();
        //agrega el nodo inicial a la fila
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            //El nodo actual es igual al nodo que salga de la fila
            Node <T> current = queue.Dequeue();
            //Se agrega ese nodo a la lista
            levelOrderList.Add(current);
            //Si tiene un hijo izquierdo lo agrega a la fila
            if(current.Left != null)
            {
                queue.Enqueue(current.Left);
            }
            //Si tiene un hijo derecho lo agrega a la fila
            if (current.Right != null)
            {
                queue.Enqueue(current.Right);
            }
        }
        Debug.Log(levelOrderList.Count.ToString());
        //Devuelve la lista
        return levelOrderList;
    }

    //Limpia el arbol haciendo que la raiz sea nula
    public void Clear()
    {
        Root = null;
    }


}