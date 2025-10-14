using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryTP08 : MonoBehaviour
{
    [SerializeField] ItemListSOTP08 allItems;
    [SerializeField] GameObject itemPrefab;
    public MySetArray<ItemSOTP8> inventoryItems;
    private int slotsAmount = 10;
    private int randomFilledSlots = 0;
    private int randomItemID = 0;
    void Start()
    {
        inventoryItems = new MySetArray<ItemSOTP8>(slotsAmount);
        randomFilledSlots = Random.Range(0, slotsAmount);

        for (int i = 0; i < randomFilledSlots; i++)
        {
            randomItemID = Random.Range(0, 20);
            ItemSOTP8 item = allItems.items[randomItemID];
            if (inventoryItems.Add(item))
            {
                GameObject newItem = itemPrefab;
                newItem.GetComponent<Image>().sprite = item.Sprite;
                Transform nameTextTransform = newItem.transform.Find("NameText");
                if (nameTextTransform != null)
                {
                    TextMeshProUGUI texto = nameTextTransform.GetComponent<TextMeshProUGUI>();
                    if (texto != null)
                    {
                        texto.text = allItems.items[randomItemID].name;
                    }
                }
                Transform priceTextTransform = newItem.transform.Find("PriceText");
                if (priceTextTransform != null)
                {
                    TextMeshProUGUI texto = priceTextTransform.GetComponent<TextMeshProUGUI>();
                    if (texto != null)
                    {
                        texto.text = "$" + allItems.items[randomItemID].Price.ToString();
                    }
                }
                GameObject instantiatedItem = Instantiate(newItem, this.transform);
                instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else
            {
                Debug.Log("Repetition prevented");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
