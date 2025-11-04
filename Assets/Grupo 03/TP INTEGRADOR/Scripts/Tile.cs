using UnityEngine;

public enum TileType { Empty, Wall, Start, End, Path }

public class Tile : MonoBehaviour
{
    public Vector2 gridPos;
    public Vector2Int gridIndex;
    public TileType type;
    public int cost = 1;

    public void UpdateColorByTipe()
    {
        switch (type)
        {
            case TileType.Wall:
                SetColor(Color.black);
                break;
            case TileType.Start:
                SetColor(Color.blue);
                break;
            case TileType.End:
                SetColor(Color.red);
                break;
            case TileType.Path:
                SetColor(Color.white);
                break;
        }
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}