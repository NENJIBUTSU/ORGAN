using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NenjiUtils;

public class TestGrid : MonoBehaviour
{
    private Grid<GridTileObject> testGrid;
    private int tileTypeCounter = 1;

    void Start()
    {
        testGrid = new Grid<GridTileObject>(5, 5, 2.5f, new Vector3(-7.5f, -7.5f, 0), false, true, () => new GridTileObject());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = MouseUtils.GetMouseWorldPosition2D();

            GridTileObject obj = testGrid.GetGridObject(position);

            if (obj != null)
            {
                obj.SetType((TileType)tileTypeCounter);
                if (tileTypeCounter < 5)
                {
                    tileTypeCounter++;
                }
                else
                {
                    tileTypeCounter = 1;
                }
            }

        }
    }
}
