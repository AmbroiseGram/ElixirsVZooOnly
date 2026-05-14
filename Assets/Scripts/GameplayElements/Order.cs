using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public Value value;
    [SerializeField] float timeBeforeExpire;
    public float currentTimeBeforeExpire;
    [SerializeField] TMP_Text valueText;
    bool isSetup = false;
    [SerializeField] Image timeBar;

    public void Setup(Value value)
    {
        this.value = value;
        isSetup = true;
        currentTimeBeforeExpire = 0;
        valueText.text = value.value.ToString();
    }
    public void Update()
    {
        if (!isSetup) return;
        currentTimeBeforeExpire += Time.deltaTime;
        timeBar.fillAmount = 1 - (currentTimeBeforeExpire / timeBeforeExpire);
        if (currentTimeBeforeExpire >= timeBeforeExpire)
        {
            Destroy(gameObject);
        }
    }
}
