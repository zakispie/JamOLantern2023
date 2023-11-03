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
        [SerializeField] private Sprite SadSprite;
        private bool _changedSprite;
        private LevelManager _levelManager;
        private bool _inLine;
        private float _spriteChangeCounter;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
            _inLine = false;
            _changedSprite = false;
            _spriteChangeCounter = 0f;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void sadSprite()
        {
            spriteRenderer.sprite = SadSprite;
            _changedSprite = true;
            _spriteChangeCounter = 0;
        }
        
        public void changeSprite(DanceAction action)
        {
            //TODO: Change to Idle Sprite
            if (action == DanceAction.UpDance)
            {
                spriteRenderer.sprite = UpSprite;
            } 
            else if (action == DanceAction.DownDance)
            {
                spriteRenderer.sprite = DownSprite;
            }
            else if (action == DanceAction.LeftDance)
            {
                spriteRenderer.sprite = LeftSprite;
            }
            else if (action == DanceAction.RightDance)
            {
                spriteRenderer.sprite = RightSprite;
            } 
            else if (action == DanceAction.BogusDance)
            {
                spriteRenderer.sprite = SadSprite;
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
                    spriteRenderer.sprite = IdleSprite;
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