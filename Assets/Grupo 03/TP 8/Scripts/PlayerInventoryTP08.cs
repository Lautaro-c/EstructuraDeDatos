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
        //Inicializa el inventario con una cantidad fija de slots
        inventoryItems = new MySetArray<ItemSOTP8>(slotsAmount);

        //Determina cuántos slots se llenarán aleatoriamente
        randomFilledSlots = Random.Range(0, slotsAmount);

        //Llena el inventario con ítems aleatorios sin repetir
        for (int i = 0; i < randomFilledSlots; i++)
        {
            randomItemID = Random.Range(0, 20);
            ItemSOTP8 item = allItems.items[randomItemID];

            //Intenta agregar el ítem al inventario (evita duplicados)
            if (inventoryItems.Add(item))
            {
                GameObject newItem = itemPrefab;
                newItem.GetComponent<Image>().sprite = item.Sprite;

                //Actualiza el nombre del ítem en el prefab
                Transform nameTextTransform = newItem.transform.Find("NameText");
                if (nameTextTransform != null)
                {
                    TextMeshProUGUI texto = nameTextTransform.GetComponent<TextMeshProUGUI>();
                    if (texto != null)
                    {
                        texto.text = allItems.items[randomItemID].name;
                    }
                }

                //Actualiza el precio del ítem en el prefab
                Transform priceTextTransform = newItem.transform.Find("PriceText");
                if (priceTextTransform != null)
                {
                    TextMeshProUGUI texto = priceTextTransform.GetComponent<TextMeshProUGUI>();
                    if (texto != null)
                    {
                        texto.text = "$" + allItems.items[randomItemID].Price.ToString();
                    }
                }

                //Instancia el ítem en la UI y limpia sus listeners
                GameObject instantiatedItem = Instantiate(newItem, this.transform);
                instantiatedItem.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            else
            {
                //Mensaje si el ítem ya estaba en el inventario
                Debug.Log("Repetition prevented");
            }
        }
    }
}