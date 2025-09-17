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
    [SerializeField] private Sprite spritePear;
    [SerializeField] private Sprite spriteApple;
    [SerializeField] private Sprite spriteTomato;
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
        string itemName = buyDropdown.options[buyDropdown.value].text;
        TValue item = storeInventory.GetItemByName(itemName);
        itemPrice.text = item.Price.ToString();
        this.itemName.text = item.Name;
        

        switch (item.Name)
        {
            case "Apple":
                buyButton.image.sprite = spriteApple;
                break;
            case "Pear":
                buyButton.image.sprite = spritePear;
                break;
            case "Tomato":
                buyButton.image.sprite = spriteTomato;
                break;
        }
    }
    public void ShowSellItemInfo()
    {
        string itemName = sellDropdown.options[sellDropdown.value].text;
        TValue item = storeInventory.GetItemByName(itemName);
        int newPrice = item.Price;
        newPrice = (newPrice * 90) / 100;
        itemSellPrice.text = newPrice.ToString();
        itemSellName.text = item.Name;

        switch (item.Name)
        {
            case "Apple":
                sellButton.image.sprite = spriteApple;
                break;
            case "Pear":
                sellButton.image.sprite = spritePear;
                break;
            case "Tomato":
                sellButton.image.sprite = spriteTomato;
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
        string itemName = buyDropdown.options[buyDropdown.value].text;
        TValue item = storeInventory.GetItemByName(itemName);
        Buy(item);
    }
    public void SellDropdown()
    {
        string itemName = sellDropdown.options[sellDropdown.value].text;
        TValue item = storeInventory.GetItemByName(itemName);
        Sell(item);
    }
    public void Buy(TValue item)
    {
        if (item == null) return;
        if (playerInventory.Money >= item.Price)
        {
            if (playerInventory.PlayerItems.ContainsKey(item.ID))
            {
                playerInventory.PlayerItems[item.ID]++;
            }
            else
            {
                playerInventory.PlayerItems[item.ID] = 1;
            }
            playerInventory.Money -= item.Price;

            RefreshActualMoney();

            Debug.Log("Compraste un/a: " + item.Name + " y te queda: " + playerInventory.Money + " pesos");
        }
        else
        {
            Debug.Log("No tenes suficiente plata");
        }
        ShowSellItemInfo();
    }
    public void Sell(TValue item)
    {
        if (item == null) return;
        if (playerInventory.PlayerItems.ContainsKey(item.ID))
        {
            if (playerInventory.PlayerItems[item.ID] >= 1)
            {
                playerInventory.Money += (item.Price * 90) / 100;
                playerInventory.PlayerItems[item.ID]--;

                RefreshActualMoney();

                Debug.Log("Vendiste un/a: " + item.Name + " y ahora tenes: " + playerInventory.Money + " pesos");

                if (playerInventory.PlayerItems[item.ID] == 0)
                {
                    Debug.Log("Te quedaste sin: " + item.Name);
                    playerInventory.PlayerItems.Remove(item.ID);
                }
            }
        }
        else
        {
            Debug.Log("No tenes ningun: " + item.Name);
            return;
        }
        ShowSellItemInfo();
    }
    private void RefreshActualMoney()
    {
        playerMoney.text = playerInventory.Money.ToString();
    }
}
