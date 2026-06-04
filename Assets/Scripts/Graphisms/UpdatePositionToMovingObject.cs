using UnityEngine;

public class UpdatePositionToMovingObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    Vector3 offset;
    Vector3 overrideDest;
    bool overrideDestSet = false;
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

    public void SetNewDest(Vector3 overrideD)
    {
        overrideDest = overrideD;
        overrideDestSet = true;
    }

    public void CancelDest()
    {
        overrideDestSet = false;
    }
}
