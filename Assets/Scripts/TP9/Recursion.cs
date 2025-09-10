using System;
using UnityEngine;


public class Recursion : MonoBehaviour
{
    private int[] numsArray;

    private void Start()
    {
        Debug.Log(Fibonacci(24));
    }

    public int Sum(int[] nums, int currentIndex)
    {
        //caso recursivo
        if (currentIndex < numsArray.Length - 1)
        {
            return nums[currentIndex] + Sum(nums, currentIndex + 1);
        }


        return nums[currentIndex];
    }

    public int Fibonacci(int num)
    {
        if (num == 0) return 0;
        if (num == 1) return 1;

        //caso recursivo
        else return Fibonacci(num - 1) + Fibonacci(num - 2);
    }

    public int Factorial(int num)
    {
        if (num == 0 || num == 1) return 1;

        //caso recursivo
        else return num * Factorial(num - 1);
    }

    public string Pyramid(int maxHeight, int currentHeight = 0)
    {

        if (currentHeight == maxHeight)
            return "";

        int spaces = maxHeight - currentHeight - 1;
        int xCount = 2 * currentHeight + 1;

        string line = new string(' ', spaces) + new string('x', xCount) + "\n";

        //caso recursivo
        return line + Pyramid(maxHeight, currentHeight + 1);
    }
    public bool Palíndromo (string palabra)
    {
        // Caso base
        if (palabra.Length <= 1)
            return true;

        // Caso base
        if (palabra[0] != palabra[palabra.Length - 1])
            return false;

        // Llamada recursiva
        else return Palíndromo(palabra.Substring(1, palabra.Length - 2));
    }
}
