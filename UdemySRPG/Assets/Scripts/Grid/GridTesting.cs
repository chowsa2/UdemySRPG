using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTesting : MonoBehaviour
{
    [SerializeField] Unit unit;

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (unit == null)
            {
                return;
            }
            // unit.GetMoveAction().GetValidActionGridPositionList();

        }

    }
}
