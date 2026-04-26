using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteractableDetector : MonoBehaviour
{
    List<Interactable> interractables = new List<Interactable>();
    Interactable currentTarget;

    bool carryable;
    Interactable lastTarget;
    private void UpdateCurrentCarryable()
    {
        if(interractables.Count == 0)
            currentTarget = null;

        List<Interactable> filtered = interractables.Where(x => (carryable)? x.canTake : x.canFill).ToList();
        currentTarget = filtered.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).FirstOrDefault();
        if(currentTarget != lastTarget)
        {
           // if(currentTarget != null)
           // currentTarget.AddOutline();
           // if (lastTarget != null)
           //     lastTarget.RemoveOutline();
        }
        lastTarget = currentTarget;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        List<Interactable> interactable = collision.gameObject.GetComponents<Interactable>().ToList();
        if (interactable != null && interactable.Count>0)
        {
            foreach(Interactable interactableItem in interactable)
            {
                if(interractables.Contains(interactableItem))
                    continue;
                interractables.Add(interactableItem);
            }
        }
        UpdateCurrentCarryable();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        List<Interactable> interactable = collision.gameObject.GetComponents<Interactable>().ToList();
        if (interactable != null && interactable.Count > 0 )
        {
            foreach (Interactable interactableItem in interactable)
            {
                if (!interractables.Contains(interactableItem))
                    continue;
                interractables.Remove(interactableItem);
            }
        }
        UpdateCurrentCarryable();
    }
    public void SetTargetCarryable(bool carryable)
    {
        this.carryable = carryable;
        UpdateCurrentCarryable();
    }

    public Interactable GetCurrentTarget()
    {
        return currentTarget;
    }


}
