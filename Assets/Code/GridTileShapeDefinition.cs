using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;

[System.Serializable]
public class GridTileShapeDefinition : ScriptableObject
{
    [SerializeField]
    private Array2DBool shape = null; //make sure this is square

    [SerializeField]
    private TileType compTileType;
    public GridTileShape Create()
    {
        return new GridTileShape(shape, compTileType);
    }
}
