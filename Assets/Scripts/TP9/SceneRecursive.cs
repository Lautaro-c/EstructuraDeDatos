using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneRecursive : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Recursion recursion;

    private void Start()
    {
        inputField = FindAnyObjectByType<TMP_InputField>();
        recursion = FindAnyObjectByType<Recursion>();
    }

    public void UseFibonacci()
    {
        if (int.TryParse(inputField.text, out int maxNum))
        {
            string output = "";

            for(int i = 0; i < maxNum; i++)
            {
                output += recursion.Fibonacci(i) + " , ";
            }

            text.text = "El golden ratio es de: " + output;
        }
        else
        {
            text.text = "Ingrese un número válido";
        }

    }
    public void UseFactorial()
    {
        if (int.TryParse(inputField.text, out int num))
        {
            int factorial = recursion.Factorial(num);
            text.text = "El factorial de " + num + " es " + factorial;
        }
        else
        {
            text.text = "Ingrese un número válido";
        }
    }
    public void ClearInputF()
    {
        inputField.text = "";
    }    
}
