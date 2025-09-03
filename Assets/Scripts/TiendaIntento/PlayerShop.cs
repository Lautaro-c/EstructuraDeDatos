using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerShop : MonoBehaviour
{
    private int money = 500;
    [SerializeField] private Shop shop;

    [SerializeField] ItemListSO allItems;
    private SimpleList<GameObject> playerItems;
    private Dictionary<GameObject, int> itemQuantities;
    [SerializeField] GameObject itemPrefab;

    void Start()
    {
        itemQuantities = new Dictionary<GameObject, int>();

        for (int i = 0; i < allItems.items.Length; i++)
        {
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
                    texto.text = allItems.items[i].ItemPrice.ToString();
                }
            }
            GameObject instantiatedItem = Instantiate(newItem, transform);
            //instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
           // instantiatedItem.GetComponent<Button>().onClick.AddListener(() => playerShop.BuyItem(item));
           instantiatedItem.SetActive(false);
           playerItems.Add(instantiatedItem);
        }
    }
    void Update()
    {

    }
    public void BuyItem(ItemSO itemSO)
    {
        if (money >= itemSO.ItemPrice)
        {
            ItemSO itemSO1 = shop.SellItem(itemSO.ID);
            if(itemSO1 != null)
            {
                Debug.Log("Compre: " +  itemSO1.name);
                itemQuantities[itemSO.ID] +=1;
                UpdateUI();
                money -= itemSO.ItemPrice;
                Debug.Log(money.ToString());
            }
        }
    }
    private void UpdateUI()
    {
        foreach (var kvp in itemQuantities)
        {
            if(kvp.Value > 0)
            {
                allItems.items[kvp.Key].gameObject.SetActive(true);
            }
        }
    }
}
