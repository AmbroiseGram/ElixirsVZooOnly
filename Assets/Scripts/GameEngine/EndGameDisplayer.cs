using UnityEngine;

public class EndGameDisplayer : MonoBehaviour
{
    [SerializeField] GameObject endGameDisplay;

    public static EndGameDisplayer instance;

    private void Start()
    {
        instance = this;
    }
    public void EndGame()
    { 
        endGameDisplay.SetActive(true);
    }
}
