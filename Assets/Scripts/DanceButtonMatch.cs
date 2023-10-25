using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to handle the button-matching dance minigame
/// </summary>
public class DanceButtonMatch : DanceMinigame
{
    [Tooltip("Possible dance sequences")] 
    [SerializeField] private List<DanceSequence> possibleDanceSequences;

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
    
    // Tracks current dance sequence
    private DanceSequence currentDanceSequence;

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
            currentDanceSequence = possibleDanceSequences[Random.Range(0, possibleDanceSequences.Count)];
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
        if (currentDanceSequence.comboSequence[placeInCombo] != action) {
            // Dance combo failed
           return DanceFail();
        }

        // Dance Move Succeeded:
        PlayerController.animator.enabled = false; // disable animator so we can hijack for our sweet moves
        PlayerController.spriteRenderer.sprite = currentDanceSequence.playerSpriteSequence[placeInCombo];
        
        if (++placeInCombo == currentDanceSequence.comboSequence.Count) {
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
        PlayerController.animator.enabled = true; // return control of animator
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
        PlayerController.animator.enabled = true; // return control of animator
        return DanceStatus.Incorrect;
    }

    /// <summary>
    /// Changes the displayed key to be the correct next key
    /// </summary>
    void DisplayNextAction()
    {
        buttonImage.sprite = currentDanceSequence.keyboardSpriteSequence[placeInCombo];
    }
}