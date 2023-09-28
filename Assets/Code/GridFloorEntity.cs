using System;
using UnityEngine;

public class GridFloorEntity : IGridEntity
{
    public event Action OnEntityValueChanged;

    public void TriggerEntityChanged()
    {
        this.OnEntityValueChanged.Invoke();
    }

    public TileType Type { get { return type; } }
    private TileType type;


}
