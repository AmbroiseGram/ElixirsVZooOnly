using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    Value value;
    float timeBeforeExpire;
    float currentTimeBeforeExpire;
    [SerializeField] TMP_Text valueText;
    bool isSetup = false;
    [SerializeField] Image timeBar;
    [SerializeField] List<float> timeBeforeExpireByLevels;
    int level;
    [SerializeField] Image orderImageDiff;
    [SerializeField] List<Sprite> spritesByLevels;
    public void Setup(Value value, int level)
    {
        this.value = value;
        isSetup = true;
        this.level = level;
        currentTimeBeforeExpire = 0;
        timeBeforeExpire = timeBeforeExpireByLevels[level];
        valueText.text = value.value.ToString();
        orderImageDiff.sprite = spritesByLevels[level];
    }
    public void Update()
    {
        if (!isSetup) return;
        currentTimeBeforeExpire += Time.deltaTime;
        timeBar.fillAmount = 1 - GetTimeBeforeExpire();
        if (currentTimeBeforeExpire >= timeBeforeExpire)
        {
            Destroy(gameObject);
        }
    }

    public Value GetValue()
    {
        return value;
    }

    public float GetTimeBeforeExpire()
    {
        return currentTimeBeforeExpire / timeBeforeExpire;
    }

    internal int GetDifficulty()
    {
        return level;
    }
}
