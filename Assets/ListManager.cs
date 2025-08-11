using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    private List<int> list;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TMP_InputField inputField;
    
    private int numeroEntero;

    public void LeerNumero()
    {
        if (int.TryParse(inputField.text, out int resultado))
        {
            numeroEntero = resultado;
            AddNumber(resultado);
            Debug.Log("Número ingresado: " + numeroEntero);
        }
        else
        {
            Debug.LogWarning("No se ingresó un número válido.");
        }
    }


    private void Start()
    {
        list = new List<int>(); 
    }

    private void Update()
    {
        countText.text = "Count: " + list.Count.ToString();
    }

    public void AddNumber(int number)
    { 
        Debug.Log(number.ToString());
        list.Add(number); 
    }

    public void ClearNumbers()
    { 
        list.Clear(); 
    }

    public void RemoveNumber(int number)
    {
        list.Remove(number);
    }

    public void AddRangeNumbers(int[] numbers)
    {
        list.AddRange(numbers);
    }
}
