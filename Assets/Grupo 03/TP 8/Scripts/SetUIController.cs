using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetUIController : MonoBehaviour
{
    [SerializeField] ItemListSOTP08 allItems;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] private PlayerInventoryTP08 player1Inventory;
    [SerializeField] private PlayerInventoryTP08 player2Inventory;
    [SerializeField] private GameObject inventory1Text;
    [SerializeField] private GameObject inventory2Text;

    private MySetArray<ItemSOTP8> player1InventorySet;
    private MySetArray<ItemSOTP8> player2InventorySet;

    private void Start()
    {
        player1InventorySet = player1Inventory.inventoryItems;
        player2InventorySet = player2Inventory.inventoryItems;
    }

    //Elimina todos los ítems visuales instanciados en la UI
    private void ClearItems()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    //Muestra los ítems que ambos jugadores tienen en común
    public void ShaderItems()
    {
        DesactivateTexts();
        ClearItems();

        //Calcula la intersección entre los dos inventarios
        MySet<ItemSOTP8> Set = player1InventorySet.IntersectWith(player2InventorySet);

        //Instancia visualmente cada ítem compartido
        for (int i = 0; i < Set.Count(); i++)
        {
            ItemSOTP8 item = Set.elements[i];
            GameObject newItem = itemPrefab;
            newItem.GetComponent<Image>().sprite = item.Sprite;

            //Actualiza el nombre del ítem
            var nameText = newItem.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = item.name;
            }

            //Actualiza el precio del ítem
            var priceText = newItem.transform.Find("PriceText")?.GetComponent<TextMeshProUGUI>();
            if (priceText != null)
            {
                priceText.text = "$" + item.Price;
            }

            //Instancia el ítem en la UI y limpia sus listeners
            GameObject instantiatedItem = Instantiate(newItem, this.transform);
            instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    //Muestra los ítems que ninguno de los jugadores tiene
    public void MissingItems()
    {
        DesactivateTexts();
        ClearItems();

        //Calcula la unión de los inventarios
        MySet<ItemSOTP8> preSet = player1InventorySet.UnionWith(player2InventorySet);

        //Crea un conjunto con todos los ítems posibles
        MySetArray<ItemSOTP8> allItemsSet = new MySetArray<ItemSOTP8>();
        for (int i = 0; i < 20; i++)
        {
            allItemsSet.Add(allItems.items[i]);
        }

        //Calcula la diferencia entre todos los ítems y los que ya tienen los jugadores
        MySet<ItemSOTP8> Set = allItemsSet.DifferenceWith(preSet);

        //Instancia visualmente cada ítem faltante
        for (int i = 0; i < Set.Count(); i++)
        {
            ItemSOTP8 item = Set.elements[i];
            GameObject newItem = itemPrefab;
            newItem.GetComponent<Image>().sprite = item.Sprite;

            var nameText = newItem.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = item.name;
            }

            var priceText = newItem.transform.Find("PriceText")?.GetComponent<TextMeshProUGUI>();
            if (priceText != null)
            {
                priceText.text = "$" + item.Price;
            }

            GameObject instantiatedItem = Instantiate(newItem, this.transform);
            instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    //Oculta los textos de cantidad de ítems
    private void DesactivateTexts()
    {
        inventory1Text.SetActive(false);
        inventory2Text.SetActive(false);
    }

    //Muestra cuántos ítems tiene cada jugador
    public void ItemsAmount()
    {
        ClearItems();
        inventory1Text.SetActive(true);
        inventory2Text.SetActive(true);

        inventory1Text.GetComponent<TextMeshProUGUI>().text = $"Player 1 has {player1InventorySet.Count()} items";
        inventory2Text.GetComponent<TextMeshProUGUI>().text = $"Player 2 has {player2InventorySet.Count()} items";
    }
}