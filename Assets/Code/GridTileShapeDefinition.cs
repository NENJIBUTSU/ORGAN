using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileShapeDefinition : ScriptableObject
{
    public GridTileShape Create()
    {
        return new GridTileShape { data = this };
    }
}
