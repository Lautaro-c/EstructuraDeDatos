using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework.Interfaces;
using UnityEngine;


public class Recursion : MonoBehaviour
{
    private int[] numsArray;

    private void Start()
    {   

    }

    public int Sum(int[] nums, int currentIndex)
    {
        if(currentIndex < numsArray.Length - 1)
        {
            return nums[currentIndex] + Sum(nums, currentIndex + 1);
        }

        //caso base
        return nums[currentIndex];
    }

    public int Fibonacci(int num)
    {
        if (num == 0) return 0;
        if (num == 1) return 1;

        return Fibonacci(num-1) + Fibonacci(num-2);
    }

    public int Factorial(int num)
    {
        if (num == 0 || num == 1) return 1;

        else return num * Factorial(num - 1);
    }

    public string Pyramid(int altura)
    {
        // altura = 4 --> 4 renglones
        // 4 -> 1 x
        // 3 -> 3 x
        // 2 -> 5 x
        // 1 -> 7 x
        // 0 -> return

        int xCounter = 1;

        return "" + Pyramid(altura - 1);
    }
}
