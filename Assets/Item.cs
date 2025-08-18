using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID;
    public string Nombre;
    public int Precio;
    public string Rareza;
    public string Tipo;

    public Item(int id, string nombre, int precio, string rareza, string tipo)
    {
        ID = id;
        Nombre = nombre;
        Precio = precio;
        Rareza = rareza;
        Tipo = tipo;
    }
}

