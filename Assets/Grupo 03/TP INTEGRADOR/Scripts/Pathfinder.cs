using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Tile[,] grid; //grid: matriz bidimensional que representa el laberinto.Cada celda es un Tile

    //Define los cuatro movimientos posibles: arriba, abajo, izquierda, derecha.
    private static readonly Vector2Int[] directions = 
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

    //Método público que busca el camino más corto entre start y end, Devuelve una lista de Node que representa el recorrido

    public List<Node> FindPath(Vector2Int start, Vector2Int end)
    {
        // open: nodos por explorar.
        var open = new List<Node>();
        // closed: nodos ya explorados.
        var closed = new HashSet<Vector2Int>();
        // Añade el nodo inicial a la lista open con costos gCost y hCost iniciales.
        open.Add(new Node { pos = start, gCost = 0, hCost = Heuristic(start, end) });

        while (open.Count > 0)
        {
            // Selecciona el nodo con el menor FCost de la lista open.
            var current = open.OrderBy(n => n.FCost).First();
            // Si se ha llegado al nodo final, reconstruye y devuelve el camino.
            if (current.pos == end)
            {
                return ReconstructPath(current);
            }
            // Mueve el nodo actual de open a closed.
            open.Remove(current);
            closed.Add(current.pos);
            // Explora los vecinos del nodo actual.
            foreach (var dir in directions)
            {
                var neighborPos = current.pos + dir;
                // Si el vecino no es válido o ya está en closed, lo ignora.
                if (!IsValid(neighborPos) || closed.Contains(neighborPos))
                {
                    continue;
                }
                // Crea un nuevo nodo vecino con costos actualizados. Posición, padre, gCost y hCost.
                var neighbor = new Node
                {
                    pos = neighborPos,
                    parent = current,
                    gCost = current.gCost + grid[neighborPos.x, neighborPos.y].cost,
                    hCost = Heuristic(neighborPos, end)
                };
                // Si el vecino ya está en open con un FCost menor o igual, lo ignora. Si no, lo añade a open.
                if (open.Any(n => n.pos == neighbor.pos && n.FCost <= neighbor.FCost))
                {
                    continue;
                }
                open.Add(neighbor);
            }
        }
        return null;
    }

    // Heurística Manhattan para estimar el costo desde el nodo a hasta el nodo b.
    //Se usa para guiar la búsqueda en A*.
    private int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    // Reconstruye el camino desde el nodo final hasta el inicial siguiendo los nodos padre.
    // Devuelve la lista de nodos en orden desde el inicio hasta el final.
    private List<Node> ReconstructPath(Node endNode)
    {
        var path = new List<Node>();
        while (endNode != null)
        {
            path.Add(endNode);
            endNode = endNode.parent;
        }
        path.Reverse();
        return path;
    }

    // Verifica si una posición es válida dentro del grid y no es una pared.
    private bool IsValid(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= grid.GetLength(0) || pos.y >= grid.GetLength(1))
        {
            return false;
        }
        return grid[pos.x, pos.y].type != TileType.Wall;
    }
}