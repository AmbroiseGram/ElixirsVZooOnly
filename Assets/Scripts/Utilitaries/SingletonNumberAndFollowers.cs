using UnityEngine;

public class SingletonNumberAndFollowers : MonoBehaviour
{
    public static SingletonNumberAndFollowers instance;

    private void Awake()
    {
        instance = this;
    }

}
