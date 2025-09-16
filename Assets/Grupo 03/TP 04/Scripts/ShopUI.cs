using System;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public enum SortCriteria
{
    ID,
    Name,
    Price,
    Rarity,
    Tipe
}

public class ShopUI : MonoBehaviour
{
    [SerializeField] ItemListSO allItems;
    [SerializeField] private PlayerShop playerShop;
    [SerializeField] GameObject itemPrefab;
    SimpleList<ItemSO> itemList = new SimpleList<ItemSO>();
    private SimpleList<GameObject> childrenList;
    private Dictionary<int, int> itemQuantities;

    void Start()
    {
        childrenList = new SimpleList<GameObject>();
        itemList.AddRange(allItems.items);
        itemQuantities = new Dictionary<int, int>();

        ItemSOSorter sorter = new ItemSOSorter();
        IIndexableList<ItemSO> sortedItems = ApplySort(itemList, SortCriteria.ID);

        // Instanciamos los ítems ordenados
        for (int i = 0; i < sortedItems.Count; i++)
        {
            ItemSO item = sortedItems[i];
            GameObject instantiatedItem = Instantiate(itemPrefab, transform);
            instantiatedItem.GetComponent<Image>().sprite = item.Sprite;
            itemQuantities.Add(item.ID, 10);

            Transform nameText = instantiatedItem.transform.Find("NameText");
            if (nameText != null)
            {
                TextMeshProUGUI texto = nameText.GetComponent<TextMeshProUGUI>();
                if (texto != null) texto.text = item.ItemName;
            }

            Transform priceText = instantiatedItem.transform.Find("PriceText");
            if (priceText != null)
            {
                TextMeshProUGUI texto = priceText.GetComponent<TextMeshProUGUI>();
                if (texto != null) texto.text = "$" + item.ItemPrice.ToString();
            }
            Transform rarityText = instantiatedItem.transform.Find("RarityText");
            if (rarityText != null)
            {
                TextMeshProUGUI texto = rarityText.GetComponent<TextMeshProUGUI>();
                if (texto != null) texto.text = item.Rareza.ToString();
            }
            Transform amountTextTransform = instantiatedItem.transform.Find("AmountText");
            if (amountTextTransform != null)
            {
                TextMeshProUGUI texto = amountTextTransform.GetComponent<TextMeshProUGUI>();
                if (texto != null)
                {
                    texto.text = "Amount: " + itemQuantities[i].ToString();
                }
            }

            instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
            instantiatedItem.GetComponent<Button>().onClick.AddListener(() => playerShop.BuyItem(item));
            childrenList.Add(instantiatedItem);
        }
    }

    public void RemoveItem(ItemSO item)
    {
        if (item != null && itemQuantities[item.ID] >  0)
        {
            itemQuantities[item.ID] -= 1;
            UpdateUI();
        }
    }

    public void AddItem(ItemSO item)
    {
        if (item != null)
        {
            itemQuantities[item.ID] += 1;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        foreach (var kvp in itemQuantities)
        {
            if (kvp.Value > 0)
            {
                Debug.Log("Pasamos el if");
                childrenList[kvp.Key].gameObject.SetActive(true);
                Transform amountTextTransform = childrenList[kvp.Key].transform.Find("AmountText");
                if (amountTextTransform != null)
                {
                    TextMeshProUGUI texto = amountTextTransform.GetComponent<TextMeshProUGUI>();
                    if (texto != null)
                    {
                        texto.text = "Amount: " + itemQuantities[kvp.Key].ToString();
                    }
                }
            }
            else
            {
                childrenList[kvp.Key].gameObject.SetActive(false);
            }
        }
    }

    private IIndexableList<ItemSO> ApplySort(IIndexableList<ItemSO> list, SortCriteria criterio)
    {
        ItemSOSorter sorter = new ItemSOSorter();
        switch (criterio)
        {
            case SortCriteria.ID:
                return sorter.SortBy(list, item => item.ID);
            case SortCriteria.Name:
                return sorter.SortBy(list, item => item.ItemName);
            case SortCriteria.Price:
                return sorter.SortBy(list, item => item.ItemPrice);
            case SortCriteria.Rarity:
                return sorter.SortBy(list, item => item.Rareza);
            case SortCriteria.Tipe:
                return sorter.SortBy(list, item => item.Tipo);
            default:
                return list;
        }
    }

    public void SortByName()
    {
        SimpleList<ItemSO> tempList = (SimpleList<ItemSO>)ApplySort(itemList, SortCriteria.Name);
        for (int i = 0; i < tempList.Count; i++)
        {
            for (int j = 0; j < childrenList.Count; j++)
            {
                if (childrenList[j].transform.Find("NameText").GetComponent<TextMeshProUGUI>().text == tempList[i].name)
                {
                    childrenList[j].transform.SetSiblingIndex(i);
                    Canvas.ForceUpdateCanvases();
                    break;
                }
            }
        }
        itemList = tempList;
    }
    public void SortByPrice()
    {
        SimpleList<ItemSO> tempList = (SimpleList<ItemSO>)ApplySort(itemList, SortCriteria.Price);
        for (int i = 0; i < tempList.Count; i++)
        {
            for (int j = 0; j < childrenList.Count; j++)
            {
                if (childrenList[j].transform.Find("PriceText").GetComponent<TextMeshProUGUI>().text == "$" + tempList[i].ItemPrice.ToString())
                {
                    childrenList[j].transform.SetSiblingIndex(i);
                    Canvas.ForceUpdateCanvases();
                    break;
                }
            }
        }
        itemList = tempList;
    }

    public void SortByRarity()
    {
        SimpleList<ItemSO> tempList = (SimpleList<ItemSO>)ApplySort(itemList, SortCriteria.Rarity);
        for (int i = 0; i < tempList.Count; i++)
        {
            for (int j = 0; j < childrenList.Count; j++)
            {
                if (childrenList[j].transform.Find("RarityText").GetComponent<TextMeshProUGUI>().text == tempList[i].Rareza.ToString())
                {
                    Debug.Log("Hubo coincidencia");
                    childrenList[j].transform.SetSiblingIndex(i);
                    Canvas.ForceUpdateCanvases();
                    break;
                }
            }
        }
        itemList = tempList;
    }

}

