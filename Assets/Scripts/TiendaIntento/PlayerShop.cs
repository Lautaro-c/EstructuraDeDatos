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
    private Dictionary<GameObject, int> itemQuantities;
    private Dictionary<int, int> itemQuantities2;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] private TextMeshProUGUI moneyText;

    void Start()
    {
        itemQuantities = new Dictionary<GameObject, int>();
        itemQuantities2 = new Dictionary<int, int>();
        playerItems = new SimpleList<GameObject>();
        moneyText.text = "Money: " + money.ToString();
        for (int i = 0; i < allItems.items.Length; i++)
        {
            GameObject newItem = itemPrefab;
            ItemSO item = allItems.items[i];
            newItem.GetComponent<Image>().sprite = item.Sprite;
            Transform nameTextTransform = newItem.transform.Find("NameText");
            itemQuantities2.Add(item.ID, 0);

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
                    texto.text = "Amount: " + itemQuantities2[i].ToString();
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
                itemQuantities2[itemSO.ID] += 1;
                //itemQuantities[itemSO.ID] +=1;
                money -= itemSO.ItemPrice;
                UpdateUI();
                Debug.Log(money.ToString());
            }
        }
    }
    private void UpdateUI()
    { /*
        foreach (var kvp in itemQuantities)
        {
            if(kvp.Value > 0)
            {
                allItems.items[kvp.Key].gameObject.SetActive(true);
            }
        }*/
        moneyText.text = "Money: " + money.ToString();
        foreach (var kvp in itemQuantities2)
        {
            if (kvp.Value > 0)
            {
                Debug.Log("Actualize la UI: " +  kvp.Key);
                playerItems[kvp.Key].gameObject.SetActive(true);
                Transform amountTextTransform = playerItems[kvp.Key].transform.Find("AmountText");
                if (amountTextTransform != null)
                {
                    TextMeshProUGUI texto = amountTextTransform.GetComponent<TextMeshProUGUI>();
                    if (texto != null)
                    {
                        texto.text = "Amount: " + itemQuantities2[kvp.Key].ToString();
                    }
                }
                //allItems.items[kvp.Key].gameObject.SetActive(true);
            }
        }
    }
}
