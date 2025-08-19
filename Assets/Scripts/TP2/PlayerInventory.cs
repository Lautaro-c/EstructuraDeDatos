using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private int money;
    public int Money {  get { return money; } set {  money = value; } }

    private Dictionary<int, Item> playerItems = new Dictionary<int, Item>();
    public Dictionary<int, Item> PlayerItems { get { return playerItems; } set { playerItems = value; } }

    public PlayerInventory() 
    { 
        money = 500;
    }
}
