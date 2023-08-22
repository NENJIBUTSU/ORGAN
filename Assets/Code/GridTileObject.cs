using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridTileObject : IGridObject
{
    public event Action OnGridValueChanged;
    
    private TileType type;

    public void SetTileType(TileType type)
    {
        this.type = type;
        this.OnGridValueChanged?.Invoke();
    }

    public TileType GetTileType()
    {
        return type;
    }

    public override string ToString()
    {
        return type.ToString();
    }
}


public enum TileType
{
    Empty,
    Flesh,
    Bone,
    Organ,
    Infected,
    Cut
}

public interface IGridObject
{
    public event Action OnGridValueChanged;
}