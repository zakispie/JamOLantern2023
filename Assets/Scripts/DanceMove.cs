using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Dance Move")]
    public class DanceMove : ScriptableObject
    {
        [Tooltip("Dance Actions That Represent A Sequence of Dance Actions")]
        [SerializeField] public DanceAction comboSequence;
    
        [Tooltip("Sprites to Represent the Keyboard Buttons for Dance Actions (should be same order as comboSequence)")]
        [SerializeField] public Sprite keyboardSpriteSequence;
    
        [Tooltip("Sprites to Represent Player's Dance Moves for Dance Actions (should be same order as comboSequence)")]
        [SerializeField] public Sprite playerSpriteSequence;
    }
}