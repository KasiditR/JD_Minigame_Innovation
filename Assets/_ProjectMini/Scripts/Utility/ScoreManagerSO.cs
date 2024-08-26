using System;
using UnityEngine;

namespace Dtawan.MatchingItem
{
    [CreateAssetMenu(fileName = "New ScoreManager", menuName = "Create ScoreManager")]
    public class ScoreManagerSO : ScriptableObject
    {
        public float scoreCandyCrush;
        public float scoreInnovation;
        public float scoreMatching;

        public string ShowScore(ScenesGame scenesGame)
        {
            return scenesGame switch
            {
                ScenesGame.Innovation => scoreInnovation.ToString(),
                ScenesGame.Matching => scoreMatching.ToString(),
                ScenesGame.CandyCrush => scoreCandyCrush.ToString(),
                _ => string.Empty
            };
        }
    }
}