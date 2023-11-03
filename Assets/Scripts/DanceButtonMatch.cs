using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to handle the button-matching dance minigame
/// </summary>
public class DanceButtonMatch : DanceMinigame
{
    [Header("Dance Configuration")]
    [Tooltip("Possible dance sequences")] 
    [SerializeField] private List<DanceMove> possibleDanceMoves;

    [Tooltip("Delay between combos (50 = 1 second)")]
    [SerializeField] private int delayBetweenCombos;

    [Tooltip("Click sound for countdown on dance")] 
    [SerializeField] private List<AudioClip> countdownSound;

    [Header("Image Files")] 
    [Tooltip("Image for 5 on timer")]
    [SerializeField] private Sprite fiveOnTimer;

    [Tooltip("Image for 6 on timer")] [SerializeField]
    private Sprite sixOnTimer;
    
    [Tooltip("Image for 7 on timer")] [SerializeField]
    private Sprite sevenOnTimer;
    
    [Tooltip("Image for 8 on timer")] [SerializeField]
    private Sprite eightOnTimer;

    [Header("UI Assignments")]
    [Tooltip("Image that indicates what button to press")]
    [SerializeField] private Image buttonImage;
    
    [Tooltip("Image that indicates next button press")]
    [SerializeField] private Image queuedButton1;
    
    [Tooltip("Image that indicates next next button press")]
    [SerializeField] private Image queuedButton2;
    
    [Tooltip("Image that indicates next next next button press")]
    [SerializeField] private Image queuedButton3;
    
    [Tooltip("Will be instantiated when a dance is succeeded")]
    [SerializeField] public GameObject successEffect;
    
    [Tooltip("Will be instantiated when a dance is failed")]
    [SerializeField] public GameObject failEffect;

    // Tracks whether there is currently a dance prompt on screen
    private bool danceInProgress = false;
    
    // Tracks index in current dance sequence
    int placeInCombo = 0;

    // Tracks a counter for delaying how often dance sequences appear
    private int delayCounter = 0;
    
    // Tracks current dance sequence
    private List<DanceMove> currentDanceSequence;
    
    // Tracks whether a dance button press is needed
    private bool danceStarted;

    /// <summary>
    /// Assigns buttonImage and disables it
    /// </summary>
    void Start()
    {
        if (countdownSound.Count != 3)
        {
            Debug.LogError("Should only have 3 countdown sounds! Current number: "  + countdownSound.Count);
        }
        buttonImage.gameObject.SetActive(false);
        queuedButton1.gameObject.SetActive(false);
        queuedButton2.gameObject.SetActive(false);
        queuedButton3.gameObject.SetActive(false);
        currentDanceSequence = new List<DanceMove>();
        for (int i = 0; i < Random.Range(4, 9); i++)
        {
            currentDanceSequence.Add(possibleDanceMoves[Random.Range(0, possibleDanceMoves.Count)]);
        }
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

        if (delayCounter >= delayBetweenCombos) // If it's time to start a new dance sequence
        {
            danceInProgress = true;
            MusicPlayer.readyForDanceEvent = true;
            delayCounter = 0;
        } 
    }

    public IEnumerator StartDanceEvent()
    {
       // yield return new WaitForSeconds(2f); // line up to be on the correct beat
        
        print("Dance event received at: " + MusicPlayer.testStopwatch.ElapsedMilliseconds);
        // dance count 2
        queuedButton3.gameObject.SetActive(true);
        queuedButton3.sprite = fiveOnTimer;
        yield return new WaitForSeconds(0.5f);
        print("Five displayed at: " + MusicPlayer.testStopwatch.ElapsedMilliseconds);
        
        // dance count 3
        queuedButton2.gameObject.SetActive(true);
        queuedButton2.sprite = fiveOnTimer;
        queuedButton3.sprite = sixOnTimer;
        yield return new WaitForSeconds(0.5f);
        print("Six displayed at: " + MusicPlayer.testStopwatch.ElapsedMilliseconds);

        // dance count 4
        queuedButton1.gameObject.SetActive(true);
        queuedButton1.sprite = fiveOnTimer;
        queuedButton2.sprite = sixOnTimer;
        queuedButton3.sprite = sevenOnTimer;
        yield return new WaitForSeconds(0.5f);
        
        // dance count 5
        buttonImage.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(countdownSound[0], PlayerController.playerPosition);
        buttonImage.sprite = fiveOnTimer;
        queuedButton1.sprite = sixOnTimer;
        queuedButton2.sprite = sevenOnTimer;
        queuedButton3.sprite = eightOnTimer;
        yield return new WaitForSeconds(0.5f);
        
        // dance count 6
        AudioSource.PlayClipAtPoint(countdownSound[0], PlayerController.playerPosition);
        buttonImage.sprite = sixOnTimer;
        queuedButton1.sprite = sevenOnTimer;
        queuedButton2.sprite = eightOnTimer;
        queuedButton3.sprite = currentDanceSequence[0].keyboardSpriteSequence;
        yield return new WaitForSeconds(0.5f);
        
        AudioSource.PlayClipAtPoint(countdownSound[1], PlayerController.playerPosition);
        buttonImage.sprite = sevenOnTimer;
        queuedButton1.sprite = eightOnTimer;
        queuedButton2.sprite = currentDanceSequence[0].keyboardSpriteSequence;
        queuedButton3.sprite = currentDanceSequence[1].keyboardSpriteSequence;
        yield return new WaitForSeconds(0.5f);
        
        AudioSource.PlayClipAtPoint(countdownSound[2], PlayerController.playerPosition);
        buttonImage.sprite = eightOnTimer;
        queuedButton1.sprite = currentDanceSequence[0].keyboardSpriteSequence;
        queuedButton2.sprite = currentDanceSequence[1].keyboardSpriteSequence;
        queuedButton3.sprite = currentDanceSequence[2].keyboardSpriteSequence;
        yield return new WaitForSeconds(0.5f);
        
        delayCounter = 0;
        danceStarted = true;
        DisplayNextAction();
        
        yield break;
    }

    /// <summary>
    /// Determines outcome of dance based on a given DanceAction
    /// </summary>
    /// <param name="action"> The DanceAction to attempt </param>
    /// <returns> DanceStatus representing the result of the dance input </returns>
    public override DanceStatus PerformDance(DanceAction action)
    {
        if (!danceStarted) { return DanceStatus.Neutral; } // ignore input if dance is not active 

        // Dance Move Failed:
        if (currentDanceSequence[placeInCombo].comboSequence != action) {
            // Dance combo failed
           return DanceFail();
        }

        // Dance Move Succeeded:
        PlayerController.spriteRenderer.sprite = currentDanceSequence[placeInCombo].playerSpriteSequence;
        
        if (++placeInCombo == currentDanceSequence.Count) {
            // Completed the dance combo successfully
            return DanceSuccess();
        }
        
        // Combo not finished yet, another key needed
        ScoreCounter.AddScore(ScoreCounter.ScoreType.DanceMove);
        DisplayNextAction();
        return DanceStatus.Correct;
    }

    /// <summary>
    /// Handles success of a dance combo
    /// </summary>
    /// <returns> DanceStatus.Correct </returns>
    DanceStatus DanceSuccess()
    {
        danceStarted = false;
        ScoreCounter.AddScore(ScoreCounter.ScoreType.DanceMove);
        ScoreCounter.AddScore(ScoreCounter.ScoreType.DanceSequence);
        buttonImage.gameObject.SetActive(false);
        queuedButton1.gameObject.SetActive(false);
        queuedButton2.gameObject.SetActive(false);
        queuedButton3.gameObject.SetActive(false);
        currentDanceSequence.Clear();
        for (int i = 0; i < Random.Range(4, 9); i++)
        {
            currentDanceSequence.Add(possibleDanceMoves[Random.Range(0, possibleDanceMoves.Count)]);
        }
        Instantiate(successEffect, PlayerController.playerPosition, Quaternion.identity);
        placeInCombo = 0;
        buttonImage.gameObject.SetActive(false);
        Debug.Log("Combo Success!");
        placeInCombo = 0;
        danceInProgress = false;
        PlayerController.spriteRenderer.sprite = PlayerController.defaultSprite; // return sprite
        return DanceStatus.Correct;
    }

    /// <summary>
    /// Handles failure of a dance combo
    /// </summary>
    /// <returns> DanceStatus.Incorrect </returns>
    DanceStatus DanceFail()
    {
        danceStarted = false;
        ScoreCounter.AddScore(ScoreCounter.ScoreType.FailedMove);
        buttonImage.gameObject.SetActive(false);
        queuedButton1.gameObject.SetActive(false);
        queuedButton2.gameObject.SetActive(false);
        queuedButton3.gameObject.SetActive(false);
        currentDanceSequence.Clear();
        for (int i = 0; i < Random.Range(4, 9); i++)
        {
            currentDanceSequence.Add(possibleDanceMoves[Random.Range(0, possibleDanceMoves.Count)]);
        }
        Instantiate(failEffect, PlayerController.playerPosition, Quaternion.identity);
        placeInCombo = 0;
        buttonImage.gameObject.SetActive(false);
        Debug.Log("Combo Broken!");
        placeInCombo = 0;
        danceInProgress = false;
        PlayerController.spriteRenderer.sprite = PlayerController.defaultSprite; // return sprite
        return DanceStatus.Incorrect;
    }

    /// <summary>
    /// Changes the displayed key to be the correct next key
    /// </summary>
    void DisplayNextAction()
    {
        buttonImage.sprite = currentDanceSequence[placeInCombo].keyboardSpriteSequence;
        int danceLength = currentDanceSequence.Count;
        
        print (danceLength + ", " + placeInCombo);
        if (placeInCombo + 1 >= danceLength)
        {
            queuedButton1.gameObject.SetActive(false);
        }
        else
        {
            queuedButton1.sprite = currentDanceSequence[placeInCombo + 1].keyboardSpriteSequence;
        }
        
        if (placeInCombo + 2 >= danceLength)
        {
            queuedButton2.gameObject.SetActive(false);
        }  else
        {
            queuedButton2.sprite = currentDanceSequence[placeInCombo + 2].keyboardSpriteSequence;
        }
        
        if (placeInCombo + 3 >= danceLength)
        {
            queuedButton3.gameObject.SetActive(false);
        }  else  {
            queuedButton3.sprite = currentDanceSequence[placeInCombo + 3].keyboardSpriteSequence;
        }
    }
}