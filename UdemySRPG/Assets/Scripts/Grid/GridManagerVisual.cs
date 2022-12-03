using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerVisual : MonoBehaviour
{

    [SerializeField] Transform gridManagerVisualPrefab;
    private GridManagerVisualSingle[,] gridManagerVisualSingleArray;

    void Start()
    {
        gridManagerVisualSingleArray = new GridManagerVisualSingle[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()];
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform gridManagerVisualSingleTransform =
                    Instantiate(gridManagerVisualPrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                gridManagerVisualSingleArray[x, z] = gridManagerVisualSingleTransform.GetComponent<GridManagerVisualSingle>();
            }
        }
    }

    private void Update()
    {
        UpdateGridVisual();
    }
    public void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                foreach (GridManagerVisualSingle gridManagerVisualSingle in gridManagerVisualSingleArray)
                {
                    gridManagerVisualSingle.Hide();
                }
            }
        }
    }
    public void ShowValidGridPosition()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        List<GridPosition> validGridPositionList = selectedUnit.GetMoveAction().GetValidActionGridPositionList();

        foreach (GridPosition gridPosition in validGridPositionList)
        {
            gridManagerVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    public void UpdateGridVisual()
    {
        HideAllGridPosition();
        ShowValidGridPosition();
    }

}
