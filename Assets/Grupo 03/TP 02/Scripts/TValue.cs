using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TValue
{
    public int ID;
    public string Name;
    public int Price;
    public string Rarity;
    public string Tipe;
    public int Amount;

    public TValue(int id, string name, int price, string rarity, string tipe, int amount)
    {
        ID = id;
        Name = name;
        Price = price;
        Rarity = rarity;
        Tipe = tipe;
        Amount = amount;
    }
}

