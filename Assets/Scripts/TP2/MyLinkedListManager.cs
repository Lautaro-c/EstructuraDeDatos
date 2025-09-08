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
    [SerializeField] private TextMeshProUGUI lastNumText;
    [SerializeField] private TextMeshProUGUI actualValueText;
    [SerializeField] private TextMeshProUGUI emptyText;
    [SerializeField] private TMP_InputField addInputField;
    [SerializeField] private TMP_InputField removeInputField;
    [SerializeField] private TMP_InputField numIDInputField;
    [SerializeField] private TMP_InputField newNumInputField;
    [SerializeField] private TMP_InputField numRangeInputField;
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
        UpdateLastNumText();
    }

    public void AddNumber()
    {
        if (int.TryParse(addInputField.text, out int result))
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
        string input = numRangeInputField.text;
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

    public void UpdateLastNumText()
    {
        if (linkedList.Count > 0)
        {
            lastNumText.text = "Last number added: " + linkedList[linkedList.Count - 1].ToString();
        }
        else
        {
            lastNumText.text = "Last number added: ";
        }
    }

    public void ClearNumbers()
    {
        linkedList.Clear();
    }

    public void RemoveNumber()
    {
        if (int.TryParse(removeInputField.text, out int result))
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

    public void ShowNumValue()
    {
        if (int.TryParse(numIDInputField.text, out int result))
        {
            actualValueText.text = "Actual value: " + linkedList[result].ToString();
        }
        else
        {
            Debug.LogWarning("No se ingresó un número válido.");
        }
    }
    
    public void ChangeValue()
    {
        if (int.TryParse(numIDInputField.text, out int result) && int.TryParse(newNumInputField.text, out int result2))
        {
            linkedList.Insert(result, result2);
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
}
