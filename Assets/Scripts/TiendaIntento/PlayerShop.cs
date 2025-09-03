using UnityEngine;

public class PlayerShop : MonoBehaviour
{
    private int money = 500;
    [SerializeField] private Shop shop;
    private SimpleList<ItemSO> list;

    void Start()
    {
        list = new SimpleList<ItemSO>();
    }
    void Update()
    {

    }
    public void BuyItem(ItemSO itemSO)
    {
        if (money >= itemSO.ItemPrice)
        {
            ItemSO itemSO1 = shop.SellItem(itemSO.ID);
            if(itemSO1 != null)
            {
                Debug.Log("Compre: " +  itemSO1.name);
                list.Add(itemSO1);
                UpdateUI();
                money -= itemSO.ItemPrice;
                Debug.Log(money.ToString());
            }
        }
    }

    private void UpdateUI()
    {

    }


}
