using System.Collections;
using System.Collections.Generic;

public class StoreInventory
{
    private Dictionary<int, TValue> items = new Dictionary<int, TValue>();

    public StoreInventory() 
    {
        items.Add(1, new TValue(1, "Apple", 50, "Common", "Fruit", 0));
        items.Add(2, new TValue(2, "Tomato", 40, "Weird", "Fruit", 0));
        items.Add(3, new TValue(3, "Pear", 80, "Common", "Fruit", 0));
        items.Add(4, new TValue(4, "Chaucha", 20, "Weird", "Vegetable", 0));
    }
    public TValue GetItemByName(string name)
    {
        foreach (var item in items.Values) // key, values
        {
            if (item.Name == name)
            {
                return item;
            } 
        }
        return null;
    }

}

