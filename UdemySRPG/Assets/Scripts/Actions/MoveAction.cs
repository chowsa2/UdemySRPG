using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{

    [SerializeField] private int maxMoveRange = 3;
    [SerializeField] private Animator unitAnimator;
    private Action onActionComplete;
    private Vector3 targetPosition;

    private bool isCanceled;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }


    public void Update()
    {
        if (isCanceled)
        {
            transform.position = targetPosition;
            isCanceled = false;
            isActive = false;
            onActionComplete();
            return;
        }
        if (!isActive) { return; }

        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float stopDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            int moveSpeed = 4;

            transform.position += moveDirection * Time.deltaTime * moveSpeed;


            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
            isActive = false;
            onActionComplete();
        }

        int rotationSpeed = 10;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }
    public void MoveUnit(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
    }

    public void CancelMovement(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        Debug.Log("Cancel Command");
        isCanceled = true;


    }
    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validActionGridPosition = GetValidActionGridPositionList();
        return validActionGridPosition.Contains(gridPosition);
    }
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveRange; x <= maxMoveRange; x++)
        {
            for (int z = -maxMoveRange; z <= maxMoveRange; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }
                if (testGridPosition == unitGridPosition)
                {
                    continue;
                }
                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;

    }
    public override string GetActionName()
    {
        return "Move";
    }

}
