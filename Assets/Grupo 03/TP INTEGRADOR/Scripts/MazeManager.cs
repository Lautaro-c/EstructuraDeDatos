using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class MazeManager : MonoBehaviour
{
    public GameObject tilePrefab;           //Prefab para las celdas del laberinto
    private int width = 10, height = 10;    //Dimensiones del laberinto
    public Pathfinder pathfinder;           //Referencia al componente Pathfinder
    public Transform character;             //Referencia al personaje que se moverá por el laberinto
    public TextMeshProUGUI statusText;      //Texto para mostrar el estado de la solución
    public Button solveButton;              //Botón para iniciar la solución del laberinto
    private SimpleList<Tile> greenPath;
    private Vector3 originalCharaceterPos = new Vector3(-7.46f, -3.35f, 0);
    [SerializeField] private Animator anim;

    //Matriz que representa el laberinto
    private Tile[,] grid;
    private Vector2Int startPos = new Vector2Int(1000, 1000);
    private Vector2Int endPos = new Vector2Int(1000, 1000); //Posiciones de inicio y fin
    private bool mazeIsBeingSolved = false;

    //  Crea la grilla, la asigna al Pathfinder, genera los tiles y conecta el botón a SolveMaze()
    void Start()
    {
        grid = new Tile[width, height];
        pathfinder.grid = grid;
        GenerateGrid();
        solveButton.onClick.AddListener(SolveMaze);
        greenPath = new SimpleList<Tile>();
    }

    //Recorre la grilla y crea un tilePrefab en cada posición
    //Asigna posición, tipo y color inicial (blanco)
    //Guarda cada tile en la matriz grid
    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int gridIndex = new Vector2Int(x, y);
                Vector3 worldPos = GridToWorld(gridIndex);

                var tileGO = Instantiate(tilePrefab, worldPos, Quaternion.identity);
                var tile = tileGO.GetComponent<Tile>();

                tile.gridIndex = gridIndex;
                tile.type = TileType.Empty;
                tile.SetColor(Color.white);

                grid[x, y] = tile;
            }
        }
    }

    //Cambia el tipo y color de un tile según el modo activo
    //Si es entrada o salida, guarda la posición para el pathfinding
    public void SetTileType(GameObject tileGO, TileType type)
    {
        Tile tile = tileGO.GetComponent<Tile>();
        tile.type = type;
        switch (type)
        {
            case TileType.Path: 
                tile.UpdateColorByType();
                if (tile.gridIndex == endPos)
                {
                    Debug.Log("Se sobrescrivio la salida");
                    endPos = new Vector2Int(1000, 1000);
                }
                if (tile.gridIndex == startPos)
                {
                    Debug.Log("Se sobrescrivio la entrada");
                    startPos = new Vector2Int(1000, 1000);
                }
                break;
            case TileType.Wall:
                tile.UpdateColorByType();
                if (tile.gridIndex == endPos)
                {
                    Debug.Log("Se sobrescrivio la salida");
                    endPos = new Vector2Int(1000, 1000);
                }
                if (tile.gridIndex == startPos)
                {
                    Debug.Log("Se sobrescrivio la entrada");
                    startPos = new Vector2Int(1000, 1000);
                }
                break;
            case TileType.Start:
                tile.UpdateColorByType();
                startPos = tile.gridIndex; 
                break;
            case TileType.End:
                tile.UpdateColorByType();
                endPos = tile.gridIndex;
                break;
        }
    }

    //Llama a FindPath() con entrada y salida
    //Si no hay camino, muestra “Sin solución”
    //Si hay camino, colorea el recorrido y mueve el personaje

    void SolveMaze()
    {
        if(startPos == new Vector2Int(1000, 1000) || endPos == new Vector2Int(1000, 1000))
        {
            statusText.text = "Sin solución";
            mazeIsBeingSolved = false;
            return;
        }
        if (!mazeIsBeingSolved)
        {
            Debug.Log("Se esta resolviendo el puzzle");
            mazeIsBeingSolved = true;
            List<Node> path = pathfinder.FindPath(startPos, endPos);
            Debug.Log("Startpos: " + startPos);
            Debug.Log("Endpos: " + endPos);
            if (path == null)
            {
                statusText.text = "Sin solución";
                mazeIsBeingSolved = false;
            }
            else
            {
                for (int i = 0; i < path.Count; i++)
                {
                    Debug.Log("PosX " + path[i].pos.x);
                    Debug.Log("PosY " + path[i].pos.y);
                }
                statusText.text = "Solución encontrada";
                foreach (var node in path)
                {
                    Tile tile = grid[node.pos.x, node.pos.y];
                    if (tile.type == TileType.Empty || tile.type == TileType.Path)
                    {
                        tile.SetColor(Color.green);
                        tile.type = TileType.Path;
                        greenPath.Add(tile);
                    }
                }
                //Agregar acá el booleano de la maze
                anim.SetBool("isPathFinding", true);
                StartCoroutine(MoveCharacter(path));
            }
        }
    }

    Vector3 GridToWorld(Vector2Int gridIndex)
    {
        float offsetX = -4.45f;
        float offsetY = -4.45f;
        return new Vector3(gridIndex.x + offsetX, gridIndex.y + offsetY, 0);
    }

    public void ClearPath()
    {
        if (!mazeIsBeingSolved)
        {
            character.transform.position = originalCharaceterPos;
            if (greenPath.Count > 0)
            {
                for (int i = 0; i < greenPath.Count; i++)
                {
                    greenPath[i].SetColor(Color.white);
                }
                greenPath.Clear();
            }
        }
    }


    //Mueve el personaje por cada nodo del camino con una pausa de 0.2 segundos
    IEnumerator MoveCharacter(List<Node> path)
    {
        foreach (var node in path)
        {
            character.position = GridToWorld(node.pos);
            yield return new WaitForSeconds(0.2f);
                
        }
        anim.SetBool("isPathFinding", false);
        mazeIsBeingSolved = false;
        Debug.Log("Se termino de resolver el puzzle");
    }
}