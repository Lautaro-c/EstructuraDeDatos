using UnityEngine;

public class Node
{
    public Vector2Int pos;
    public Node parent; //Nodo anterior en el camino. Se usa para reconstruir el recorrido una vez que se llega al destino
    public int gCost; //Costo desde el inicio hasta este nodo(camino recorrido)
    public int hCost; //Heurística estimada desde este nodo hasta el destino (camino por recorrer)
    public int FCost => gCost + hCost; //Costo total estimado. A* elige el nodo con menor FCost
}