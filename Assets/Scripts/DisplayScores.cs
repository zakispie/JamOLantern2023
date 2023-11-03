using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayScores : MonoBehaviour
{
    [SerializeField] private GameObject scoreCanvas;
    private ScoreCounter scores;
    // Start is called before the first frame update
    void Start()
    {
        scores = FindObjectOfType<ScoreCounter>();
        scoreCanvas.GetComponentInChildren<TextMeshProUGUI>().text =
            "Dance Move : " + scores.danceMoveValue + "\n" +
            "Dance Sequence : " + scores.danceSequenceValue + "\n" +
            "Partygoers : " + scores.partygoerValue + "\n" +
            "Time : " + scores.timeValue + "\n" +
            "Fails : " + scores.failedMoveValue;
    }

    public void MainMenu()
    {
        Destroy(scores.gameObject);
        SceneManager.LoadScene(0);
    }
}
