using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridEntity
{
    public event Action OnEntityValueChanged;

    public void TriggerEntityChanged();

}

public interface ICorruptible
{
    void OnCorrupt();
}
public interface IDestroyable
{
    void OnDestroy();
}

public abstract class GridStructureEntity : IGridEntity
{
    public event Action OnEntityValueChanged;

    public void TriggerEntityChanged()
    {
        this.OnEntityValueChanged.Invoke();
    }

    public int[,] Size { get { return size; } set { size = value; } }
    int[,] size;
}