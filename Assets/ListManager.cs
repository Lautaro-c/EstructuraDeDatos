using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    private List<int> list;
    private int secondsToDeactivate = 2;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI lastNumText;
    [SerializeField] private TextMeshProUGUI actualValueText;
    [SerializeField] private TMP_InputField addInputField;
    [SerializeField] private TMP_InputField removeInputField;
    [SerializeField] private TMP_InputField numIDInputField;
    [SerializeField] private TMP_InputField newNumInputField;
    [SerializeField] private TMP_InputField numRangeInputField;
    [SerializeField] private GameObject tickSign;
    [SerializeField] private GameObject crossSing;

    private void Start()
    {
        list = new List<int>();
    }

    private void Update()
    {
        countText.text = "Count: " + list.Count.ToString();
        UpdateLastNumText();
    }

    public void AddNumber()
    {
        if (int.TryParse(addInputField.text, out int result))
        {
            list.Add(result);
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
        list.AddRange(partsNumbers);
    }

    public void UpdateLastNumText()
    {
        if (list.Count > 0)
        {
            lastNumText.text = "Last number added: " + list[list.Count - 1].ToString();
        }else
        {
            lastNumText.text = "Last number added: ";
        }
    }

    public void ClearNumbers()
    { 
        list.Clear(); 
    }

    public void RemoveNumber()
    {
        if (int.TryParse(removeInputField.text, out int result))
        {
            if (list.Remove(result))
            {
                Debug.Log("Número borrado: " + result);
                tickSign.SetActive(true);
            }else
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
            actualValueText.text = "Actual value: " + list[result].ToString();
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
            if (list.Count - 1 >= result)
            {
                list[result] = result2;
            }
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
