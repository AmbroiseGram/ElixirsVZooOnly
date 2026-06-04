using System;
using UnityEngine;

public class Spot : Interactable
{
    public ValuedCarryable onTop;
    public bool setpos;
    public Transform posTextPos;
    public bool setTextPos;
    public override void Drop(ValuedCarryable carriable)
    {

        this.onTop = carriable;
        carriable.canBeUsed = false;
        carriable.transform.SetParent(transform);
        if (setpos)
            carriable.transform.position = transform.position;
        if(setTextPos)
            carriable.SetTargetPositionText(posTextPos);
        base.Drop(carriable);    
    }

    public override ValuedCarryable Take()
    {

        ValuedCarryable temp = onTop;
        onTop.CancelTargetPositionText();
        onTop = null;
        base.Take();
        return temp;
    }

    public override ValuedCarryable GetOnTop()
    {
        return onTop;
    }
}
