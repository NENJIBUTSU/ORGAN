using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NenjiUtils;

public class TestGrid : MonoBehaviour
{

    private Grid<GridTileObject> testGrid;


    public GridTileShapeDefinition CurrentShapeDefinition 
    {
        get
        {
            return _currentShapeDefinition;
        }
        set
        {
            _currentShapeDefinition = value;
            currentShape = value.Create();
        }
    }

    private GridTileShapeDefinition _currentShapeDefinition;

    private GridTileShape currentShape;

    private void Start()
    {
        testGrid = new Grid<GridTileObject>(5, 5, 10f, new Vector3(-25f, -25f, 0), true, true, () => new GridTileObject());
        currentShape = _currentShapeDefinition.Create();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = MouseUtils.GetMouseWorldPosition2D();

            testGrid.GetGridPosition(position, out int gridX, out int gridY);

            Debug.Log(gridX + "," + gridY);

            currentShape.PlaceOnGrid(testGrid, new Vector2Int(gridX - Mathf.FloorToInt(currentShape.GridSize.x / 2), gridY + Mathf.FloorToInt(currentShape.GridSize.y / 2)));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentShape.RotateComp();
        }
    }
}
