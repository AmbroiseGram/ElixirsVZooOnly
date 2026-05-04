using System;
using System.Collections;
using UnityEngine;

public class Effector : MonoBehaviour
{
    [SerializeField] private Spot Enter1;
    [SerializeField] private Spot Enter2;
    [SerializeField] private Spot Exit;
    [SerializeField] private GameObject potionPrefab;
    public Operation operation;
    void Start()
    {
        Enter1.onFill += OnSpotFilled;
        Enter2.onFill += OnSpotFilled;
    }

    private void OnSpotFilled(ValuedCarryable carryable)
    {
        if(Enter1.onTop != null && (Enter2 == null || Enter2.onTop != null))
        {
            StartCoroutine(Declenche());
        }
    }

    private IEnumerator Declenche()
    {
        Destroy(Enter1.Take().gameObject);
        ValuedCarryable newPotion = Instantiate(potionPrefab, Exit.transform.position, Quaternion.identity).GetComponent<ValuedCarryable>();
      //  newPotion
        yield return null;
    }
}

public class  Operation
{
    public int Operate(int a)
    {
        return a;
    }
}