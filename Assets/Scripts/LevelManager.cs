using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelManager : MonoBehaviour
    {
        private PlayerController _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        public void AddToLine(GameObject dancer)
        {
            _player.AddToLine(dancer);
        }
    }
}