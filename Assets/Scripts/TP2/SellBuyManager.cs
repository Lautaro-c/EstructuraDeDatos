using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBuyManager: MonoBehaviour
{
    private StoreInventory storeInventory;
    private PlayerInventory playerInventory;
    private void Start()
    {
        storeInventory = new StoreInventory();
        playerInventory = new PlayerInventory();
    }
    public void Buy(Item item)
    {
        if (playerInventory.Money >= item.Precio)
        {
            //playerInventory.  //el dictionary de player, como accedemos al item especifico en lugar de cambiar todo
        }
    }
    public void Sell(Item item)
    {

    }
}
