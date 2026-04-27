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
        valueText.text = "";
    }

    public void ShowText()
    {
        valueText.text = value.ToString();
    }
}
