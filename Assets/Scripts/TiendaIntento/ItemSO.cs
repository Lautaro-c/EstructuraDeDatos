using UnityEngine;

//Esta clase va a ser un Wrapper, para separar el Asset de la class Item
//Podriamos hacer que el Item sea el ScriptableObject, pero asi es mas limpio
[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO")]
public class ItemSO : ScriptableObject
{
    //Aca va el Item
    [field: SerializeField] private Item Item { get; set; }

    //Todas las propiedades llevan a la variable correspondiente del Item
    public int ID { get => Item.id; set { Item.id = value; } }
    public string ItemName { get => Item.name; set { Item.name = value; } }

    public int ItemPrice { get => Item.price; set { Item.price = value; } }
    public int Rareza { get => Item.rareza; set { Item.rareza = value; } }
    public string Tipo { get => Item.tipo; set { Item.tipo = value; } }
    public Sprite Sprite { get => Item.sprite; set { Item.sprite = value; } }
}