using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class OrderValueGenerator : MonoBehaviour
{
    int depth = 5;
    List<List<Value>> orderValues = new List<List<Value>>();
    private void Awake()
    {
        List<Effector> effectors = FindObjectsByType<Effector>(FindObjectsSortMode.None).ToList();
        List<Stock> stocks = FindObjectsByType<Stock>(FindObjectsSortMode.None).ToList();
        orderValues = new List<List<Value>>();
        orderValues.Add(new List<Value>());
        List<Value> uniqueValues = new List<Value>();
        foreach (Stock stock in stocks)
        {
            if(uniqueValues.Find(v => v.value == stock.value.value) == null)
            {
                uniqueValues.Add(stock.value);
                orderValues[0].Add(stock.value);
            }


        }

        for (int i = 1; i < depth; i++)
        {
            orderValues.Add(new List<Value>());
            foreach (Effector effector in effectors)
            {
                foreach (Value value in orderValues[i - 1])
                {
                    Value newValue = effector.operation.Operate(value, effector.value);
                    if (uniqueValues.Find(v => v.value == newValue.value) == null)
                    {
                        uniqueValues.Add(newValue);
                        orderValues[i].Add(effector.operation.Operate(value, effector.value));
                    }
                }
            }
        }
    }
}
