using UnityEngine;

public enum TileType { Empty, Wall, Start, End, Path }

public class Tile : MonoBehaviour
{
    public Vector2Int gridPos;
    public TileType type;
    public int cost = 1;

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}