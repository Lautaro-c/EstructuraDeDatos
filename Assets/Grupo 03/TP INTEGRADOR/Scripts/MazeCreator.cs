using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreator : MonoBehaviour
{
    private TileType newTileType;
    private GameObject clickedObject;
    [SerializeField] private MazeManager mazeManager;
    private GameObject lastEndTile;
    private GameObject lastStartTile;
    [SerializeField] private Pointer pointer;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mazeManager.ClearPath();
        }
        if (Input.GetMouseButton(0)) // 0 = clic izquierdo
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                clickedObject = hit.collider.gameObject;
                string Objecttag = hit.collider.tag;

                switch (Objecttag)
                {
                    case "EndTile":
                        newTileType = TileType.End;
                        pointer.ChangeBrushColor("Red");
                        break;

                    case "StartTile":
                        newTileType = TileType.Start;
                        pointer.ChangeBrushColor("Blue");
                        break;

                    case "WallTile":
                        newTileType = TileType.Wall;
                        pointer.ChangeBrushColor("Black");
                        break;

                    case "EmptyTile":
                        newTileType = TileType.Path;
                        pointer.ChangeBrushColor("White");
                        break;
                    case "EditableTile":
                        UpdateTile(clickedObject);
                        break;
                }
            }
        }
    }

    private void UpdateTile(GameObject tile)
    {
        if (tile.GetComponent<Tile>())
        {
            mazeManager.SetTileType(tile, newTileType);
            if (newTileType  == TileType.End)
            {
                if(lastEndTile != null && lastEndTile != tile)
                {
                    mazeManager.SetTileType(lastEndTile, TileType.Path);
                }
                lastEndTile = tile;
            }
            if (newTileType == TileType.Start)
            {
                if (lastStartTile != null && lastStartTile != tile)
                {
                    mazeManager.SetTileType(lastStartTile, TileType.Path);
                }
                lastStartTile = tile;
            }
        }
    }

}
