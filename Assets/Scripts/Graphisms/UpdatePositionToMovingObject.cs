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
        if (target == null)
            Destroy(gameObject);
        else
            transform.position = target.position + offset;
    }
}
