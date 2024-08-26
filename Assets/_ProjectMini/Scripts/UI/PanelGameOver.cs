using System.Collections;
using System.Collections.Generic;
using Dtawan;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Boss.UI
{
    public class PanelGameOver : MonoBehaviour
    {
        [SerializeField] private GameObject _endGamePanel = null;
        [SerializeField] private GameObject _restartMainMenuButton = null;
        [SerializeField] private GameObject _nextGameButton = null;
        [SerializeField] private TMP_Text _titleText = null;
        [SerializeField] private TMP_Text _conditionOneText = null;
        [SerializeField] private TMP_Text _conditionTwoText = null;

        [HorizontalLine]
        [SerializeField] private Image _conditionOneImage = null;
        [SerializeField] private Image _conditionTwoImage = null;
        [SerializeField] private Sprite _crossLineSprite = null;
        [SerializeField] private Sprite _checkLineSprite = null;
        
        [HorizontalLine]
        [SerializeField] private UnityEvent _onOpenPanel;
        private KeyBehaviorAnimation _keyBehaviorAnimation;

        public void OpenPanel()
        {
            _endGamePanel.SetActive(true);
            _onOpenPanel?.Invoke();
        }

        public void OpenButton(bool isSuccess)
        {
            if (!isSuccess)
            {
                _titleText.text = "Game Over";
                _titleText.fontSize = 28;
                _conditionOneText.text = "Score less than 80%.";
                _conditionTwoText.text = "Use more than 3 min.";
                _conditionOneImage.sprite = _crossLineSprite;
                _conditionTwoImage.sprite = _crossLineSprite;
                _restartMainMenuButton.SetActive(true);
                _nextGameButton.SetActive(false);
            }
            else
            {
                _titleText.text = "Congratulations";
                _titleText.fontSize = 20;
                _conditionOneText.text = "Score more than 80%.";
                _conditionTwoText.text = "Use less than 3 min.";
                _conditionOneImage.sprite = _checkLineSprite;
                _conditionTwoImage.sprite = _checkLineSprite;
                _restartMainMenuButton.SetActive(false);
                _nextGameButton.SetActive(true);
            }
        }
    }
}
