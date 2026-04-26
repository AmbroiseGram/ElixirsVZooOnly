using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterInteractionManager : MonoBehaviour
{
    [SerializeField] private PlayerInteractableDetector interactableDetector;

    Interactable holded = null;

    [SerializeField] GameObject placeholderHolded;
    [SerializeField] TMPro.TMP_Text value;
    private void Start()
    {
        interactableDetector.SetTargetCarryable(true);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        Interactable temp = interactableDetector.GetCurrentTarget();
        if (holded == null)
        {   
            if (temp == null)
                return;

            holded = temp;
            holded.active = false;
            holded.gameObject.SetActive(false);
            placeholderHolded.SetActive(true);
            value.text = holded.GetComponent<ValuedCarryable>().GetValue().ToString();
            interactableDetector.SetTargetCarryable(false);
        }
    }

}
