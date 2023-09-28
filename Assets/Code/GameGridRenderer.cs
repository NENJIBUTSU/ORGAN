using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NenjiUtils.MeshUtils;

public class GameGridRenderer : MonoBehaviour
{
    private Grid<GridTileObject> grid;
    private Mesh mesh;

    public GameGridRenderer()
    {
        grid.OnGridObjectChanged += (object sender, Grid<GridTileObject>.OnGridObjectChangedEventArgs eventArgs) =>
        {
            UpdateRenderedGridTiles();
        };
    }

    void UpdateRenderedGridTiles()
    {
        int tileCount = 0;
        List<Vector2Int> tilePositions = new List<Vector2Int>();

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                GridTileObject obj = grid.GetGridObject(x, y);
                TileType tileType = obj.GetTileType();

                if (tileType != TileType.Invalid)
                {
                    Vector2Int tilePosition = new Vector2Int(x, y);
                    tilePositions.Add(tilePosition);

                    tileCount++;
                }
            }
        }

        CreateEmptyMeshArrays(tileCount, out Vector3[] verts, out Vector2[] uvs, out int[] tris);

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                int index = x * grid.Height + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.TileSize;

                GridTileObject obj = grid.GetGridObject(x, y);
                TileType tileType = obj.GetTileType();

                Vector2 gridObjectUV;

                gridObjectUV = Vector2.one;

                AddToMeshArrays(verts, uvs, tris, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, Vector2.zero, Vector2.zero);
            }
        }

        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.triangles = tris;
    }
}
