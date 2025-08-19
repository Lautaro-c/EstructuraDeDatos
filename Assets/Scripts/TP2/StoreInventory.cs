using System.Collections;
using System.Collections.Generic;

public class StoreInventory
{
    private Dictionary<int, Item> items = new Dictionary<int, Item>();

    public StoreInventory() 
    {
        items.Add(1, new Item(1, "Manzana", 50, "Comun", "Fruta", 0));
        items.Add(2, new Item(2, "Tomate", 40, "Raro", "Fruta", 0));
        items.Add(3, new Item(3, "Pera", 80, "Comun", "Fruta", 0));
        items.Add(4, new Item(4, "Chaucha", 20, "Raro", "Verdura", 0));
    }
    public Item GetItemByName(string nombre)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Nombre == nombre) return items[i];
        }
        return null;
    }

}



 //private Item[] items;
    //private int[] keys;
    //private int[] amount;
    //private int count;

    //public StoreInventory(int size, int id, string nombre, int precio, string rareza, string tipo)
    //{
    //    items = new Item[size];
    //    keys = new int[id];
    //    amount = new int[size];
    //    count = 0;
    //}