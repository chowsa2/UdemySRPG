using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnitVisual : MonoBehaviour
{

    [SerializeField] private Unit unit;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
    void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChange;
    }

    private void UnitActionSystem_OnSelectedUnitChange(object sender, EventArgs empty)
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == null)
        {
            meshRenderer.enabled = false;
            return;
        }
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }

    }

}
