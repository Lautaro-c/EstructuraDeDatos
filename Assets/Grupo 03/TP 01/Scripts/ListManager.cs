using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    private SimpleList<int> list;
    private int secondsToDeactivate = 2;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI listText;
    [SerializeField] private TextMeshProUGUI actualValueText;
    [SerializeField] private TMP_InputField generalInputField;
    [SerializeField] private TMP_InputField numIDInputField;
    [SerializeField] private TMP_InputField newNumInputField;
    [SerializeField] private GameObject tickSign;
    [SerializeField] private GameObject crossSing;

    [SerializeField] private Transform listContent;
    [SerializeField] private GameObject listItemPrefab;

    private void Start()
    {
        list = new SimpleList<int>();
        UpdateAllUI();
    }

    private void Update()
    {
        countText.text = "Count: " + list.Count.ToString();
        UpdateListText();

    }

    public void AddNumber()
    {
        if (int.TryParse(generalInputField.text, out int result))
        {
            list.Add(result);
            UpdateAllUI();
            generalInputField.text = "";
        }
        else
        {
            Debug.LogWarning("No se ingresó un número válido.");
        }
    }

    public void AddNumbersRange()
    {
        string input = generalInputField.text;
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
        UpdateAllUI();
    }

    public void UpdateListText()
    {
        if (list.Count > 0)
        {
            listText.text = "The full list is: " + list.ToString();
        }
        else
        {
            listText.text = "The full list is: ";
        }
    }

    public void ClearNumbers()
    { 
        list.Clear();
        UpdateAllUI();
    }

    public void RemoveNumber()
    {
        if (int.TryParse(generalInputField.text, out int result))
        {
            if (list.Remove(result))
            {
                Debug.Log("Número borrado: " + result);
                tickSign.SetActive(true);
                UpdateAllUI();
            }
            else
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
            if (list.Count - 1 >= result)
            {
                actualValueText.text = "Actual value: " + list[result].ToString();
            }
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
                UpdateAllUI();
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
    public void UpdateListVisual()
    {
        foreach (Transform child in listContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < list.Count; i++)
        {
            GameObject item = Instantiate(listItemPrefab, listContent);
            item.transform.localScale = Vector3.one;

            item.GetComponent<ListItemUI>().Setup(i, list[i]);
        }
    }
    private void UpdateAllUI()
    {
        countText.text = "Count: " + list.Count.ToString();
        UpdateListText();
        UpdateListVisual();
    }


}
