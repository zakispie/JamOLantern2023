using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A MonoBehavior to control the physics and movement of the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Tooltip("Player Movement Speed")]
    [SerializeField] private float movementSpeed;

    // Movement Input as a Vector2
    private Vector2 movementInput = Vector2.zero;
    
    // Player Rigidbody
    private Rigidbody2D rb;
    
    // Player Sprite Renderer
    private SpriteRenderer spriteRenderer;

    // Dance minigame component
    private DanceMinigame danceMinigame;

    /// <summary>
    /// Assigns variables
    /// </summary>
    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer  = GetComponentInChildren<SpriteRenderer>();
        danceMinigame = GameObject.FindGameObjectWithTag("DanceController").GetComponent<DanceButtonMatch>();
    }
    
    /// <summary>
    /// Given a DanceAction, relays the action to attempt to perform a dance. Then handles necessary logic based on the returned DanceStatus
    /// </summary>
    /// <param name="action"> The DanceAction to perform </param>
    void RelayAction(DanceAction action)
    {
        var result = danceMinigame.PerformDance(action);
        switch (result) {
            case DanceStatus.Correct:
                // Do any "on correct action" feedback here.
                break;
            case DanceStatus.Incorrect:
                // Do any "on action incorrect" feedback here.
                break;
        }
    }
    
    /// <summary>
    /// Updates the position of the rigidbody based on the current movement input
    /// </summary>
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * (Time.fixedDeltaTime * movementSpeed));
    }
    
    #region Receive Inputs
    /// <summary>
    /// Assigns movement input from Unity Input System to movementInput variable
    /// Called whenever "Movement" input is detected
    /// </summary>
    /// <param name="inputValue"> Received input value from Unity Input System </param>
    private void OnMovement(InputValue inputValue)
    {
        Vector2 input  = inputValue.Get<Vector2>();
        movementInput = input;
        // flip sprite based on movement
        if (input.x != 0) spriteRenderer.flipX = input.x > 0;
    }

    /// <summary>
    /// Handles when up arrow key is pressed
    /// </summary>
    /// <param name="inputValue"> Received input value from Unity Input System </param>
    private void OnDanceUp(InputValue inputValue)
    {
        RelayAction(DanceAction.UpDance);
    }
    
    /// <summary>
    ///  Handles when down arrow key is pressed
    ///  </summary>
    ///  <param name="inputValue"> Received input value from Unity Input System </param>
    private void OnDanceDown(InputValue inputValue)
    {
        RelayAction(DanceAction.DownDance);
    }

    /// <summary>
    ///  Handles when left arrow key is pressed
    ///  </summary>
    ///  <param name="inputValue"> Received input value from Unity Input System </param>
    private void OnDanceLeft(InputValue inputValue)
    {
        RelayAction(DanceAction.LeftDance);
    }

    /// <summary>
    ///  Handles when right arrow key is pressed
    ///  </summary>
    ///  <param name="inputValue"> Received input value from Unity Input System </param>
    private void OnDanceRight(InputValue inputValue)
    {
        RelayAction(DanceAction.RightDance);
    }
    #endregion
}
