using UnityEngine;

public class Spot : Interactable
{
    public ValuedCarryable carriable;
    [SerializeField] TMPro.TMP_Text valueText;

    public override void Drop(ValuedCarryable carriable)
    {
        if (canFill)
        {
            this.carriable = carriable;
            carriable.canBeUsed = false;
            carriable.transform.position = transform.position;
            carriable.transform.SetParent(transform);
            carriable.HideText();
            canFill = false;
            canTake = true;
            valueText.text = carriable.GetValue().ToString();
        }
    }

    public override ValuedCarryable Take()
    {
        canTake = false;
        canFill = true;
        valueText.text = "";
        return carriable;
    }

}
