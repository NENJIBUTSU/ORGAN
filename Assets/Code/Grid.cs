using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NenjiUtils;

public class Grid<TGridObject> where TGridObject : IGridObject
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }


    float tileSize;
    int width;
    int height;

    bool showDebugLines;
    bool showDebugText;

    TGridObject[,] gridArray;
    TextMesh[,] debugTextArray;

    private Vector3 originPosition;

    public Grid(int width, int height, float tileSize, Vector3 originPosition, bool showDebugLines, bool showDebugText, Func<TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.tileSize = tileSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                int x1 = x, y1 = y;
                gridArray[x, y] = createGridObject();
                Debug.Log("Object at " + x + "," + y + " created!");
                gridArray[x, y].OnGridValueChanged += () => { Debug.Log("[" + x1 + ", " + y1 + "] was changed!"); };

                gridArray[x, y].OnGridValueChanged += () => { OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x1, y = y1 }); }; //this is likely to cause a memory leak. FIX!!!!
            }
        }

        this.showDebugLines = showDebugLines;
        this.showDebugText = showDebugText;

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (showDebugLines)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
                if (showDebugText)
                {
                    debugTextArray[x,y] = TextUtils.CreateDebugWorldText(gridArray[x, y]?.ToString(), null,
                                                                         GetWorldPosition(x,y) + new Vector3(tileSize, tileSize) * .5f, 
                                                                         30, Color.white, TextAnchor.MiddleCenter);
                }
            }
        }

        if (showDebugLines)
        {
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }


        if (showDebugText)
        {
            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }

    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y]?.ToString();
        }
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetGridPosition(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null)
        {
            OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * tileSize + originPosition;
    }

    public void GetGridPosition(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / tileSize);
        y = Mathf.FloorToInt((worldPosition -originPosition).y / tileSize);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else return default(TGridObject);

    }

    public TGridObject GetGridObject (Vector3 worldPosition)
    {
        int x, y;
        GetGridPosition(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}
