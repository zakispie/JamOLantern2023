using System;
using System.Collections.Generic;
using UnityEngine;
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
        
        private PlayerController _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            //tilemap.CompressBounds();
            if (wantRandomSpawning)
            {
                List<Vector3Int> alreadyUsed = new List<Vector3Int>();
                for (int i = 0; i < howManyDancersWanted; i++)
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
                    Instantiate(dancers[Random.Range(0, dancers.Count)], danceWorldPos, Quaternion.identity);

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
    }
}