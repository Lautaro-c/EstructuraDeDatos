using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellBuyManager: MonoBehaviour
{
    private StoreInventory storeInventory;
    private PlayerInventory playerInventory;

    [SerializeField] private TMP_Dropdown sellDropdown;
    [SerializeField] private TMP_Dropdown buyDropdown;
    private void Start()
    {
        storeInventory = new StoreInventory();
        playerInventory = new PlayerInventory();
    }
    public void BuyDropdown()
    {
        string nombreItem = buyDropdown.options[buyDropdown.value].text;
        Item item = storeInventory.GetItemByName(nombreItem);
        Buy(item);
    }
    public void SellDropdown()
    {
        string nombreItem = sellDropdown.options[sellDropdown.value].text;
        Item item = storeInventory.GetItemByName(nombreItem);
        Sell(item);
    }
    public void Buy(Item item)
    {
        if (item == null) return;
        if (playerInventory.Money >= item.Precio)
        {
            if (playerInventory.PlayerItems.ContainsKey(item.ID))
            {
                playerInventory.PlayerItems[item.ID]++;
            }
            else
            {
                playerInventory.PlayerItems[item.ID] = 1;
            }
            playerInventory.Money -= item.Precio;
            Debug.Log("Compraste un/a: " + item.Nombre + " y te queda: " + playerInventory.Money + " pesos");
        }
        else
        {
            Debug.Log("No tenes suficiente plata");
        }
    }
    public void Sell(Item item)
    {
        if (item == null) return;
        if (playerInventory.PlayerItems.ContainsKey(item.ID))
        {
            if (playerInventory.PlayerItems[item.ID] >= 1)
            {
                playerInventory.Money += (item.Precio * 90) / 100;
                playerInventory.PlayerItems[item.ID]--;

                Debug.Log("Vendiste un/a: " + item.Nombre + " y ahora tenes: " + playerInventory.Money + " pesos");

                if (playerInventory.PlayerItems[item.ID] == 0)
                {
                    Debug.Log("Te quedaste sin: " + item.Nombre);
                    playerInventory.PlayerItems.Remove(item.ID);
                }
            }
        }
        else
        {
            Debug.Log("No tenes ningun: " + item.Nombre);
            return;
        }
    }
}
