using UnityEngine;

/// <summary>
/// MonoBehavior to destroy the attached gameobject after a given amount of time
/// </summary>
public class DestroyAfterSeconds : MonoBehaviour
{
    [Tooltip("How many seconds after creation should this gameobject be destroyed?")]
    [SerializeField] private int secondsUntilDestroy;
    
    /// <summary>
    /// Destroy this gameobject after the given amount of seconds
    /// </summary>
    void Start()
    {
        if (secondsUntilDestroy != -1)
        {
            Destroy(this.gameObject, secondsUntilDestroy);
        }
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
