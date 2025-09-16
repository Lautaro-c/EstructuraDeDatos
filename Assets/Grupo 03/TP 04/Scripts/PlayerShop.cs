using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class PlayerShop : MonoBehaviour
{
    private int money = 500;
    [SerializeField] private Shop shop;

    [SerializeField] ItemListSO allItems;
    private SimpleList<GameObject> playerItems;
    private Dictionary<int, int> itemQuantities;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] private TextMeshProUGUI moneyText;
    SimpleList<ItemSO> itemList = new SimpleList<ItemSO>();
    private ItemSOSorter sorter;
    [SerializeField] private ShopUI shopUI;

    void Start()
    {
        sorter = new ItemSOSorter();
        itemQuantities = new Dictionary<int, int>();
        playerItems = new SimpleList<GameObject>();
        moneyText.text = "Money: " + money.ToString();
        itemList.AddRange(allItems.items);
        for (int i = 0; i < allItems.items.Length; i++)
        {
            GameObject newItem = itemPrefab;
            ItemSO item = allItems.items[i];
            newItem.GetComponent<Image>().sprite = item.Sprite;
            Transform nameTextTransform = newItem.transform.Find("NameText");
            itemQuantities.Add(item.ID, 0);

            if (nameTextTransform != null)
            {
                TextMeshProUGUI texto = nameTextTransform.GetComponent<TextMeshProUGUI>();
                if (texto != null)
                {
                    texto.text = allItems.items[i].name;
                }
            }
            Transform priceTextTransform = newItem.transform.Find("PriceText");
            if (priceTextTransform != null)
            {
                TextMeshProUGUI texto = priceTextTransform.GetComponent<TextMeshProUGUI>();
                if (texto != null)
                {
                    texto.text = "$" + allItems.items[i].ItemPrice.ToString();
                }
            }
            Transform amountTextTransform = newItem.transform.Find("AmountText");
            if (amountTextTransform != null)
            {
                TextMeshProUGUI texto = amountTextTransform.GetComponent<TextMeshProUGUI>();
                if (texto != null)
                {
                    texto.text = "Amount: " + itemQuantities[i].ToString();
                }
            }
            Transform rarityText = newItem.transform.Find("RarityText");
            if (rarityText != null)
            {
                TextMeshProUGUI texto = rarityText.GetComponent<TextMeshProUGUI>();
                if (texto != null) texto.text = item.Rareza.ToString();
            }
            GameObject instantiatedItem = Instantiate(newItem, transform);
            instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
            instantiatedItem.GetComponent<Button>().onClick.AddListener(() => RemoveItem(item));
            instantiatedItem.GetComponent<Button>().onClick.AddListener(() => shopUI.AddItem(item));
            instantiatedItem.SetActive(false);
            playerItems.Add(instantiatedItem);
        }
    }

    public void BuyItem(ItemSO itemSO)
    {
        if (money >= itemSO.ItemPrice)
        {
            ItemSO itemSO1 = shop.SellItem(itemSO.ID);
            if(itemSO1 != null)
            {
                Debug.Log("Compre: " +  itemSO1.name);
                itemQuantities[itemSO.ID] += 1;
                money -= itemSO.ItemPrice;
                UpdateUI();
                Debug.Log(money.ToString());
            }
        }
    }

    private void RemoveItem(ItemSO item)
    {
        if(item != null)
        {
            money += item.ItemPrice;
            itemQuantities[item.ID] -= 1;
            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        moneyText.text = "Money: " + money.ToString();
        foreach (var kvp in itemQuantities)
        {
            if (kvp.Value > 0)
            {
                Debug.Log("Pasamos el if");
                playerItems[kvp.Key].gameObject.SetActive(true);
                Transform amountTextTransform = playerItems[kvp.Key].transform.Find("AmountText");
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
                playerItems[kvp.Key].gameObject.SetActive(false);
            }      
        }
    }

    private IIndexableList<ItemSO> ApplySort(IIndexableList<ItemSO> list, SortCriteria criterio)
    {
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
            for (int j = 0; j < playerItems.Count; j++)
            {
                if (playerItems[j].transform.Find("NameText").GetComponent<TextMeshProUGUI>().text == tempList[i].name)
                {
                    playerItems[j].transform.SetSiblingIndex(i);
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
            for (int j = 0; j < playerItems.Count; j++)
            {
                if (playerItems[j].transform.Find("PriceText").GetComponent<TextMeshProUGUI>().text == "$" + tempList[i].ItemPrice.ToString())
                {
                    playerItems[j].transform.SetSiblingIndex(i);
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
            for (int j = 0; j < playerItems.Count; j++)
            {
                if (playerItems[j].transform.Find("RarityText").GetComponent<TextMeshProUGUI>().text == tempList[i].Rareza.ToString())
                {
                    Debug.Log("Hubo coincidencia");
                    playerItems[j].transform.SetSiblingIndex(i);
                    Canvas.ForceUpdateCanvases();
                    break;
                }
            }
        }
        itemList = tempList;
    }
}
