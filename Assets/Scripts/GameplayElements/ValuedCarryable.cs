using System;
using UnityEngine;

public class ValuedCarryable : Interactable
{
    [SerializeField] Value value;
    [SerializeField] TMPro.TMP_Text valueText;
    [SerializeField] Collider2D[] colliders;
    private void Start()
    {
        ShowText();
    }
    public override ValuedCarryable Take()
    {
        return this;
    }

    public int GetValueInt()
    {
        return value.value;
    }

    public Value GetValue()
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
        valueText.text = value.value.ToString();
    }

    public void SetValue(Value value)
    {
        this.value = value;
        UpdateText();
    }

    internal void SetValue(int value)
    {
        this.value = new Value(value);
        UpdateText();
    }

    internal void EnableAllColliders(bool enable)
    {
        foreach (var col in colliders)
        {
            col.enabled = enable;
        }
    }
}
