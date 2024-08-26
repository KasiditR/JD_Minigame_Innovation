using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Boss.Dotween
{
    public class ArrowAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _arrow;
        [SerializeField] private float _endValue;
        [SerializeField] private float _durationMove;
        [SerializeField] private Ease _ease;
        [Space]
        [SerializeField] private Image _arrowIMG;
        [SerializeField] private float _durationFade;
        [SerializeField] private int _loopTime;

        private void OnEnable()
        {
            _arrow.DOLocalMoveY(_endValue, _durationMove, false).SetEase(_ease).SetLoops(_loopTime).OnComplete(() =>
            {
                _arrowIMG.DOFade(0, _durationFade);
            });
        }
    }
}