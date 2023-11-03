using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Dancers : MonoBehaviour
    {
        [SerializeField] public Sprite IdleSprite;
        [SerializeField] private Sprite LeftSprite;
        [SerializeField] private Sprite RightSprite;
        [SerializeField] private Sprite UpSprite;
        [SerializeField] private Sprite DownSprite;
        [SerializeField] private Sprite SadSprite;
        [SerializeField] private List<Sprite> inLineSprites;
        private bool _changedSprite;
        private LevelManager _levelManager;
        [HideInInspector] public bool _inLine;
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
                    if (_inLine)
                    {
                        spriteRenderer.sprite = inLineSprites[Random.Range(0, inLineSprites.Count)];
                    }
                    else
                    {
                        spriteRenderer.sprite = IdleSprite;
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_inLine && other.CompareTag("Player"))
            {
                _levelManager.AddToLine(gameObject);
                spriteRenderer.sprite = inLineSprites[Random.Range(0, inLineSprites.Count)];
                _inLine = true;
            }
        }
    }
}