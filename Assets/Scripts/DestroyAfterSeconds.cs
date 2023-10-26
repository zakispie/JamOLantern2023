using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private int secondsUntilDestroy;
    
    void Start()
    {
        Destroy(this, secondsUntilDestroy);
    }
}
