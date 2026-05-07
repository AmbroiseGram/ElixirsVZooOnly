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
    public Value value;
    void Start()
    {
        operation = new Operation();
        Enter1.onFill += OnTestEffectorEvent;
        Exit.onTake += OnTestEffector;
    }

    private void OnTestEffectorEvent(ValuedCarryable carryable)
    {
        OnTestEffector();
    }

    private void OnTestEffector()
    {
        if(Enter1.onTop != null && Exit.onTop == null)
        {
            StartCoroutine(Declenche());
        }
    }

    private IEnumerator Declenche()
    {
        ValuedCarryable Entry1 = Enter1.Take();
        Value input1 = Entry1.GetValue();
        Destroy(Entry1.gameObject);
        ValuedCarryable newPotion = Instantiate(potionPrefab, Exit.transform.position, Quaternion.identity).GetComponentInChildren<ValuedCarryable>();
        newPotion.SetValue(operation.Operate(input1, value).value);
        Exit.Drop(newPotion);
        yield return null;
    }
}

