using UnityEngine;

public class PotionGlobalParent : MonoBehaviour
{
    private void Start()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(0);
            
            child.SetParent(SingletonNumberAndFollowers.instance.transform);
        }
        Destroy(gameObject);
    }
}
