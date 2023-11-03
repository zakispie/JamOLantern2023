using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Door : MonoBehaviour
{
    private LevelManager _levelManager;
    
    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _levelManager.ExitDoor();
        }
    }
}
