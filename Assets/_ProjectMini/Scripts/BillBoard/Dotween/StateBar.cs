using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Boss.Dotween
{
    public class StateBar : MonoBehaviour
    {
        [SerializeField] private Image _bar = null;
        [SerializeField] private float _durationFillAmount = 2.5f;
        [SerializeField] private float _endFillAmount = 0.25f;
        [SerializeField] private Ease _easeFillAmount = Ease.Linear;

        [HorizontalLine]

        [SerializeField] private Vector3 _endScale = Vector3.zero;
        [SerializeField] private float _durationScaleClose = 0.1f;
        [SerializeField] private float _durationScaleOpen = 0.5f;
        [SerializeField] private Ease _easeScale = Ease.Linear;

        [HorizontalLine]

        [SerializeField] private Sprite _imgCurrentState = null;
        [SerializeField] private Sprite _imgCompleteState = null;
        [SerializeField] private Image[] _imgState;
        private int _currentIndex;

        public void Bar()
        {
            if (_bar.fillAmount == 1)
            {
                return;
            }
            ChangeImageComplete();
            _bar.DOFillAmount(_endFillAmount,_durationFillAmount).SetRelative(true).SetEase(_easeFillAmount).OnComplete(() =>
            {
                ChangeImageCurrent();
            });
        }
        private void ChangeImageCurrent()
        {
            _currentIndex++;
            _imgState[_currentIndex].sprite = _imgCurrentState;
            _imgState[_currentIndex].rectTransform.DOScale(_endScale, _durationScaleOpen).SetEase(_easeScale);
        }

        private void ChangeImageComplete()
        {
            _imgState[_currentIndex].sprite = _imgCompleteState;
            _imgState[_currentIndex].rectTransform.DOScale(Vector3.one, _durationScaleClose).SetEase(_easeScale);
        }
    }
}
