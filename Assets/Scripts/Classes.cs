using System.Collections.Generic;
using UnityEngine;

public enum EOperationType
{
    Add,
    Subtract,
    Multiply,
    Divide
}

[System.Serializable]
public class Operation
    {

    public EOperationType operationType;
    public Value Operate(Value a, Value b)
    {
        switch(operationType)
        {
            case EOperationType.Add:
                return new Value(a.value + b.value);
            case EOperationType.Subtract:
                return new Value(a.value - b.value);
            case EOperationType.Multiply:
                return new Value(a.value * b.value);
            case EOperationType.Divide:
                return new Value(a.value / b.value);
        }
        return a;
        }
    }

[System.Serializable]
public class Value
    {
        public int value;

    public Value(int value)
    {
        this.value = value;
    }
}

[System.Serializable]
public class ListValue
{
    public List<Value> values;
    public ListValue()
    {
        values = new List<Value>();
    }
}

