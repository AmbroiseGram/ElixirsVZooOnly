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
        operation = new Operation();
        Enter1.onFill += OnSpotFilled;
        if(Enter2 != null)
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
        ValuedCarryable Entry1 = Enter1.Take();
        Value input1 = Entry1.GetValue();
        Destroy(Enter1.Take().gameObject);
        ValuedCarryable newPotion = Instantiate(potionPrefab, Exit.transform.position, Quaternion.identity).GetComponentInChildren<ValuedCarryable>();
        newPotion.SetValue(operation.Operate(input1).value);
        yield return null;
    }
}

