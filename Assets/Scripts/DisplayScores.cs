using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayScores : MonoBehaviour
{
    [SerializeField] private GameObject scoreCanvas;
    //private ScoreCounter scores;
    // Start is called before the first frame update
    void Start()
    {
        //scores = FindObjectOfType<ScoreCounter>();
        int danceMoveHighScore = PlayerPrefs.GetInt("DanceMoveHighScore", 0);
        if (danceMoveHighScore < ScoreCounter.GetScore(ScoreCounter.ScoreType.DanceMove))
        {
            PlayerPrefs.SetInt("DanceMoveHighScore", ScoreCounter.GetScore(ScoreCounter.ScoreType.DanceMove));
            danceMoveHighScore = ScoreCounter.GetScore(ScoreCounter.ScoreType.DanceMove);
        }
        
        int danceSequenceHighScore = PlayerPrefs.GetInt("DanceSequenceHighScore", 0);
        if (danceSequenceHighScore < ScoreCounter.GetScore(ScoreCounter.ScoreType.DanceSequence))
        {
            PlayerPrefs.SetInt("DanceSequenceHighScore", ScoreCounter.GetScore(ScoreCounter.ScoreType.DanceSequence));
            danceSequenceHighScore = ScoreCounter.GetScore(ScoreCounter.ScoreType.DanceSequence);
        }
        
        int partygoers = PlayerPrefs.GetInt("PartygoersHighScore", 0);
        if (partygoers < ScoreCounter.GetScore(ScoreCounter.ScoreType.Partygoer))
        {
            PlayerPrefs.SetInt("Partygoers", ScoreCounter.GetScore(ScoreCounter.ScoreType.Partygoer));
            partygoers = ScoreCounter.GetScore(ScoreCounter.ScoreType.Partygoer);
        }

        int totalHighScore = PlayerPrefs.GetInt("TotalHighScore", 0);
        int realTimeHighScore = ScoreCounter.GetScore(ScoreCounter.ScoreType.DanceSequence) + 
                                ScoreCounter.GetScore(ScoreCounter.ScoreType.Partygoer) +
                                ScoreCounter.GetScore(ScoreCounter.ScoreType.DanceMove) +
                                ScoreCounter.GetScore(ScoreCounter.ScoreType.Time) -
                                ScoreCounter.GetScore(ScoreCounter.ScoreType.FailedMove);
        if (totalHighScore < realTimeHighScore)
        {
            PlayerPrefs.SetInt("TotalHighScore", realTimeHighScore);
            totalHighScore = realTimeHighScore;
        }
        
        scoreCanvas.GetComponentInChildren<TextMeshProUGUI>().text =
            ScoreCounter.GetScore(ScoreCounter.ScoreType.DanceMove) + "\n" +
            ScoreCounter.GetScore(ScoreCounter.ScoreType.DanceSequence) + "\n" +
            ScoreCounter.GetScore(ScoreCounter.ScoreType.Partygoer) + "\n" +
            ScoreCounter.GetScore(ScoreCounter.ScoreType.Time) + "\n-" +
            ScoreCounter.GetScore(ScoreCounter.ScoreType.FailedMove) + "\n \n" +
            realTimeHighScore + "\n" +
            totalHighScore;
    }

    public void MainMenu()
    {
        Destroy(FindObjectOfType<ScoreCounter>());
        SceneManager.LoadScene(0);
    }
}
