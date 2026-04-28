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
    }

}