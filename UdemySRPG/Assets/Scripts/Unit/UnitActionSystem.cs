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
    GridPosition startingGridPosition;
    private bool isBusy;
    private BaseAction selectedAction;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(null);
    }
    void Update()
    {
        if (isBusy) { return; }

        TryHandleUnitSelection();


        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        switch (selectedAction)
        {
            case MoveAction moveAction:

                if (Input.GetMouseButtonDown(1))
                {

                    GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
                    startingGridPosition = selectedUnit.GetGridPosition();

                    if (moveAction.IsValidActionGridPosition(mouseGridPosition))
                    {
                        SetBusy();
                        moveAction.MoveUnit(mouseGridPosition, ClearBusy);
                    }
                }
                if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.LeftControl))
                {
                    SetBusy();
                    if (startingGridPosition == null) { return; }
                    moveAction.CancelMovement(startingGridPosition, ClearBusy);
                }
                break;

            case SpinAction spinAction:
                if (Input.GetKeyDown(KeyCode.G))
                {
                    SetBusy();
                    spinAction.Spin(ClearBusy);
                }
                break;
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, unitLayerMask))
            {
                if (rayCastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
                SetSelectedUnit(null);
                return false;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        Debug.Log(unit);
        if (unit == null)
        {
            return;
        }
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
