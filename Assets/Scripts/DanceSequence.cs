using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dance Sequence")]
public class DanceSequence : ScriptableObject
{
    [Tooltip("Dance Actions That Represent A Sequence of Dance Actions")]
    [SerializeField] public List<DanceAction> comboSequence;
    
    [Tooltip("Sprites to Represent the Keyboard Buttons for Dance Actions (should be same order as comboSequence)")]
    [SerializeField] public List<Sprite> keyboardSpriteSequence;
    
    [Tooltip("Sprites to Represent Player's Dance Moves for Dance Actions (should be same order as comboSequence)")]
    [SerializeField] public List<Sprite> playerSpriteSequence;
    
    [Tooltip("Will be instantiated when a dance is succeeded")]
    [SerializeField] public GameObject successEffect;
    
    [Tooltip("Will be instantiated when a dance is failed")]
    [SerializeField] public GameObject failEffect;
}