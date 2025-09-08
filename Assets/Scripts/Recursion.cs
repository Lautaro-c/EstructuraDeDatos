using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework.Interfaces;
using UnityEngine;


public class Recursion : MonoBehaviour
{
    private int[] numsArray = { 1, 2, 3, 4 };

    private void Start()
    {
        Debug.Log(Sum(numsArray, 0));
    }

    private int Sum(int[] nums, int currentIndex)
    {
        if(currentIndex < numsArray.Length - 1)
        {
            return nums[currentIndex] + Sum(nums, currentIndex + 1);
        }

        //caso base
        return nums[currentIndex];
    }
}
