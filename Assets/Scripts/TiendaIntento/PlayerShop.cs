using UnityEngine;

public class PlayerShop : MonoBehaviour
{
    private int money = 500;

    void Start()
    {
        
    }
    void Update()
    {

    }
    void BuyItem(ItemSO itemSO)
    {
        if(money >= itemSO.ItemPrice)
        {
            
            money -= itemSO.ItemPrice;

        }
    }


}
