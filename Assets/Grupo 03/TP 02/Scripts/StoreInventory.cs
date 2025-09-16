using System.Collections;
using System.Collections.Generic;

public class StoreInventory
{
    private Dictionary<int, TValue> items = new Dictionary<int, TValue>();

    public StoreInventory() 
    {
        items.Add(1, new TValue(1, "Manzana", 50, "Comun", "Fruta", 0));
        items.Add(2, new TValue(2, "Tomate", 40, "Raro", "Fruta", 0));
        items.Add(3, new TValue(3, "Pera", 80, "Comun", "Fruta", 0));
        items.Add(4, new TValue(4, "Chaucha", 20, "Raro", "Verdura", 0));
    }
    public TValue GetItemByName(string nombre)
    {
        foreach (var item in items.Values) // key, values
        {
            if (item.Nombre == nombre)
            {
                return item;
            } 
        }
        return null;
    }

}

