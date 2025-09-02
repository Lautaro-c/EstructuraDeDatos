using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ItemListSO allItems;
    [SerializeField] GameObject itemPrefab;
    void Start()
    {
        
        for (int i = 0; i < allItems.items.Length; i++)
        {
            GameObject newItem = itemPrefab;
            Debug.Log(i.ToString());
            newItem.GetComponent<Image>().sprite = allItems.items[i].Sprite;
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


            Instantiate(newItem, transform);

        }
    }

    void Update()
    {
        
    }
}
