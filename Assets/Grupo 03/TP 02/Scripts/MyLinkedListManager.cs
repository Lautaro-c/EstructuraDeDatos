using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MyLinkedListManager : MonoBehaviour
{
    private MyLinkedList<int> linkedList;
    private int secondsToDeactivate = 2;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI listText;
    [SerializeField] private TextMeshProUGUI emptyText;
    [SerializeField] private TMP_InputField generalInputField;
    [SerializeField] private GameObject tickSign;
    [SerializeField] private GameObject crossSing;

    [SerializeField] private Transform listContent;
    [SerializeField] private GameObject listItemPrefab;

    private void Start()
    {
        linkedList = new MyLinkedList<int>();
        UpdateAllUI();
    }

    private void Update()
    {
        countText.text = "Count: " + linkedList.Count.ToString();
        emptyText.text = "IsEmpty: " + linkedList.IsEmpty();
        //UpdateListText();
    }

    public void AddNumber()
    {
        if (int.TryParse(generalInputField.text, out int result))
        {
            linkedList.Add((int)result); // Explicitly cast to resolve ambiguity  
            Debug.Log("Número ingresado: " + result);
            UpdateAllUI();
        }
        else
        {
            Debug.LogWarning("No se ingresó un número válido.");
        }
    }

    public void AddNumbersRange()
    {
        string input = generalInputField.text;
        string[] parts = input.Split(',');
        int[] partsNumbers = new int[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            if (int.TryParse(parts[i], out int number))
            {
                partsNumbers[i] = number;
            }
        }
        linkedList.AddRange(partsNumbers);
        UpdateAllUI();
    }

    public void UpdateListText()
    {
        if (linkedList.Count > 0)
        {

            listText.text = "The full list is: " + linkedList.ToString();
        }
        else
        {
            listText.text = "The full list is: ";
        }
    }
    public void UpdateListVisual()
    {
        foreach (Transform child in listContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < linkedList.Count; i++)
        {
            GameObject item = Instantiate(listItemPrefab, listContent);
            item.transform.localScale = Vector3.one;

            item.GetComponent<ListItemUI>().Setup(i, linkedList[i]);
        }
    }
    private void UpdateAllUI()
    {
        countText.text = "Count: " + linkedList.Count.ToString();
        UpdateListText();
        UpdateListVisual();
    }

    public void ClearNumbers()
    {
        linkedList.Clear();
        UpdateAllUI();
    }

    public void RemoveNumber()
    {
        if (int.TryParse(generalInputField.text, out int result))
        {
            if (linkedList.Remove(result))
            {
                Debug.Log("Número borrado: " + result);
                tickSign.SetActive(true);
                UpdateAllUI();
            }
            else
            {
                crossSing.SetActive(true);
            }
            Invoke("DeactivateSigns", secondsToDeactivate);
        }
        else
        {
            Debug.LogWarning("No se ingresó un número válido.");
        }
    }

    public void DeactivateSigns()
    {
        tickSign.SetActive(false);
        crossSing.SetActive(false);
    }
    public void RemoveAtIndex()
    {
        if (int.TryParse(generalInputField.text, out int index))
        {
            if (linkedList.RemoveAt(index))
            {
                Debug.Log("Número borrado: " + index);
                tickSign.SetActive(true);
                UpdateAllUI();
            }
            else
            {
                crossSing.SetActive(true);
            }
            Invoke("DeactivateSigns", secondsToDeactivate);
        }
        else
        {
            Debug.LogWarning("No se ingresó un número válido.");
        }
    }
}
