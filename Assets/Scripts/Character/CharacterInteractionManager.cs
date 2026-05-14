using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.UIElements;
using System.Threading;
using static UnityEditor.PlayerSettings;
public class CharacterInteractionManager : MonoBehaviour
{
    [SerializeField] private PlayerInteractableDetector interactableDetector;

    ValuedCarryable holded = null;

    [SerializeField] GameObject placeholderHolded;
    bool isTaking =false;
    [SerializeField] private float heightPotion = 1f;
    private void Start()
    {
        interactableDetector.SetTargetCarryable(true);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        if (isTaking)
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

        StartCoroutine(DropAnimation(holded, (temp != null) ? temp.transform.position : transform.position + Vector3.up, temp));
        holded = null;
    }

    private IEnumerator DropAnimation(ValuedCarryable carried, Vector3 pos, Interactable dropPlace)
    {
        carried.EnableAllColliders(false);
        carried.transform.SetParent(null);
        if (dropPlace != null)
            dropPlace.PreDrop(carried);
        yield return StartCoroutine(MoveCurve(carried.gameObject, pos, 0.5f, (dropPlace != null) ? dropPlace.transform : null));

        carried.EnableAllColliders(true);
        if (dropPlace != null)
            dropPlace.Drop(carried);
        yield return 0;
    }

    private IEnumerator TakeAnimation(ValuedCarryable tempV)
    {
        isTaking = true;
        holded = tempV;
        holded.canBeUsed = false;
        holded.transform.SetParent(transform);
        holded.EnableAllColliders(false);
        yield return StartCoroutine(MoveCurve(holded.gameObject, transform.position, 0.5f, transform));

        holded.gameObject.SetActive(false);
        holded.transform.position = transform.position;
        placeholderHolded.SetActive(true);
        interactableDetector.SetTargetCarryable(false);
        isTaking = false;
    }

    private IEnumerator MoveCurve(GameObject target, Vector3 destination, float duration, Transform targetDestination = null)
    {
        float currentTime = 0;
        Vector3 basepos = target.transform.position;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float lerp = currentTime / duration;
            if(targetDestination != null)
                destination = targetDestination.position;
            Vector3 targetPos = Vector3.Lerp(basepos, destination, lerp);
            targetPos.y = heightPotion * Mathf.Sin(Mathf.PI * lerp) + Mathf.Lerp(basepos.y, destination.y, lerp);
            target.transform.position = targetPos;
            yield return 0;
        }
    }

    private void TryHold()
    {
        Interactable temp = interactableDetector.GetCurrentTarget();
        if (temp == null)
            return;
        ValuedCarryable tempV = temp.Take(); 
        if(temp == null)
            return;  
        StartCoroutine(TakeAnimation(tempV));

    }
}
