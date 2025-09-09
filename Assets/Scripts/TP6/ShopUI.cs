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

    void Start()
    {
        // Convertimos el array a SimpleList
        itemList.AddRange(allItems.items);

        // Ordenamos según el criterio
        ItemSOSorter sorter = new ItemSOSorter();
        IIndexableList<ItemSO> sortedItems = ApplySort(itemList, SortCriteria.ID);

        // Instanciamos los ítems ordenados
        for (int i = 0; i < sortedItems.Count; i++)
        {
            ItemSO item = sortedItems[i];
            GameObject newItem = itemPrefab;

            newItem.GetComponent<Image>().sprite = item.Sprite;

            Transform nameText = newItem.transform.Find("NameText");
            if (nameText != null)
            {
                TextMeshProUGUI texto = nameText.GetComponent<TextMeshProUGUI>();
                if (texto != null) texto.text = item.ItemName;
            }

            Transform priceText = newItem.transform.Find("PriceText");
            if (priceText != null)
            {
                TextMeshProUGUI texto = priceText.GetComponent<TextMeshProUGUI>();
                if (texto != null) texto.text = "$" + item.ItemPrice.ToString();
            }

            GameObject instantiatedItem = Instantiate(newItem, transform);
            instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
            instantiatedItem.GetComponent<Button>().onClick.AddListener(() => playerShop.BuyItem(item));
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
            Debug.Log(tempList[i].ItemName);
        }
        itemList = tempList;
    }
    public void SortByPrice()
    {
        SimpleList<ItemSO> tempList = (SimpleList<ItemSO>)ApplySort(itemList, SortCriteria.Price);
        for (int i = 0; i < tempList.Count; i++)
        {
            Debug.Log(tempList[i].ItemName);
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
/*
public class ShopUI : MonoBehaviour
{
    [SerializeField] ItemListSO allItems;
    [SerializeField] private PlayerShop playerShop;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] private SimpleList<ItemSO> itemList;
    private ItemSOSorter sorter;
    void Start()
    {  
        sorter = new ItemSOSorter();
        for (int i = 0; i < allItems.items.Length; i++)
        {
            itemList.Add(allItems.items[i]);
            GameObject newItem = itemPrefab;
            ItemSO item = allItems.items[i];
            newItem.GetComponent<Image>().sprite = item.Sprite;
            Transform hijo = newItem.transform.Find("NameText");

            if (hijo != null)
            {
                TextMeshProUGUI texto = hijo.GetComponent<TextMeshProUGUI>();
                if (texto != null)
                {
                    texto.text = allItems.items[i].name;
                }
            }
            Transform hijo2 = newItem.transform.Find("PriceText");
            if (hijo != null)
            {
                TextMeshProUGUI texto = hijo.GetComponent<TextMeshProUGUI>();
                if (texto != null)
                {
                    texto.text = "$" + allItems.items[i].ItemPrice.ToString();
                }
            }
            GameObject instantiatedItem = Instantiate(newItem, transform);
            instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
            instantiatedItem.GetComponent<Button>().onClick.AddListener(() => playerShop.BuyItem(item));
        }
    }

}*/

