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
    [SerializeField] private TMP_InputField GeneralInputField;
    [SerializeField] private GameObject tickSign;
    [SerializeField] private GameObject crossSing;

    private void Start()
    {
        linkedList = new MyLinkedList<int>();
    }

    private void Update()
    {
        countText.text = "Count: " + linkedList.Count.ToString();
        emptyText.text = "IsEmpty: " + linkedList.IsEmpty();
        UpdateListText();
    }

    public void AddNumber()
    {
        if (int.TryParse(GeneralInputField.text, out int result))
        {
            linkedList.Add((int)result); // Explicitly cast to resolve ambiguity  
            Debug.Log("Número ingresado: " + result);
        }
        else
        {
            Debug.LogWarning("No se ingresó un número válido.");
        }
    }

    public void AddNumbersRange()
    {
        string input = GeneralInputField.text;
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
    }

    public void UpdateListText()
    {
        if (linkedList.Count > 0)
        {
            string fullList = "";
            for (int i = 0; i < linkedList.Count; i++)
            {
                fullList += linkedList[i].ToString() + " - ";
            }
            listText.text = "The full list is: " + fullList;
        }
        else
        {
            listText.text = "The full list is: ";
        }
    }

    public void ClearNumbers()
    {
        linkedList.Clear();
    }

    public void RemoveNumber()
    {
        if (int.TryParse(GeneralInputField.text, out int result))
        {
            if (linkedList.Remove(result))
            {
                Debug.Log("Número borrado: " + result);
                tickSign.SetActive(true);
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
        if (int.TryParse(GeneralInputField.text, out int index))
        {
            if (linkedList.RemoveAt(index))
            {
                Debug.Log("Número borrado: " + index);
                tickSign.SetActive(true);
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
