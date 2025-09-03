using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ItemListSO allItems;
    [SerializeField] private PlayerShop playerShop;
    [SerializeField] GameObject itemPrefab;
    void Start()
    {
        
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
            instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
            instantiatedItem.GetComponent<Button>().onClick.AddListener(() => playerShop.BuyItem(item));

        }
    }

    void Update()
    {
        
    }
}
