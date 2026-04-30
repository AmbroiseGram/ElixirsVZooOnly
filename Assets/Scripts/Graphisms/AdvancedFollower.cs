using UnityEngine;

public class AdvancedFollower : MonoBehaviour
{
    public GameObject target;
    public float offset;
    [Range(1f, 20f)]
    public float followSpeed;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + Vector3.up * offset, Time.deltaTime*followSpeed);

        Vector3 direction = target.transform.position - transform.position;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * followSpeed);
        }
    }

}