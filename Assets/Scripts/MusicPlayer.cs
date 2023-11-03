using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Tooltip("Tolerance for being off (in ms)")]
    [SerializeField] private int tolerance;

    [Tooltip("Offset from the timer for the beat (in ms)")] 
    [SerializeField] private int beatOffset;

    public static Stopwatch testStopwatch;
    
    private Stopwatch _stopwatch;

    public static bool readyForDanceEvent;

    private DanceButtonMatch danceButtonMatch;

    void Start()
    {
        danceButtonMatch = GameObject.FindGameObjectWithTag("DanceController").GetComponent<DanceButtonMatch>();
        _stopwatch = new Stopwatch();
        testStopwatch = new Stopwatch();
        _stopwatch.Start();
        testStopwatch.Start();
        PlayerController.playerAudioSource.Play();
    }

    void Update()
    {
        //print(readyForDanceEvent + ": " + _stopwatch.ElapsedMilliseconds / 1000);
        if (readyForDanceEvent)
        {
            long milliseconds = _stopwatch.ElapsedMilliseconds + beatOffset;
            if (milliseconds % 4000 < tolerance || milliseconds % 4000 > 4000 - tolerance)
            {
                print("Dance event sent at: " + testStopwatch.ElapsedMilliseconds);
                // we are on a solid second within tolerance, so initialize the event
                readyForDanceEvent = false;
                StartCoroutine(danceButtonMatch.StartDanceEvent());
            }
        }
    }
}