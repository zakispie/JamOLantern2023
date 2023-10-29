using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Dancers : MonoBehaviour
    {
        private LevelManager _levelManager;
        private bool _inLine;

        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
            _inLine = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_inLine && other.CompareTag("Player"))
            {
                _levelManager.AddToLine(gameObject);
                _inLine = true;
            }
        }
    }
}