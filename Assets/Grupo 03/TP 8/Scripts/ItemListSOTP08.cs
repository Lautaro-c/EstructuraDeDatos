using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemListSOTP8", menuName = "ScriptableObjects/ItemListTP8")]
public class ItemListSOTP08 : ScriptableObject
{
    public ItemSOTP8[] items;

    //OnValidate se llama al cambiar un valor en Inspector
    //En este caso, al agregar, remover o cambiar items
    private void OnValidate()
    {
        //Asignamos los IDs de todos los items
        if (items == null) return;

        for (int i = 0; i < items.Length; i++)
            if (items[i] != null)
                items[i].ID = i;
    }
}
