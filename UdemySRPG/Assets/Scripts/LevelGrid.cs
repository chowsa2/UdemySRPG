using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }
    [SerializeField] private Transform debugGridObject;
    GridManager gridManager;

    void Awake()
    {
        Instance = this;
        gridManager = new GridManager(10, 10, 2f);
        gridManager.CreateDebugObjects(debugGridObject);

    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridManager.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridManager.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridManager.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }



    public void UnitChangedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridManager.GetGridPosition(worldPosition);
    public bool IsValidGridPosition(GridPosition gridPosition) => gridManager.IsValidGridPosition(gridPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridManager.GetWorldPosition(gridPosition);
    public int GetWidth() => gridManager.GetWidth();
    public int GetHeight() => gridManager.GetHeight();
    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridobject = gridManager.GetGridObject(gridPosition);
        return gridobject.HasAnyUnit();

    }
}
