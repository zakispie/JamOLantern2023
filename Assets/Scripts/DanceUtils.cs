using UnityEngine;

/// <summary>
/// Represents an action in a dance, usually represented by a keypress
/// </summary>
public enum DanceAction
{
    UpDance,
    DownDance,
    LeftDance,
    RightDance
}

/// <summary>
/// Represents a dance result (success, failure, ignore)
/// </summary>
public enum DanceStatus
{
    Correct,
    Incorrect,
    Neutral
}

/// <summary>
/// Represents a dance minigame that can be played given a DanceAction
/// </summary>
public abstract class DanceMinigame : MonoBehaviour
{
    public abstract DanceStatus PerformDance(DanceAction action);
}