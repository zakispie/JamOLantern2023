using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [Tooltip("Value of a successfully hit dance move")]
    public int danceMoveValue;

    [Tooltip("Value of a successfully hit dance sequence")]
    public int danceSequenceValue;

    [Tooltip("Value for each partygoer successfully brought out")]
    public int partygoerValue;

    [Tooltip("Value per second remaining on the clock")]
    public int timeValue;
    
    [Tooltip("Value for each missed dance move (subtracted)")]
    public int failedMoveValue;

    private static ScoreCounter instance;

    private static int playerScore = 0;

    private static int danceMoveScore = 0;

    private static int danceSequenceScore = 0;

    private static int partygoerScore = 0;

    private static int timeScore = 0;

    private static int failedMoveScore = 0;

    public enum ScoreType
    {
        DanceMove,
        DanceSequence,
        Partygoer,
        Time,
        FailedMove
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ResetAllScores()
    {
        playerScore = 0;
        danceMoveScore = 0;
        danceSequenceScore = 0;
        partygoerScore = 0;
        timeScore = 0;
        failedMoveScore = 0;
    }

    public static void AddScore(ScoreType scoreType)
    {
        switch (scoreType)
        {
            case ScoreType.DanceMove:
                danceMoveScore += instance.danceMoveValue;
                break;
            case ScoreType.DanceSequence:
                danceSequenceScore += instance.danceSequenceValue;
                break;
            case ScoreType.Partygoer:
                partygoerScore += instance.partygoerValue;
                break;
            case ScoreType.Time:
                timeScore += instance.timeValue;
                break;
            case ScoreType.FailedMove:
                failedMoveScore += instance.failedMoveValue;
                break;
        }
    }

    public static int GetScore(ScoreType scoreType)
    {
        switch (scoreType)
        { 
            case ScoreType.DanceMove:
                return danceMoveScore;
            case ScoreType.DanceSequence:
                return danceSequenceScore;
            case ScoreType.Partygoer:
                return partygoerScore;
            case ScoreType.Time:
                return timeScore;
            case ScoreType.FailedMove:
                return failedMoveScore;
        }
        return 0;
    }
}
