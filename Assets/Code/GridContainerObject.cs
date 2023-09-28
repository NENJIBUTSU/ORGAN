using System.Collections.Generic;
using System;
public class GridContainerObject
{
    private GridFloorEntity floorEntity;
    private GridStructureEntity currentGridStructureEntity;

    public GridContainerObject(GridFloorEntity floor)
    {
        this.floorEntity = floor;
        this.currentGridStructureEntity = null;
    }

    public GridContainerObject(GridFloorEntity floor, GridStructureEntity gridStructureEntity)
    {
        this.floorEntity = floor;
        this.currentGridStructureEntity = gridStructureEntity;
    }

}
