using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Shop : MonoBehaviour
{
    //ID, Cantidad
    Dictionary<int, ItemSO> itemStock;
    [SerializeField] ItemListSO shopItems;

    void Start()
    {
        //Inicializamos el diccionario
        itemStock = new Dictionary<int, ItemSO>();
        //itemQuantities = new SimpleList<int>(shopItems.items.Length);

        for (int i = 0; i < shopItems.items.Length; i++)
        {
            itemStock.Add(shopItems.items[i].ID, shopItems.items[i]);
        }
    }
    public ItemSO SellItem(int itemId)
    {
        if (!itemStock.ContainsKey(itemId))
        {
            return null;
        }
        ItemSO item = itemStock[itemId]; 
        return item;
    }
}
