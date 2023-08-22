using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NenjiUtils;

public class TestGrid : MonoBehaviour
{
    private Grid<GridTileObject> testGrid;

    [SerializeField]
    GridTileComp currentTileComp;

    void Start()
    {
        testGrid = new Grid<GridTileObject>(5, 5, 10f, new Vector3(-25f, -25f, 0), true, true, () => new GridTileObject());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = MouseUtils.GetMouseWorldPosition2D();

            testGrid.GetGridPosition(position, out int gridX, out int gridY);

            Debug.Log(gridX + "," + gridY);

            currentTileComp.PlaceOnGrid(testGrid, new Vector2Int(gridX - Mathf.FloorToInt(currentTileComp.shape.GridSize.x / 2), gridY + Mathf.FloorToInt(currentTileComp.shape.GridSize.y / 2)));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentTileComp.RotateComp();
        }
    }
}
