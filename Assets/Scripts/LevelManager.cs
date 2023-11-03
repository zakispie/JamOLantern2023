using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class LevelManager : MonoBehaviour
    {
        
        [SerializeField] List<GameObject> dancers;
        [SerializeField] private bool wantRandomSpawning;
        [SerializeField] private int howManyDancersWanted;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject exitText;
        
        private PlayerController _player;
        private List<Vector3Int> alreadyUsed;
        private Timer timer;

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            timer = FindObjectOfType<Timer>();
            _player.AddManager(this);
            //tilemap.CompressBounds();
            alreadyUsed = new List<Vector3Int>();
            if (wantRandomSpawning)
            {
                for (int i = 0; i < howManyDancersWanted; i++)
                {
                    GameObject dancer = Instantiate(dancers[Random.Range(0, dancers.Count)], 
                        new Vector3(0,0,0), Quaternion.identity);
                    PlaceRandomly(dancer);
                  /*  Vector3Int dancePosition;
                    //GameObject actualDancer = Instantiate(dancers[Random.Range(0, dancers.Count)]);
                    do
                    {
                        // Generate a random position for the food within the tilemap bounds.
                        int x = Random.Range(tilemap.cellBounds.x, tilemap.cellBounds.xMax);
                        int y = Random.Range(tilemap.cellBounds.y, tilemap.cellBounds.yMax);
                        dancePosition = new Vector3Int(x, y, 0);
                    } while (_player.IsOccupying(dancePosition) && IsAlreadyUsed(dancePosition, alreadyUsed));
                    alreadyUsed.Add(dancePosition);
                    Vector3 danceWorldPos = tilemap.GetCellCenterWorld(dancePosition);
                    Instantiate(dancers[Random.Range(0, dancers.Count)], danceWorldPos, Quaternion.identity);*/
                }
            }
        }

        public void AddToLine(GameObject dancer)
        {
            _player.AddToLine(dancer);
        }

        private bool IsAlreadyUsed(Vector3Int potentialCell, List<Vector3Int> alreadyUsed)
        {
            foreach (var cell in alreadyUsed)
            {
                if (potentialCell == cell)
                {
                    return true;
                }
            }

            return false;
        }

        public void PlaceRandomly(GameObject dancer)
        {
            Vector3Int dancePosition;
            //GameObject actualDancer = Instantiate(dancers[Random.Range(0, dancers.Count)]);
            do
            {
                // Generate a random position for the food within the tilemap bounds.
                int x = Random.Range(tilemap.cellBounds.x, tilemap.cellBounds.xMax);
                int y = Random.Range(tilemap.cellBounds.y, tilemap.cellBounds.yMax);
                dancePosition = new Vector3Int(x, y, 0);
            } while (_player.IsOccupying(dancePosition) && IsAlreadyUsed(dancePosition, alreadyUsed));
            alreadyUsed.Add(dancePosition);
            Vector3 danceWorldPos = tilemap.GetCellCenterWorld(dancePosition);
            dancer.transform.position = danceWorldPos;
            dancer.GetComponent<Dancers>()._inLine = false;
            //Instantiate(dancers[Random.Range(0, dancers.Count)], danceWorldPos, Quaternion.identity);
        }
        
        private void OnPause(InputValue inputValue)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }

        public void Resume()
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ExitDoor()
        {
            if (timer.GetSeconds() > 5)
            {
                Time.timeScale = 0;
                exitText.SetActive(true);
                exitText.GetComponentInChildren<TextMeshProUGUI>().text = timer.GetSeconds().ToString() + " seconds";
            }
            else
            {
                RealExit();
            }
            
        }

        public void RealExit()
        {
            Time.timeScale = 1;
            _player.AddScores();
            for (int i = 0; i < timer.GetSeconds(); i++)
            {
                ScoreCounter.AddScore(ScoreCounter.ScoreType.Time);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void NoExit()
        {
            exitText.SetActive(false);
            Time.timeScale = 1;
        }
    }
}