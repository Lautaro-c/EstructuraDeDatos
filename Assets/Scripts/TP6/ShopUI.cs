using System;
using TMPro;
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

    void Start()
    {
        childrenList = new SimpleList<GameObject>();
        itemList.AddRange(allItems.items);

        ItemSOSorter sorter = new ItemSOSorter();
        IIndexableList<ItemSO> sortedItems = ApplySort(itemList, SortCriteria.ID);

        // Instanciamos los ítems ordenados
        for (int i = 0; i < sortedItems.Count; i++)
        {
            ItemSO item = sortedItems[i];
            GameObject instantiatedItem = Instantiate(itemPrefab, transform);
            instantiatedItem.GetComponent<Image>().sprite = item.Sprite;

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

            instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
            instantiatedItem.GetComponent<Button>().onClick.AddListener(() => playerShop.BuyItem(item));
            childrenList.Add(instantiatedItem);
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
                    Debug.Log("Hubo una coincidencia en nombres el objeto: " + j + "Con la posicion: " + i);
                    childrenList[i].transform.SetSiblingIndex(i);
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
            if (childrenList[i].transform.Find("PriceText").GetComponent<TextMeshProUGUI>().text == "$" + tempList[i].ItemPrice.ToString())
            {
                Debug.Log("Hubo una coincidencia en precios");
                transform.GetChild(i).SetSiblingIndex(i);
            }
        }
        itemList = tempList;
    }

    public void SortByRarity()
    {
        SimpleList<ItemSO> tempList = (SimpleList<ItemSO>)ApplySort(itemList, SortCriteria.Rarity);
        for (int i = 0; i < tempList.Count; i++)
        {
            Debug.Log(tempList[i].ItemName);
        }
        itemList = tempList;
    }

}

