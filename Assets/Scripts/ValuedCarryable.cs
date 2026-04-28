using System;
using UnityEngine;

public class ValuedCarryable : Interactable
{
    int value;
    [SerializeField] TMPro.TMP_Text valueText;

    private void Start()
    {
        ShowText();
    }
    public override ValuedCarryable Take()
    {
        return this;
    }

    public int GetValue()
    {
        return value;
    }

    public void HideText()
    {
        valueText.gameObject.SetActive(false);
    }

    public void ShowText()
    {
        UpdateText();
        valueText.gameObject.SetActive(true);
    }

    private void UpdateText()
    {
        valueText.text = value.ToString();
    }

    internal void SetValue(int value)
    {
        this.value = value;
        UpdateText();
    }
}
