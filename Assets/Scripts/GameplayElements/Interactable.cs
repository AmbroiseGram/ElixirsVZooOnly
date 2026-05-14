using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual ValuedCarryable Take()
    {
        onTake?.Invoke();
        return null;
    }

    public virtual void Drop(ValuedCarryable toDrop)
    {
        onFill?.Invoke(toDrop);
    }

    public virtual void PreDrop(ValuedCarryable toDrop)
    {

    }

    public event Action onTake;
    public event Action<ValuedCarryable> onFill;

    public bool canTake;
    public bool canFill;
    public bool canBeUsed = true;
}
