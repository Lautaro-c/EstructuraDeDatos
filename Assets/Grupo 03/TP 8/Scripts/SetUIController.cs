using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetUIController : MonoBehaviour
{
    [SerializeField] ItemListSOTP08 allItems;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] private PlayerInventoryTP08 player1Inventory;
    [SerializeField] private PlayerInventoryTP08 player2Inventory;
    private MySetArray<ItemSOTP8> player1InventorySet;
    private MySetArray<ItemSOTP8> player2InventorySet;

    private void Start()
    {
        player1InventorySet = player1Inventory.inventoryItems;
        player2InventorySet = player2Inventory.inventoryItems;
    }

    private void ClearItems()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShaderItems()
    {
        ClearItems();
        MySet<ItemSOTP8> Set = player1InventorySet.IntersectWith(player2InventorySet);
        for (int i = 0; i < Set.Count(); i++)
        {
            ItemSOTP8 item = Set.elements[i];
            GameObject newItem = itemPrefab;
            newItem.GetComponent<Image>().sprite = item.Sprite;
            Transform nameTextTransform = newItem.transform.Find("NameText");
            if (nameTextTransform != null)
            {
                TextMeshProUGUI texto = nameTextTransform.GetComponent<TextMeshProUGUI>();
                if (texto != null)
                {
                    texto.text = Set.elements[i].name;
                }
            }
            Transform priceTextTransform = newItem.transform.Find("PriceText");
            if (priceTextTransform != null)
            {
                TextMeshProUGUI texto = priceTextTransform.GetComponent<TextMeshProUGUI>();
                if (texto != null)
                {
                    texto.text = "$" + Set.elements[i].Price.ToString();
                }
            }
            GameObject instantiatedItem = Instantiate(newItem, this.transform);
            instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    public void MissingItems()
    {
        ClearItems();
        MySet<ItemSOTP8> preSet = player1InventorySet.UnionWith(player2InventorySet);
        MySetArray<ItemSOTP8> allItemsSet = new MySetArray<ItemSOTP8>();
        for (int i = 0; i < 20;  i++)
        {
            allItemsSet.Add(allItems.items[i]);
        }

        MySet<ItemSOTP8> Set = allItemsSet.DifferenceWith(preSet);
        for (int i = 0; i < Set.Count(); i++)
        {
            ItemSOTP8 item = Set.elements[i];
            GameObject newItem = itemPrefab;
            newItem.GetComponent<Image>().sprite = item.Sprite;
            Transform nameTextTransform = newItem.transform.Find("NameText");
            if (nameTextTransform != null)
            {
                TextMeshProUGUI texto = nameTextTransform.GetComponent<TextMeshProUGUI>();
                if (texto != null)
                {
                    texto.text = Set.elements[i].name;
                }
            }
            Transform priceTextTransform = newItem.transform.Find("PriceText");
            if (priceTextTransform != null)
            {
                TextMeshProUGUI texto = priceTextTransform.GetComponent<TextMeshProUGUI>();
                if (texto != null)
                {
                    texto.text = "$" + Set.elements[i].Price.ToString();
                }
            }
            GameObject instantiatedItem = Instantiate(newItem, this.transform);
            instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}



