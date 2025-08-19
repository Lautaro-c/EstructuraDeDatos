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
    public int Cantidad;

    public Item(int id, string nombre, int precio, string rareza, string tipo, int cantidad)
    {
        ID = id;
        Nombre = nombre;
        Precio = precio;
        Rareza = rareza;
        Tipo = tipo;
        Cantidad = cantidad;
    }
}

