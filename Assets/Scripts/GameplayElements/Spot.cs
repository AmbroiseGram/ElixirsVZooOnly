using System;
using UnityEngine;

public class Spot : Interactable
{
    public ValuedCarryable onTop;

    public override void Drop(ValuedCarryable carriable)
    {
        if (canFill)
        {
            this.onTop = carriable;
            carriable.canBeUsed = false;
            carriable.transform.position = transform.position;
            carriable.transform.SetParent(transform);
            canFill = false;
            canTake = true;
        }
        base.Drop(carriable);    
    }

    public override ValuedCarryable Take()
    {
        canTake = false;
        canFill = true;
        base.Take();
        return onTop;
    }

}
