using UnityEngine;

public class Delivery : Spot
{
    public override void PreDrop(ValuedCarryable toDrop)
    {
        base.PreDrop(toDrop);
        OrderManager.instance.TrySolve(toDrop.GetValue());
    }
    public override void Drop(ValuedCarryable carriable)
    {
        base.Drop(carriable);

        Destroy(carriable.gameObject);
        canFill = true;
        canTake = false;
    }
}
