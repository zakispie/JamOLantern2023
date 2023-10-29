using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

/// <summary>
/// A MonoBehavior to control the physics and movement of the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Tooltip("Player Movement Speed")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private Tilemap tilemap;
    
    private Vector3Int nextCellPosition;
    private Vector3 prevPosition;

    // Represents this PlayerController
    private static PlayerController instance;
    
    // Movement Input as a Vector2
    private Vector2 movementInput = Vector2.zero;
    
    // Player Rigidbody
    private static Rigidbody2D rb;
    
    // Player Sprite Renderer
    public static SpriteRenderer spriteRenderer;
    
    // Player Animator
    public static Animator animator;

    // Player position
    public static Vector2 playerPosition => instance.transform.position;

    // Dance minigame component
    private DanceMinigame danceMinigame;
    
    // List of Dancers
    private List<GameObject> dancers = new List<GameObject>();

    /// <summary>
    /// Assigns variables
    /// </summary>
    private void Awake()
    {
        instance = this;
        rb = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer  = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        var danceController = GameObject.FindGameObjectWithTag("DanceController");
        if (danceController != null)
        {
            danceMinigame = GameObject.FindGameObjectWithTag("DanceController").GetComponent<DanceButtonMatch>();
        }
    }

    private void Start()
    {
        nextCellPosition = tilemap.WorldToCell(transform.position);
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

    public void AddToLine(GameObject dancer)
    {
        dancer.transform.position = new Vector3(prevPosition.x, prevPosition.y, -1);
        prevPosition = dancer.transform.position;
        dancers.Add(dancer);
    }
    
    private void MoveAround()
    {
        prevPosition = transform.position;
        nextCellPosition += new Vector3Int((int)movementInput.x, (int)movementInput.y, 0);
        if (tilemap.HasTile(nextCellPosition))
        {
            Vector3 nextCellWorldPosition = tilemap.GetCellCenterWorld(nextCellPosition);
            Vector3 newPosition = new Vector3(nextCellWorldPosition.x, nextCellWorldPosition.y, -1);
            transform.position = newPosition;
            //Debug.Log(dancers.Count.ToString());
            if (transform.position != prevPosition)
            {
                foreach (GameObject d in dancers)
                {
                    Vector3 newLinePos = new Vector3(prevPosition.x, prevPosition.y, -1);
                    prevPosition = d.transform.position;
                    d.transform.position = newLinePos;
                }
            }
        }
        else
        {
            Debug.Log("Game Over!");
        }
    }
    
    /// <summary>
    /// Updates the position of the rigidbody based on the current movement input
    /// </summary>
    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movementInput * (Time.fixedDeltaTime * movementSpeed));
    }
    
    #region Receive Inputs
    /// <summary>
    /// Assigns movement input from Unity Input System to movementInput variable
    /// Called whenever "Movement" input is detected
    /// </summary>
    /// <param name="inputValue"> Received input value from Unity Input System </param>
    private void OnMovement(InputValue inputValue)
    {
        //RelayAction(DanceAction.BogusDance); // fail dance combo if try to walk during it
        Vector2 input  = inputValue.Get<Vector2>();
        movementInput = input;
        // flip sprite based on movement
        if (input.x != 0) spriteRenderer.flipX = input.x > 0;
        MoveAround();
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
