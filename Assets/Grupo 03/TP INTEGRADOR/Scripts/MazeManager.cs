using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeManager : MonoBehaviour
{
    public GameObject tilePrefab; //Prefab para las celdas del laberinto
    public int width = 12, height = 12; //Dimensiones del laberinto
    public Pathfinder pathfinder; //Referencia al componente Pathfinder
    public Transform character; //Referencia al personaje que se mover� por el laberinto
    public Text statusText; //Texto para mostrar el estado de la soluci�n
    public Button solveButton; //Bot�n para iniciar la soluci�n del laberinto

    //Matriz que representa el laberinto
    private Tile[,] grid;
    private Vector2Int startPos, endPos; //Posiciones de inicio y fin

    //  Crea la grilla, la asigna al Pathfinder, genera los tiles y conecta el bot�n a SolveMaze()
    void Start()
    {
        grid = new Tile[width, height];
        pathfinder.grid = grid;
        GenerateGrid();
        solveButton.onClick.AddListener(SolveMaze);
    }

    //Recorre la grilla y crea un tilePrefab en cada posici�n
    //Asigna posici�n, tipo y color inicial (blanco)
    //Guarda cada tile en la matriz grid
    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tileGO = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                var tile = tileGO.GetComponent<Tile>();
                tile.gridPos = new Vector2Int(x, y);
                tile.type = TileType.Empty;
                tile.SetColor(Color.white);
                grid[x, y] = tile;
            }
        }
    }

    //Cambia el tipo y color de un tile seg�n el modo activo
    //Si es entrada o salida, guarda la posici�n para el pathfinding
    public void SetTileType(Vector2Int pos, TileType type)
    {
        var tile = grid[pos.x, pos.y];
        tile.type = type;
        switch (type)
        {
            case TileType.Empty: tile.SetColor(Color.white); break;
            case TileType.Wall: tile.SetColor(Color.black); break;
            case TileType.Start: tile.SetColor(Color.blue); startPos = pos; break;
            case TileType.End: tile.SetColor(Color.red); endPos = pos; break;
        }
    }

    //Llama a FindPath() con entrada y salida
    //Si no hay camino, muestra �Sin soluci�n�
    //Si hay camino, colorea el recorrido y mueve el personaje

    void SolveMaze()
    {
        var path = pathfinder.FindPath(startPos, endPos);
        if (path == null)
        {
            statusText.text = "Sin soluci�n";
        }
        else
        {
            statusText.text = "Soluci�n encontrada";
            foreach (var node in path)
            {
                var tile = grid[node.pos.x, node.pos.y];
                if (tile.type == TileType.Empty)
                {
                    tile.SetColor(Color.green);
                    tile.type = TileType.Path;
                }
            }
            StartCoroutine(MoveCharacter(path));
        }
    }

    //Mueve el personaje por cada nodo del camino con una pausa de 0.2 segundos
    IEnumerator MoveCharacter(List<Node> path)
    {
        foreach (var node in path)
        {
            character.position = new Vector3(node.pos.x, node.pos.y, 0);
            yield return new WaitForSeconds(0.2f);
        }
    }
}