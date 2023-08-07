using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Swagger"), System.Serializable]
public class GridTileComp : ScriptableObject
{
    int width, height;
    GridTileMDArray tileGrid;
}

public class GridTileMDArray : MonoBehaviour, ISerializationCallbackReceiver
{
    public GridTileObject[,] unserializable = new GridTileObject[5, 5];
    [SerializeField, HideInInspector] private List<Package<GridTileObject>> serializable;

    [System.Serializable]
    struct Package<TElement>
    {
        public int Index0;
        public int Index1;
        public TElement Element;
        public Package(int idx0, int idx1, TElement element)
        {
            Index0 = idx0;
            Index1 = idx1;
            Element = element;
        }
    }

    public void OnBeforeSerialize()
    {
        serializable = new List<Package<GridTileObject>>();
        for (int i = 0; i < unserializable.GetLength(0); i++)
        {
            for (int j = 0; j < unserializable.GetLength(1); j++)
            {
                serializable.Add(new Package<GridTileObject>(i, j, unserializable[i, j]));
            }
        }
    }

    public void OnAfterDeserialize()
    {
        unserializable = new GridTileObject[5, 5];
        foreach (var package in serializable)
        {
            unserializable[package.Index0, package.Index1] = package.Element;
        }
    }
}