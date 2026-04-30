using UnityEngine;

public class UpdatePositionToMovingObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    Vector3 offset;
    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = target.position + offset;
    }
}
