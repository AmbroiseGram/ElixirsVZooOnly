using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual ValuedCarryable Take()
    {
        return null;
    }

    public virtual void Drop(ValuedCarryable toDrop)
    {
        
    }

    public bool canTake;
    public bool canFill;
    public bool canBeUsed = true;
}
