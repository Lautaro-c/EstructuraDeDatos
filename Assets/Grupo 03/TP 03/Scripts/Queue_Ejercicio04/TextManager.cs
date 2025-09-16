using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField]private ShadowManager shadowManager;
    void Start()
    {
        //textMesh = GetComponent<TextMeshProUGUI>();
        //shadowManager = GetComponent<ShadowManager>();
    }
    void Update()
    {
        textMesh.text = "Inputs in queue: " + shadowManager.Queue.Count.ToString();
    }
}
