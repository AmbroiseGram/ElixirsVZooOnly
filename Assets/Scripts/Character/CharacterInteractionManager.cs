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
    [SerializeField] private GameObject placeholderHolded;
    [SerializeField] private float heightPotion = 1f;

    private ValuedCarryable holded = null;
    private bool isAnimating = false;

    private void Start()
    {
        interactableDetector.SetTargetCarryable(true);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.started || isAnimating)
            return;

        if (holded == null)
            StartCoroutine(TryHold());
        else
            StartCoroutine(TryDrop()); // Changé en StartCoroutine pour suivre le flux proprement
    }

    private IEnumerator TryHold()
    {
        Interactable temp = interactableDetector.GetCurrentTarget();
        if (temp == null)
            yield break;

        ValuedCarryable tempV = temp.Take();
        if (tempV == null) // Correction ici : on vérifie l'objet récupéré, pas le réceptacle à nouveau
            yield break;

        isAnimating = true;
        yield return StartCoroutine(TakeAnimation(tempV));
        isAnimating = false;
    }

    private IEnumerator TryDrop()
    {
        Interactable temp = interactableDetector.GetCurrentTarget();

        // Si on n'a pas de cible ou si la cible ne peut pas prendre l'objet, on drop au sol normalement
        if (temp == null || !temp.canTake || temp.GetOnTop() == null)
        {
            isAnimating = true;

            holded.gameObject.SetActive(true);
            holded.canBeUsed = true;
            holded.transform.SetParent(null);
            placeholderHolded.SetActive(false);
            interactableDetector.SetTargetCarryable(true);
            holded.EnableAllColliders(false);

            if (temp != null)
                temp.PreDrop(holded);

            Vector3 dropPosition = (temp != null) ? temp.transform.position : transform.position + Vector3.up;
            Transform dropTarget = (temp != null) ? temp.transform : null;

            yield return StartCoroutine(MoveCurve(holded.gameObject, dropPosition, 0.5f, dropTarget));

            holded.EnableAllColliders(true);
            if (temp != null)
                temp.Drop(holded);

            holded = null;
            isAnimating = false;
        }
        else
        {
            // Cas du Swap (Toast) : temp != null && temp.canTake
            isAnimating = true;
            yield return StartCoroutine(SwapAnimation(temp));
            isAnimating = false;
        }
    }

    private IEnumerator TakeAnimation(ValuedCarryable tempV)
    {
        holded = tempV;
        holded.canBeUsed = false;
        holded.transform.SetParent(transform);
        holded.EnableAllColliders(false);

        yield return StartCoroutine(MoveCurve(holded.gameObject, transform.position, 0.5f, transform));

        holded.gameObject.SetActive(false);
        holded.transform.position = transform.position;
        placeholderHolded.SetActive(true);
        interactableDetector.SetTargetCarryable(false);
    }

    private IEnumerator SwapAnimation(Interactable targetInteractable)
    {
        // 1. Préparation de l'objet actuellement tenu (holded) pour son départ
        ValuedCarryable oldHolded = holded;
        oldHolded.gameObject.SetActive(true);
        oldHolded.canBeUsed = true;
        oldHolded.transform.SetParent(null);
        placeholderHolded.SetActive(false);
        oldHolded.EnableAllColliders(false);

        targetInteractable.PreDrop(oldHolded);

        // 2. Récupération simultanée de la potion/objet se trouvant dans le réceptacle (temp)
        ValuedCarryable newHolded = targetInteractable.Take();
        newHolded.canBeUsed = false;
        newHolded.transform.SetParent(transform);
        newHolded.EnableAllColliders(false);

        // 3. Animation simultanée des deux objets
        float currentTime = 0;
        float duration = 0.5f;

        Vector3 startPosOld = oldHolded.transform.position;
        Vector3 startPosNew = newHolded.transform.position;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float lerp = currentTime / duration;

            // Trajectoire de l'ancien objet vers le réceptacle
            Vector3 targetPosOld = Vector3.Lerp(startPosOld, targetInteractable.transform.position, lerp);
            targetPosOld.y += heightPotion * Mathf.Sin(Mathf.PI * lerp);
            oldHolded.transform.position = targetPosOld;

            // Trajectoire du nouvel objet vers le joueur
            Vector3 targetPosNew = Vector3.Lerp(startPosNew, transform.position, lerp);
            targetPosNew.y += heightPotion * Mathf.Sin(Mathf.PI * lerp);
            newHolded.transform.position = targetPosNew;

            yield return null;
        }

        // 4. Finalisation pour l'ancien objet déposé
        oldHolded.EnableAllColliders(true);
        targetInteractable.Drop(oldHolded);

        // 5. Finalisation pour le nouvel objet récupéré
        holded = newHolded;
        holded.gameObject.SetActive(false);
        holded.transform.position = transform.position;
        placeholderHolded.SetActive(true);
        interactableDetector.SetTargetCarryable(false);
    }

    private IEnumerator MoveCurve(GameObject target, Vector3 destination, float duration, Transform targetDestination = null)
    {
        float currentTime = 0;
        Vector3 basepos = target.transform.position;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float lerp = currentTime / duration;

            if (targetDestination != null)
                destination = targetDestination.position;

            Vector3 targetPos = Vector3.Lerp(basepos, destination, lerp);
            targetPos.y += heightPotion * Mathf.Sin(Mathf.PI * lerp); // Légère correction de la formule pour la clarté
            target.transform.position = targetPos;

            yield return null;
        }
    }
}
