using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{

    float totalSpinAmount;
    private Action onSpinComplete;
    private void Update()
    {
        if (!isActive) { return; }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        totalSpinAmount += spinAddAmount;

        if (totalSpinAmount >= 360f)
        {
            isActive = false;
            onSpinComplete();
        }

    }
    public void Spin(Action onSpinComplete)
    {
        this.onSpinComplete = onSpinComplete;
        isActive = true;
        totalSpinAmount = 0;
    }
    public override string GetActionName()
    {
        return "Spin";
    }
}
