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
    void Start()
    {
        operation = new Operation();
        Enter1.onFill += OnTestEffectorEvent;
        Exit.onTake += OnTestEffector;
        operationText.text = operation.GetSymbol() + value.value.ToString();
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
        Enter1.canBeUsed = false;
        if(Enter2)
            Enter2.canBeUsed = false;
        Exit.canBeUsed = false;
        ValuedCarryable Entry1 = Enter1.Take();
        Value input1 = Entry1.GetValue();

        float currentTime = 0;
        Vector3 basepos = Entry1.transform.position;
        while(currentTime < timeAnimationPotion)
        {
            currentTime += Time.deltaTime;
            Vector3 targetPos = Vector3.Lerp(basepos, posPotion.position, currentTime / timeAnimationPotion);
            targetPos.y = Mathf.Sin(Mathf.PI * currentTime);
            Entry1.transform.position = targetPos;
            yield return 0;
        }


        Destroy(Entry1.gameObject);
        ValuedCarryable newPotion = Instantiate(potionPrefab, Exit.transform.position, Quaternion.identity).GetComponentInChildren<ValuedCarryable>();
        newPotion.SetValue(operation.Operate(input1, value).value);
        Exit.Drop(newPotion);
        yield return null;
    }
}

