using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] TMP_Text text;
    bool isPlaying = true;
    void Update()
    {
        if(!isPlaying) return;
        duration -= Time.deltaTime;
        text.text = Mathf.CeilToInt(duration).ToString();
        if(duration < 0)
            {
            isPlaying = false;
            EndGame();
        }
    }



    private void EndGame()
    {
        Time.timeScale = 0;
        EndGameDisplayer.instance.EndGame();
    }
}
