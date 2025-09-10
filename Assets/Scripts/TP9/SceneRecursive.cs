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
            text.text = "El golden ratio es de";
            int i = 0;
            int fib = recursion.Fibonacci(i);
            while (fib <= maxNum)
            {
                text.text += " " + fib + ", ";
                i++;
                fib = recursion.Fibonacci(i);
            }
        }
        else
        {
            text.text = "Ingrese un n�mero v�lido";
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
            text.text = "Ingrese un n�mero v�lido";
        }
    }
    public void ClearInputF()
    {
        inputField.text = "";
    }    
}
