using System;
using UnityEngine;

public class Spot : Interactable
{
    public ValuedCarryable onTop;
    [SerializeField] TMPro.TMP_Text valueText;

    public override void Drop(ValuedCarryable carriable)
    {
        if (canFill)
        {
            this.onTop = carriable;
            carriable.canBeUsed = false;
            carriable.transform.position = transform.position;
            carriable.transform.SetParent(transform);
            carriable.HideText();
            canFill = false;
            canTake = true;
            valueText.text = carriable.GetValue().ToString();
        }
        base.Drop(carriable);    
    }

    public override ValuedCarryable Take()
    {
        canTake = false;
        canFill = true;
        valueText.text = "";
        base.Take();
        return onTop;
    }

}
