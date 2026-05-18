using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Effector : MonoBehaviour
{
    [SerializeField] private Spot Enter1;
    [SerializeField] private Spot Enter2;
    [SerializeField] private Spot Exit;
    [SerializeField] private GameObject potionPrefab;
    public Operation operation;
    public Value value;
    [SerializeField] private TMP_Text operationText;
    [SerializeField] float timeAnimationPotion;
    [SerializeField] private Transform posPotion;
    [SerializeField] private float amplitudeJumpPotion;
    bool skipnext = false;
    void Start()
    {
        operation = new Operation();
        Enter1.onFill += OnTestEffectorEvent;
        Exit.onTake += OnTestEffector;
        operationText.text = operation.GetSymbol() + value.value.ToString();
    }

    private void OnTestEffectorEvent(ValuedCarryable carryable)
    {
        if(skipnext)
        {
            skipnext = false;
            return;
        }
        OnTestEffector();
    }

    private void OnTestEffector()
    {
        if(Enter1.onTop != null)
        {
            StartCoroutine(Declenche());
        }
    }

    private IEnumerator Declenche()
    {
        ActivateSpots(false);
        ValuedCarryable Entry1 = Enter1.Take();
        Value input1 = Entry1.GetValue();     

        yield return new WaitForSeconds(0.5f);
        Destroy(Entry1.gameObject);
        ValuedCarryable newPotion = Instantiate(potionPrefab, Exit.transform.position, Quaternion.identity).GetComponentInChildren<ValuedCarryable>();
        newPotion.SetValue(operation.Operate(input1, value).value);
        skipnext = true;
        Exit.Drop(newPotion);
        ActivateSpots(true);
        yield return null;
    }

    private void ActivateSpots(bool active)
    {
        Enter1.canBeUsed = active;
        if (Enter2)
            Enter2.canBeUsed = active;
        Exit.canBeUsed = active;
    }
}

