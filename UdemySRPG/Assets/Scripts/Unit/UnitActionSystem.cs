using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    // This is a singleton, the forcing of a single instance of this class, 
    //and the creation of a reference to the property. 
    public static UnitActionSystem Instance { get; private set; }
    //-------
    public event EventHandler OnSelectedUnitChanged;


    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private bool isBusy;
    private BaseAction selectedAction;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }
    void Update()
    {
        if (isBusy) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            TryHandleUnitSelection();
        }

        if (Input.GetMouseButtonDown(1))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (selectedUnit == null) { return; }

            if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectedUnit.GetMoveAction().MoveUnit(mouseGridPosition, ClearBusy);
            }

        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);
        }
    }

    private void HandleSelectedAction()
    {
        if ()
        {
            
        }
    }
    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }
    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, unitLayerMask))
        {
            if (rayCastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        SetSelectedUnit(null);
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        SetSelectedAction(unit.GetMoveAction());
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
