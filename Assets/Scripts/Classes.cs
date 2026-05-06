using UnityEngine;


    public class Operation
    {
        public Value Operate(Value a)
        {
            return a;
        }
    }

    public class Value
    {
        public int value;

    public Value(int value)
    {
        this.value = value;
    }
}

