using System;
using TMPro;
using UnityEngine;

namespace Dtawan.MatchingItem
{
    public enum ScenesGame
    {
        CandyCrush,
        Matching,
        Innovation
    }
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private ScoreManagerSO scoreManager;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private ScenesGame scenesGame;

        private void OnEnable()
        {
            scoreText.text = scoreManager.ShowScore(scenesGame);
        }
    }
}