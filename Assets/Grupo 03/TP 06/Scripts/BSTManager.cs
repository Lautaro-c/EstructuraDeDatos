using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BSTManager : MonoBehaviour
{
    private MyBST<int> myBst;
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private SimpleList<GameObject> gameObjectsList;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text inOrderText;
    [SerializeField] private TMP_Text preOrderText;
    [SerializeField] private TMP_Text balanceFactorText;
    [SerializeField] private TMP_Text levelOrderText;
    [SerializeField] private TMP_Text heightText;
    [SerializeField] private TMP_Text postOrderText;
    //Los valores con los que arranca el arbol
    private int[] myArray = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };
    private float initialYPos = 391;
    void Start()
    {
        //Inicializa el BST
        myBst = new MyBST<int>();
        //Inicializa la lista
        gameObjectsList = new SimpleList<GameObject>();
        //Por cada elemento del array con los valores iniciales los agrega al arbol. 
        for (int i = 0; i < myArray.Length; i++)
        {
            myBst.Insert(myArray[i]);
        }
        //Dibuja el arbol
        DrawTree();
    }
    //Calcula la profundidad desde el nodo raiz hasta un nodo objetivo
    public int GetDepth(Node<int> root, Node<int> target, int depth = 0)
    {
        //Si la raiz es nula devuelve -1
        if (root == null)
        {
            return -1;
        }
        //Si la raiz es el objetivo devuleve 0
        if (root == target)
        {
            return depth;
        }
        //llama recursivamente a la funcion para el nodo hijo izquierdo, sumandole 1 a la profundidad
        int left = GetDepth(root.Left, target, depth + 1);
        //Si despues de esas llamadas left es distinto a -1 (no era nulo) devuelve left
        if (left != -1)
        {
            return left;
        }
        //llama recursivamente a la funcion para el nodo hijo derecho, sumandole 1 a la profundidad
        return GetDepth(root.Right, target, depth + 1);
    }

    public void DrawTree()
    {
        //Crea una lista que es igual al arbol ordenado usando InOrder
        SimpleList<Node<int>> nodesList = myBst.InOrder();
        //Las distancias en X e Y que tiene que haber entre nodos para dibujarlos
        float horizontalSpacing = 75f;
        float verticalSpacing = 120f;
        //Recorre la lista de nodos
        for (int i = 0; i < nodesList.Count; i++)
        {
            //Revisa el primer nodo de la lista y calcula su profundidad
            Node<int> node = nodesList[i];
            int depth = GetDepth(myBst.Root, node);
            //Calcula donde se tienen que dibujar en X e Y los nodos a partir de las distancias que tiene
            //que haber entre ellos y su posicion en la listo o su profundidad
            float x = (i - nodesList.Count / 2f) * horizontalSpacing;
            float y = initialYPos - (verticalSpacing * depth);
            //Instancia un GameObject a partir del prefab del nodo y lo vuelve hijo del canvas
            GameObject nodeGO = Instantiate(nodePrefab, canvasTransform);
            //busca a el componentente text del hijo del GameObject y lo iguala a la data del nodo
            nodeGO.GetComponentInChildren<TextMeshProUGUI>().text = node.Data.ToString();
            //Iguala su posicion a la calculada arriba
            nodeGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            //lo agrega a la lista de GameObejects
            gameObjectsList.Add(nodeGO);
        }

        //Actualiza todos los textos de UI
        UpdateInOrderUI();
        UpdatePreOrderUI();
        UpdatePostOrderUI();
        int balanceFactor = myBst.GetBalanceFactor();
        GetBalanceFactor(balanceFactor);
        ShowLevelOrder();
        UpdateHeightUI();
    }
    //Limpia el arbol eliminando a todos los elementos de la lista de GameObjects, limpiandola y limpiando
    //el BST
    public void ClearTree()
    {
        for (int i = 0; i < gameObjectsList.Count; i++)
        {
            Destroy(gameObjectsList[i]);
        }
        gameObjectsList.Clear();
        myBst.Clear();
    }

    //Muestra el recorrido por niveles del árbol y lo actualiza en la UI
    public void ShowLevelOrder()
    {
        SimpleList<Node<int>> nodesList = myBst.LevelOrder();
        string result = "LevelOrder: ";
        for (int i = 0; i < nodesList.Count; i++)
        {
            result += nodesList[i].Data.ToString();
            if (i < nodesList.Count - 1)
                result += ", ";
        }
        levelOrderText.text = result;
    }

    //Agrega múltiples valores al árbol a partir del input del usuario
    public void AddRange()
    {
        string input = inputField.text;
        string[] parts = input.Split(',');
        for (int i = 0; i < parts.Length; i++)
        {
            if (int.TryParse(parts[i], out int number))
            {
                myBst.Insert(number);
            }
        }
        //Redibuja el árbol con los nuevos valores
        DrawTree();
    }

    //Actualiza el texto de UI con el recorrido InOrder del árbol
    private void UpdateInOrderUI()
    {
        SimpleList<Node<int>> nodesList = myBst.InOrder();
        string result = "InOrder: ";

        for (int i = 0; i < nodesList.Count; i++)
        {
            result += nodesList[i].Data.ToString();
            if (i < nodesList.Count - 1)
                result += ", ";
        }

        inOrderText.text = result;
    }

    //Actualiza el texto de UI con el factor de balance del árbol
    public void GetBalanceFactor(int factor)
    {
        switch (factor)
        {
            case <= -1:
                balanceFactorText.text = $"Árbol cargado a la derecha, el factor de balance es de {factor}";
                break;
            case 0:
                balanceFactorText.text = $"Árbol balanceado, el factor de balance es de {factor}";
                break;
            case >= 1:
                balanceFactorText.text = $"Árbol cargado a la izquierda, el factor de balance es de {factor}";
                break;
        }
    }

    //Actualiza el texto de UI con el recorrido PostOrder del árbol
    private void UpdatePostOrderUI()
    {
        SimpleList<Node<int>> nodesList = myBst.PostOrder();
        string result = "PostOrder: ";

        for (int i = 0; i < nodesList.Count; i++)
        {
            result += nodesList[i].Data.ToString();
            if (i < nodesList.Count - 1)
                result += ", ";
        }
        postOrderText.text = result;
    }

    //Actualiza el texto de UI con la altura del árbol
    private void UpdateHeightUI()
    {
        int height = myBst.GetHeight();
        heightText.text = "Altura: " + height.ToString();
    }

    //Actualiza el texto de UI con el recorrido PreOrder del árbol
    private void UpdatePreOrderUI()
    {
        SimpleList<Node<int>> nodesList = myBst.PreOrder();
        string result = "PreOrder: ";

        for (int i = 0; i < nodesList.Count; i++)
        {
            result += nodesList[i].Data.ToString();
            if (i < nodesList.Count - 1)
                result += ", ";
        }

        preOrderText.text = result;
    }

}
