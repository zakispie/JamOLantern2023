using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main Menu controller
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    /// <summary>
    /// Opens the play scene
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Load the game scene
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        credits.SetActive(true);
    }
}
