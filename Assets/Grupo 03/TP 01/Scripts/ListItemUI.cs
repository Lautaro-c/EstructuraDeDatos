using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI indexText;
    [SerializeField] private TextMeshProUGUI valueText;

    public void Setup(int index, int value)
    {
        indexText.text = index.ToString();
        valueText.text = value.ToString();
    }
}
