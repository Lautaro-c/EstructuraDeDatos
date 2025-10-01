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

    private int[] myArray = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };
    private float initialYPos = 391;
    void Start()
    {
        myBst = new MyBST<int>();
        gameObjectsList = new SimpleList<GameObject>();
        for (int i = 0; i < myArray.Length; i++)
        {
            myBst.Insert(myArray[i]);
        }
        DrawTree();
    }
    public int GetDepth(Node<int> root, Node<int> target, int depth = 0)
    {
        if (root == null) return -1;
        if (root == target) return depth;

        int left = GetDepth(root.Left, target, depth + 1);
        if (left != -1) return left;

        return GetDepth(root.Right, target, depth + 1);
    }

    public void DrawTree()
    {
        SimpleList<Node<int>> nodesList = myBst.InOrder();
        float horizontalSpacing = 100f;
        float verticalSpacing = 140f;

        for (int i = 0; i < nodesList.Count; i++)
        {
            Node<int> node = nodesList[i];
            int depth = GetDepth(myBst.Root, node);

            float x = (i - nodesList.Count / 2f) * horizontalSpacing;
            float y = initialYPos - (verticalSpacing * depth);

            GameObject nodeGO = Instantiate(nodePrefab, canvasTransform);
            nodeGO.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = node.Data.ToString();
            nodeGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            gameObjectsList.Add(nodeGO);
        }

        UpdateInOrderUI();
        int balanceFactor = myBst.GetBalanceFactor();
        GetBalanceFactor(balanceFactor);
        ShowLevelOrder();
    }

    public void ClearTree()
    {
        for (int i = 0; i < gameObjectsList.Count; i++)
        {
            Destroy(gameObjectsList[i]);
        }
        gameObjectsList.Clear();
        myBst.Clear();
    }
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
        DrawTree();
    }

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
    public void GetBalanceFactor(int factor)
    {
        switch (factor)
        {
            case <=-1: 
                balanceFactorText.text = $"Árbol cargado a la derecha, el factor es de {factor}";
                break;
            case 0:
                balanceFactorText.text = $"Árbol balanceado, el factor es de {factor}";
                break;
            case >=1:
                balanceFactorText.text = $"Árbol cargado a la izquierda, el factor es de {factor}";
                break;
        }
    }
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
