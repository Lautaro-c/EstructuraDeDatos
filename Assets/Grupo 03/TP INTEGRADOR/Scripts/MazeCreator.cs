using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreator : MonoBehaviour
{
    private TileType newTileType;
    private GameObject clickedObject;
    [SerializeField] private MazeManager mazeManager;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 = clic izquierdo
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
                        Debug.Log("EndTile");
                        break;

                    case "StartTile":
                        newTileType = TileType.Start;
                        Debug.Log("StartTile");
                        break;

                    case "WallTile":
                        newTileType = TileType.Wall;
                        Debug.Log("WallTile");
                        break;

                    case "EmptyTile":
                        newTileType = TileType.Path;
                        Debug.Log("PathTile");
                        break;
                    case "EditableTile":
                        Debug.Log("EditableTile");
                        UpdateTile(clickedObject);
                        break;
                }
            }
        }
    }

    private void UpdateTile(GameObject tile)
    {
        Debug.Log("I was called");
        if (tile.GetComponent<Tile>())
        {
            Debug.Log("The if was passed");
            mazeManager.SetTileType(tile, newTileType);
        }
    }

}
