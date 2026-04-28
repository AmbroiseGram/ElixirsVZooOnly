using System;
using UnityEngine;

public class Stock : Spot
{
    [SerializeField] private GameObject potion;
    public int value;

    private void Start()
    {
        Generate();
        onTake += Generate;
    }

    private void Generate()
    {
       ValuedCarryable temp = Instantiate(potion, transform.position, Quaternion.identity).GetComponent<ValuedCarryable>();
        temp.SetValue(value);
        Drop(temp);
    }


}
