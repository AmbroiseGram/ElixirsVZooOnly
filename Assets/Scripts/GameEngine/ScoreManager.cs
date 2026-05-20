using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] List<int> scoreByLevel;
    public static ScoreManager Instance;
    int score = 0;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text scoreText2;
    private void Awake()
    {
        Instance = this;    
    }
    public void ScoreOrder(int level)
    {
        score += scoreByLevel[level];
        scoreText.text = score.ToString();
        scoreText2.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
