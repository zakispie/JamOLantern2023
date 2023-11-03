using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Tooltip("Time in seconds before the player loses")]
    [SerializeField] private int secondsUntilLose;

    [Tooltip("Count down?")]
    [SerializeField] private bool timerEnabled;
    
    [Tooltip("Text object to display the time")]
    [SerializeField] private TextMeshProUGUI timeText;

    /// <summary>
    /// Begins the countdown coroutine
    /// </summary>
    void Start()
    {
        StartCoroutine(Countdown());
    }

    /// <summary>
    /// Handles counting down 1 second at a time
    /// </summary>
    /// <returns> Waits one second between each count </returns>
    IEnumerator Countdown()
    {
        while (timerEnabled)
        {
            int seconds = secondsUntilLose % 60;
            string secondsString = seconds < 10 ? "0" + seconds : seconds.ToString();
            timeText.text = "Time Remaining: " + (secondsUntilLose / 60) + ":" + secondsString;
            if (secondsUntilLose < 0)
            {
                timerEnabled = false;
                timeText.text = "Time Remaining: 0:00";
                Debug.Log("Game over!");
            }

            yield return new WaitForSeconds(1);
            secondsUntilLose--;
        }
        FindObjectOfType<LevelManager>().RealExit();
        yield break;
    }

    public int GetSeconds()
    {
        return secondsUntilLose;
    }
}
