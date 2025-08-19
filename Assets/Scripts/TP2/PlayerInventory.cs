using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private int money;
    public int Money {  get { return money; } set {  money = value; } }

    private Dictionary<int, int> playerItems = new Dictionary<int, int>(); // ID, cantidad
    public Dictionary<int, int> PlayerItems { get { return playerItems; } set { playerItems = value; } }

    public PlayerInventory() 
    { 
        money = 500;
    }
}
