using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;

public class GridTileShape : MonoBehaviour
{
    private Array2DBool shape = null; //make sure this is square
    private TileType compTileType;

    public Vector2Int GridSize { get { return shape.GridSize; } }

    public GridTileShape(Array2DBool shape, TileType tileType)
    {
        this.shape = shape;
        this.compTileType = tileType;
    }


    public bool PlaceOnGrid(Grid<GridTileObject> grid, Vector2Int gridPosition) // this works off of the bottom left corner
    {
        var cells = shape.GetCells();



        for (int y = 0; y < shape.GridSize.y; y++)
        {
            for (int x = 0; x < shape.GridSize.x; x++)
            {
                if (cells[y, x])
                {
                    if (grid.GetGridObject(gridPosition.x + x, gridPosition.y - y) == null)
                    {
                        Debug.Log("Attempted to place out of bounds!");
                        return false;
                    }

                    if (grid.GetGridObject(gridPosition.x + x, gridPosition.y - y).GetTileType() == compTileType)
                    {
                        Debug.Log("Overlapping tile types!");
                        return false;
                    }
                }
            }
        }

        for (int y = 0; y < shape.GridSize.y; y++)
        {
            for (int x = 0; x < shape.GridSize.x; x++)
            {
                if (cells[y, x])
                {
                   GridTileObject obj = grid.GetGridObject(gridPosition.x + x, gridPosition.y - y);

                   obj.SetTileType(compTileType);
                }
            }
        }
        return true;
    }

    public void RotateComp() //rotate comp 90deg clockwise
    {
        var cells = shape.GetCells();

        int rows = cells.GetLength(0);
        int cols = cells.GetLength(1);

        bool[,] transposed = new bool[cols, rows];

        for(int i=0; i<rows; i++) //transpose matrix
        {
            for (int j = 0; j < cols; j++)
            {
                transposed[j, i] = cells[i, j];
            }
        }

        bool[,] reversed = new bool[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                reversed[rows - i - 1, j] = transposed[i, j];
            }
        }

        for (int y = 0; y < shape.GridSize.y; y++)
        {
            for (int x = 0; x < shape.GridSize.x; x++)
            {
                shape.SetCell(x, y, reversed[x, y]);
            }
        }
    }
}