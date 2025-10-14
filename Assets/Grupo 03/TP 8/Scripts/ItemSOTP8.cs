using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSOTP8", menuName = "ScriptableObjects/ItemSOTP8")]
public class ItemSOTP8 : ScriptableObject
{
    [field: SerializeField] private Item Item { get; set; }
    public string Name { get => Item.name; set { Item.name = value; } }
    public int Price { get => Item.price; set { Item.price = value; } }
}
