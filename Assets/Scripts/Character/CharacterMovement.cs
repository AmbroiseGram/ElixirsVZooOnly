using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;


    public void Move(InputAction.CallbackContext context)
    {
        rb.linearVelocity = context.ReadValue<Vector2>() * moveSpeed;
    }
}
