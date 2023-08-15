using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;

[CreateAssetMenu(menuName = "Swag/Swagger")]
public class GridTileComp : ScriptableObject
{
    [SerializeField]
    private Array2DBool shape = null;

    [SerializeField]
    TileType compTileType;

    public void PlaceOnGrid(Grid<GridTileObject> grid, Vector2Int gridPosition) // this works off of the top left corner
    {
        var cells = shape.GetCells();

        for (int x = 0; x < shape.GridSize.x; x++)
        {
            for (int y = 0; y < shape.GridSize.y; y++)
            {
                if (cells[x, y])
                {
                   GridTileObject obj = grid.GetGridObject(x + gridPosition.x, y + gridPosition.y);
                   obj.SetType(compTileType);
                }
            }
        }
    }
}