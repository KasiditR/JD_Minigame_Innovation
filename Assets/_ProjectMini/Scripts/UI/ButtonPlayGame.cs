using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Boss.Billboard
{
    public class ButtonPlayGame : MonoBehaviour
    {
        [Header("Panel")]
        [HorizontalLine]
        [SerializeField] private float _timeToPlay = 1f;
        [SerializeField] private RectTransform _panel = null;
        [SerializeField] private float _endValuePanel = -400f;
        [SerializeField] private float _durationPanel = 0.5f;
        [SerializeField] private Ease _easePanel = Ease.Linear;

        [Header("Event OnClick Play Button")]
        [HorizontalLine]
        [SerializeField] private UnityEvent _onPlay;
        
        public void DoEvent()
        {
            _panel.DOLocalMoveY(_endValuePanel,_durationPanel,false).SetEase(_easePanel).OnComplete(() => 
            {
                StartCoroutine(StartPlay(_timeToPlay));
            });
        }

        private IEnumerator StartPlay(float timePlay)
        {
            yield return new WaitForSeconds(timePlay);
            _panel.gameObject.SetActive(false);
            _onPlay?.Invoke();
        }
    }
}
