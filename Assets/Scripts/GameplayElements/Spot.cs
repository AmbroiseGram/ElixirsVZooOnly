using System;
using UnityEngine;

public class Spot : Interactable
{
    public ValuedCarryable onTop;
    public bool setpos;
    public override void Drop(ValuedCarryable carriable)
    {

        this.onTop = carriable;
        carriable.canBeUsed = false;
        carriable.transform.SetParent(transform);
        canFill = false;
        canTake = true;
        if (setpos)
            carriable.transform.position = transform.position;

        base.Drop(carriable);    
    }

    public override ValuedCarryable Take()
    {
        canTake = false;
        canFill = true;

        ValuedCarryable temp = onTop;

        onTop = null;
        base.Take();
        return temp;
    }

}
