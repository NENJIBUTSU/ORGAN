using System;

public class GridTileObject : IGridObject
{
    public event Action OnGridValueChanged;

    public TileType Type { get { return type; } }
    private TileType type;

    public Direction Direction { get { return direction; } set { direction = value; } }
    private Direction direction;

    public GridTileObject()
    {
        this.type = TileType.Invalid;
        this.Direction = 0;
    }

    public GridTileObject(Direction dir)
    {
        this.type = TileType.Invalid;
        this.Direction = dir;
    }

    public GridTileObject(TileType type)
    {
        this.type = type;
        this.Direction = 0;
    }

    public GridTileObject(TileType type, Direction dir)
    {
        this.type = type;
        this.Direction = dir;
    }


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

    public void ChangeDirectionCCW()
    {
        if ((int)this.Direction <= 4)
        {
            this.Direction = Direction + 2;
        }
        else
        {
            this.Direction = 0;
        }
    }
}