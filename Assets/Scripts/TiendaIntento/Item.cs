using UnityEngine;

//Hacemos visible la clase en Inspector
//Si estamos usando System, podemos obviarlo y poner [Serializable]
[System.Serializable]
public class Item
{
    //Ocultamos el ID porque se va a setear automaticamente
    [HideInInspector] public int id;

    //Las otras variables si son publicas
    public string name;
    public int rareza;
    public string tipo;
    public int price;
    public Sprite sprite;
}