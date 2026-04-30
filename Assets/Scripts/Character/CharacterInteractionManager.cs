using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterInteractionManager : MonoBehaviour
{
    [SerializeField] private PlayerInteractableDetector interactableDetector;

    ValuedCarryable holded = null;

    [SerializeField] GameObject placeholderHolded;
    private void Start()
    {
        interactableDetector.SetTargetCarryable(true);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        if (holded == null)
            TryHold();
        else
            TryDrop();
    }

    private void TryDrop()
    {
        Interactable temp = interactableDetector.GetCurrentTarget();

        holded.gameObject.SetActive(true);
        holded.canBeUsed = true;
        holded.transform.SetParent(null);
        placeholderHolded.SetActive(false);
        interactableDetector.SetTargetCarryable(true);
        
        if (temp != null)
        {
            temp.Drop(holded.GetComponent<ValuedCarryable>());
        }
        else
        {
            holded.transform.position = transform.position + Vector3.up;

            holded.ShowText();
        }
        holded = null;
    }

    private void TryHold()
    {
        Interactable temp = interactableDetector.GetCurrentTarget();
        if (temp == null)
            return;
        ValuedCarryable tempV = temp.Take(); 
        if(temp == null)
            return;  
        holded = tempV;
        holded.canBeUsed = false;
        holded.transform.SetParent(transform);
        holded.gameObject.SetActive(false);
        holded.transform.position = transform.position;
        placeholderHolded.SetActive(true);
        interactableDetector.SetTargetCarryable(false);
    }
}
