using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //ID, Cantidad
    Dictionary<int, int> itemStock;
    [SerializeField] SimpleList<int> itemQuantities;
    [SerializeField] ItemListSO shopItems;
    //[SerializeField] ItemSO[] shopItems;

    void Start()
    {
        //Inicializamos el diccionario
        itemStock = new Dictionary<int, int>();
        itemQuantities = new SimpleList<int>(shopItems.items.Length);
        for (int i = 0; i < shopItems.items.Length; i++)
        {
            itemQuantities[i] = 1;
        }
        for (int i = 0; i < shopItems.items.Length; i++)
        {
            itemStock.Add(shopItems.items[i].ID, itemQuantities[i]);
        }
    }

    public void BuyFromPlayer(ItemSO itemToBuy)
    {
        if (itemStock.ContainsKey(itemToBuy.ID)) itemStock[itemToBuy.ID] += 1;
        else itemStock.Add(itemToBuy.ID, 1);
    }
}
