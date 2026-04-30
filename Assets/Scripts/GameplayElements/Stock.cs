using System;
using UnityEngine;

public class Stock : Spot
{
    [SerializeField] private GameObject potion;
    public int value;

    public Sprite[] spritesStock;
    public float timeAnimation;

    [SerializeField] private SpriteRenderer sprite;

    private void Start()
    {
        Generate();
        onTake += Generate;
    }

    private void Generate()
    {
        StopCoroutine(GenerateCoroutine());
        canBeUsed = false;
        StartCoroutine(GenerateCoroutine());

    }


    private System.Collections.IEnumerator GenerateCoroutine()
    {
        float time = 0;
        int index = 0;
        while (index < spritesStock.Length-1)
        {
            time += Time.deltaTime;
            if(time > timeAnimation/spritesStock.Length)
            {
                index++;

                sprite.sprite = spritesStock[index];
                time = 0;

            }
            yield return null;
        }
        ValuedCarryable temp = Instantiate(potion, transform.position, Quaternion.identity).GetComponentInChildren<ValuedCarryable>();
        temp.SetValue(value);
        Drop(temp);
        canBeUsed = true;

        time = 0;
        index = spritesStock.Length-1;
        while (index > 0)
        {
            time += Time.deltaTime;
            if (time > timeAnimation / spritesStock.Length)
            {
                index--;
                
                sprite.sprite = spritesStock[index];
                time = 0;
                
            }
            yield return null;
        }
    }
}
