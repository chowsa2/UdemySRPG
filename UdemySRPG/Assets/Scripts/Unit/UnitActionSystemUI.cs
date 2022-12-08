using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{

    [SerializeField] Transform actionButtonContainerTransform;
    [SerializeField] Transform actionButtonPrefab;



    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons()
    {

        /* 
        This is unity being unclear. with a foreach, You'd expect that with Transform, 
        Unity would offer a call such as, say, "allChildren". So you'd do something like: foreach (Transform child in transform.allChildren() { . . . 
        However, Unity does not do that. For better or worse, they make "transform" magically supply "all of its children" when it is in a situation such as a foreach.
        
        To specify, transform inplements IEnumerator, which foreach utilizes. To read up on. 
        */
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        if (selectedUnit == null)
        {
            return;
        }
        foreach (BaseAction baseAction in selectedUnit.GetBaseActions())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);   // TODO to revisit for studying: actionbuttonUI as a script (search external variable with serialized field)
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>(); //TODO try to have the reference to the actionButtonUI text only in this script. (for exercise only to understand options)
            actionButtonUI.SetBaseAction(baseAction);
        }

    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
    }
}
