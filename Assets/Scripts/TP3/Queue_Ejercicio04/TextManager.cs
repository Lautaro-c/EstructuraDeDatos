using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    private ShadowManager shadowManager;
    void Start()
    {
        //textMesh = GetComponent<TextMeshProUGUI>();
        shadowManager = GetComponent<ShadowManager>();
    }
    void FixedUpdate()
    {
        textMesh.text = "Inputs in queue: " + shadowManager.counterAmount.ToString();
    }
}
