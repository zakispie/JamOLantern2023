using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Dancers : MonoBehaviour
    {
        [SerializeField] private Sprite IdleSprite;
        [SerializeField] private Sprite LeftSprite;
        [SerializeField] private Sprite RightSprite;
        [SerializeField] private Sprite UpSprite;
        [SerializeField] private Sprite DownSprite;
        [SerializeField] private Sprite BogusSprite;
        private bool _changedSprite;
        private LevelManager _levelManager;
        private bool _inLine;
        private float _spriteChangeCounter;

        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
            _inLine = false;
            _changedSprite = false;
            _spriteChangeCounter = 0f;
        }
        
        public void changeSprite(DanceAction action)
        {
            //TODO: Change to Idle Sprite
            if (action == DanceAction.UpDance)
            {
                
            } 
            else if (action == DanceAction.DownDance)
            {
                
            }
            else if (action == DanceAction.LeftDance)
            {
                
            }
            else if (action == DanceAction.RightDance)
            {
                
            } 
            else if (action == DanceAction.BogusDance)
            {

            }

            _changedSprite = true;
            _spriteChangeCounter = 0f;
            
        }

        private void Update()
        {
            if (_changedSprite)
            {
                _spriteChangeCounter += Time.deltaTime;
                if (_spriteChangeCounter >= 1)
                {
                    _changedSprite = false;
                    _spriteChangeCounter = 0f;
                    //TODO: Change to Idle Sprite
                }
            }
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