using System;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedFollower : MonoBehaviour
{
    public GameObject target;
    public float offset;
    [Range(1f, 20f)]
    public float followSpeed;
    
    private bool fixTarget;
    private Transform overrideTarget;
    [SerializeField]
    private SpriteRenderer toHide;
    internal void SetNewDest(Transform position)
    {
        fixTarget = true;
        overrideTarget = position;
        toHide.enabled = false;
    }

    public void CancelDest()
    {
        fixTarget = false;
        toHide.enabled = true;
    }

    private void Update()
    {
        if (target.gameObject == null)
            Destroy(gameObject);
        else
        {
            Vector3 targetPosition = fixTarget ? overrideTarget.position : target.transform.position + Vector3.up * offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

            if(fixTarget)
                return;
            Vector3 direction = target.transform.position - transform.position;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * followSpeed);
            }
        }
    }

}