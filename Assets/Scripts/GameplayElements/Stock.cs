using System;
using UnityEngine;

public class Stock : Spot
{
    [SerializeField] private GameObject potion;
    public int value;

    public Sprite[] spritesStock;
    public float timeAnimation;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] float minusPosSpawn;
    [SerializeField] float plusPosSpawn;
    [SerializeField] float timeToSpawn;
    private void Start()
    {
        Generate();
        onTake += Generate;
    }

    private void Generate()
    {
        StopAllCoroutines();
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
        ValuedCarryable temp = Instantiate(potion, transform.position - Vector3.up * minusPosSpawn, Quaternion.identity).GetComponentInChildren<ValuedCarryable>();
        SpriteRenderer spriteRenderer = temp.GetComponentInChildren<SpriteRenderer>();
        temp.SetValue(value);
        Drop(temp);
        time = 0;
        Vector3 basePos = temp.transform.position;
        while (time < timeToSpawn)
        {
            temp.transform.position = Vector3.Lerp(basePos, transform.position + Vector3.up * plusPosSpawn, time / timeToSpawn);
            time += Time.deltaTime;
            yield return null;
        }
        canBeUsed = true;
        time = 0;
        index = spritesStock.Length-1;
        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
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
