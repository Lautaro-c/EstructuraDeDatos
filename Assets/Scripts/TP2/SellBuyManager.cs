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
    [SerializeField] private TMP_Text itemPrice;
    [SerializeField] private TMP_Text itemSellPrice;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemSellName;
    [SerializeField] private Sprite spritePera;
    [SerializeField] private Sprite spriteManzana;
    [SerializeField] private Sprite spriteTomate;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private TMP_Text playerMoney;
    [SerializeField] private TMP_Text fruitAmount;
    private void Start()
    {
        storeInventory = new StoreInventory();
        playerInventory = new PlayerInventory();
        RefreshActualMoney();
    }
    public void ShowItemInfo()
    {
        string nombreItem = buyDropdown.options[buyDropdown.value].text;
        Item item = storeInventory.GetItemByName(nombreItem);
        itemPrice.text = item.Precio.ToString();
        itemName.text = item.Nombre;
        

        switch (item.Nombre)
        {
            case "Manzana":
                buyButton.image.sprite = spriteManzana;
                break;
            case "Pera":
                buyButton.image.sprite = spritePera;
                break;
            case "Tomate":
                buyButton.image.sprite = spriteTomate;
                break;
        }
    }
    public void ShowSellItemInfo()
    {
        string nombreItem = sellDropdown.options[sellDropdown.value].text;
        Item item = storeInventory.GetItemByName(nombreItem);
        int newPrice = item.Precio;
        newPrice = (newPrice * 90) / 100;
        itemSellPrice.text = newPrice.ToString();
        itemSellName.text = item.Nombre;

        switch (item.Nombre)
        {
            case "Manzana":
                sellButton.image.sprite = spriteManzana;
                break;
            case "Pera":
                sellButton.image.sprite = spritePera;
                break;
            case "Tomate":
                sellButton.image.sprite = spriteTomate;
                break;
        }
        if (playerInventory.PlayerItems.ContainsKey(item.ID))
        {
            fruitAmount.text = playerInventory.PlayerItems[item.ID].ToString();
        }
        else
        {
            fruitAmount.text = "0";
        }
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

            RefreshActualMoney();

            Debug.Log("Compraste un/a: " + item.Nombre + " y te queda: " + playerInventory.Money + " pesos");
        }
        else
        {
            Debug.Log("No tenes suficiente plata");
        }
        ShowSellItemInfo();
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

                RefreshActualMoney();

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
    private void RefreshActualMoney()
    {
        playerMoney.text = playerInventory.Money.ToString();
    }
}
