using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager instance;
    [SerializeField]List<int> starValues;
    [SerializeField] List<GameObject> starsUI;
    [SerializeField] List<GameObject> starsUIEndPanel;
    int currentStars = 0;
    void Awake()
    {
        instance = this;
    }
    public void CheckStar(int value)
    {
        if (currentStars == starValues.Count)
            return;
        if(value >= starValues[currentStars])
        {
                starsUI[currentStars].SetActive(true);
                starsUIEndPanel[currentStars].SetActive(true);
            currentStars++;
        }
    }

}
