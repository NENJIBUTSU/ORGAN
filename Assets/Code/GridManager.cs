using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid<GridTileObject> gameGrid;

    private List<Vector2Int> creepPositions;
    //tile placer??

    float timer;
    bool loaded;

    private void Start()
    {
        timer = 0;
        gameGrid = new Grid<GridTileObject>(5, 5, 10f, new Vector3(-25f, -25f, 0), true, true, () => new GridTileObject(TileType.Ground));
        creepPositions = new List<Vector2Int>();

        gameGrid.GetGridObject(1, 1).SetTileType(TileType.Creep);
        gameGrid.GetGridObject(1, 1).Direction = Direction.North;
        gameGrid.GetGridObject(2, 2).SetTileType(TileType.Wall);
        gameGrid.GetGridObject(2, 3).SetTileType(TileType.Wall);

        GetCreepPositions();
    }

    private void Update()
    {
            if (timer < 1f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                SpreadCreep();
                timer = 0;
            }
    }

    void GetCreepPositions()
    {
        for (int x = 0; x < gameGrid.Width; x++)
        {
            for (int y = 0; y < gameGrid.Height; y++)
            {
                GridTileObject tile = gameGrid.GetGridObject(x, y);
                if (tile.Type == TileType.Creep)
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    creepPositions.Add(pos);
                    Debug.Log("Added creep position at " + pos);
                }
            }
        }
    }

    void SpreadCreep() //this is ugly and makes me sad :( but the momentum must continue
    {
        for (int i = 0; i < creepPositions.Count; i++)
        {
            Vector2Int pos = new Vector2Int(creepPositions[i].x, creepPositions[i].y);
            GridTileObject tile = gameGrid.GetGridObject(pos.x, pos.y);

            if (CheckNextTileInDirection(pos.x, pos.y, TileType.Ground))
            {
                GridTileObject obj = GetNextTileInDirection(pos.x, pos.y);
                obj.SetTileType(TileType.Creep);
                obj.Direction = tile.Direction;
            }
            else if (CheckNextTileInDirection(pos.x, pos.y, TileType.Objective))
            {
                GridTileObject obj = GetNextTileInDirection(pos.x, pos.y);
                //infect organ code??? change organ to new tile
            }
            else
            {
                for (int z = 0; z < (int)Direction.South + 1; z += 2)
                {
                    if (CheckNextTileInDirection(pos.x, pos.y, TileType.Ground, (Direction)z))
                    {
                        GridTileObject obj = GetNextTileInDirection(pos.x, pos.y, (Direction)z);
                        obj.SetTileType(TileType.Creep);
                        obj.Direction = (Direction)z;
                        break;
                    }
                    else if (CheckNextTileInDirection(pos.x, pos.y, TileType.Objective, (Direction)z))
                    {
                        //organ code again
                    }
                }
            }
        }
        creepPositions.Clear();
        GetCreepPositions();
    }

    bool CheckNextTileInDirection(int x, int y, TileType tileType, Direction dir)
    {
        if (GetNextTileInDirection(x, y, dir)?.Type == tileType)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CheckNextTileInDirection(int x, int y, TileType tileType)
    {
        if (GetNextTileInDirection(x,y)?.Type == tileType)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    GridTileObject GetNextTileInDirection(int x, int y, Direction dir)
    {
        if (dir == Direction.North)
        {
            GridTileObject nextTile = gameGrid.GetGridObject(x, y - 1);
            return nextTile;
        }
        else if (dir == Direction.East)
        {
            GridTileObject nextTile = gameGrid.GetGridObject(x + 1, y);
            return nextTile;
        }
        else if (dir == Direction.South)
        {
            GridTileObject nextTile = gameGrid.GetGridObject(x, y + 1);
            return nextTile;
        }
        else if (dir == Direction.West)
        {
            GridTileObject nextTile = gameGrid.GetGridObject(x - 1, y);
            return nextTile;
        }

        return null;
    }

    GridTileObject GetNextTileInDirection(int x,int y)
    {
        GridTileObject tile = gameGrid.GetGridObject(x, y);

        if (tile.Direction == Direction.North)
        {
            GridTileObject nextTile = gameGrid.GetGridObject(x, y - 1);
            return nextTile;
        }
        else if (tile.Direction == Direction.East)
        {
            GridTileObject nextTile = gameGrid.GetGridObject(x + 1, y);
            return nextTile;
        }
        else if (tile.Direction == Direction.South)
        {
            GridTileObject nextTile = gameGrid.GetGridObject(x, y + 1);
            return nextTile;
        }
        else if (tile.Direction == Direction.West)
        {
            GridTileObject nextTile = gameGrid.GetGridObject(x - 1, y);
            return nextTile;
        }

        return null;
    }

   
}
