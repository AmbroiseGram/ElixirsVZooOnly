using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class OrderValueGenerator : MonoBehaviour
{
    public static OrderValueGenerator instance;
    int depth = 5;
    List<ListValue> orderValues = new List<ListValue>();
    private void Awake()
    {
        List<Effector> effectors = FindObjectsByType<Effector>(FindObjectsSortMode.None).ToList();
        List<Stock> stocks = FindObjectsByType<Stock>(FindObjectsSortMode.None).ToList();
        instance = this;
        orderValues = new List<ListValue>();
        orderValues.Add(new ListValue());
        List<Value> uniqueValues = new List<Value>();
        foreach (Stock stock in stocks)
        {
            if(uniqueValues.Find(v => v.value == stock.value.value) == null)
            {
                uniqueValues.Add(stock.value);
                orderValues[0].values.Add(stock.value);
            }


        }

        for (int i = 1; i < depth; i++)
        {
            orderValues.Add(new ListValue());
            foreach (Effector effector in effectors)
            {
                foreach (Value value in orderValues[i - 1].values)
                {
                    Value newValue = effector.operation.Operate(value, effector.value);
                    if (uniqueValues.Find(v => v.value == newValue.value) == null)
                    {
                        uniqueValues.Add(newValue);
                        orderValues[i].values.Add(effector.operation.Operate(value, effector.value));
                    }
                }
            }
        }
    }

public Value GetRandomValue(int difficulty)
    {
        return orderValues[difficulty].values[Random.Range(0, orderValues[difficulty].values.Count)];
    }
}
