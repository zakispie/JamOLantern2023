using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

/// <summary>
/// Class to handle the button-matching dance minigame
/// </summary>
public class DanceButtonMatch : DanceMinigame
{
    [Tooltip("Dance Actions That Represent A Sequence of Dance Actions")]
    [SerializeField] private List<DanceAction> comboSequence;
    
    [Tooltip("Sprites to Represent the Dance Actions (should be same order as comboSequence)")]
    [SerializeField] private List<Sprite> comboSpriteSequence;
    
    [Tooltip("Delay between combos (50 = 1 second)")]
    [SerializeField] private int delayBetweenCombos;

    // Tracks whether there is currently a dance prompt on screen
    private bool danceInProgress = false;

    // Image that indicates what button to press
    private Image buttonImage;
    
    // Tracks index in current dance sequence
    int placeInCombo = 0;

    // Tracks a counter for delaying how often dance sequences appear
    private int delayCounter = 0;

    /// <summary>
    /// Assigns buttonImage and disables it
    /// </summary>
    void Start()
    {
        buttonImage = GameObject.FindGameObjectWithTag("ButtonImage").GetComponent<Image>();
        buttonImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// Initiates a dance sequence every delayCounter iterations
    /// </summary>
    void FixedUpdate()
    {
        if (!danceInProgress)
        {
            delayCounter++;
        }

        if (delayCounter >= delayBetweenCombos)
        {
            delayCounter = 0;
            danceInProgress = true;
            StartDance();
        }
    }

    /// <summary>
    /// Initiates a dance sequence
    /// </summary>
    void StartDance()
    {
        buttonImage.gameObject.SetActive(true);
        DisplayNextAction();
        danceInProgress = true;
    }
    
    /// <summary>
    /// Determines outcome of dance based on a given DanceAction
    /// </summary>
    /// <param name="action"> The DanceAction to attempt </param>
    /// <returns> DanceStatus representing the result of the dance input </returns>
    public override DanceStatus PerformDance(DanceAction action)
    {
        if (!danceInProgress) { return DanceStatus.Neutral; } // ignore input if dance is not active 

        // Dance Move Failed:
        if (comboSequence[placeInCombo] != action) {
            // Dance combo failed
           return DanceFail();
        }

        // Dance Move Succeeded:
        if (++placeInCombo == comboSequence.Count) {
            // Completed the dance combo successfully
            return DanceSuccess();
        }
        
        // Combo not finished yet, another key needed
        DisplayNextAction();
        return DanceStatus.Correct;
    }

    /// <summary>
    /// Handles success of a dance combo
    /// </summary>
    /// <returns> DanceStatus.Correct </returns>
    DanceStatus DanceSuccess()
    {
        placeInCombo = 0;
        buttonImage.gameObject.SetActive(false);
        Debug.Log("Combo Success!");
        placeInCombo = 0;
        danceInProgress = false;
        return DanceStatus.Correct;
    }

    /// <summary>
    /// Handles failure of a dance combo
    /// </summary>
    /// <returns> DanceStatus.Incorrect </returns>
    DanceStatus DanceFail()
    {
        placeInCombo = 0;
        buttonImage.gameObject.SetActive(false);
        Debug.Log("Combo Broken!");
        placeInCombo = 0;
        danceInProgress = false;
        return DanceStatus.Incorrect;
    }

    /// <summary>
    /// Changes the displayed key to be the correct next key
    /// </summary>
    void DisplayNextAction()
    {
        buttonImage.sprite = comboSpriteSequence[placeInCombo];
    }
}