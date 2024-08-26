using TMPro;
using UnityEngine;

namespace Dtawan.MatchingItem
{
    public enum ItemTypes
    {
        BetterResult,
        ContinuousLearning,
        Creativity,
        Don_tBlameMistake,
        EmbraceChange,
        FindNewSolutions,
        Growth,
        Improvement,
        TheLimitationIntoPossibilities,
        TryNewWays
    }
    
    [CreateAssetMenu(fileName = "New item", menuName = "Create Item")]
    public sealed class ItemSO : ScriptableObject
    {
        public int CountCreate;
        public Sprite ImageFront => imageFront;
        public ItemTypes Type => type;
        public string Text => text;
        
        [SerializeField] private Sprite imageFront;
        [SerializeField] private ItemTypes type;
        [SerializeField] [TextArea] private string text;
        
        private void OnEnable()
        {
            CountCreate = 0;
        }
    }
}