using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Boss.UI
{
    public class MatchingPopup : MonoBehaviour
    {
        [Header("Popup")]
        [SerializeField] private RectTransform _popup = null;
        [SerializeField] private float _durationPopup = 0.55f;
        [SerializeField] private float _durationPopupClose = 2f; 
        [SerializeField] private float _delayTimePopup = 0.8f;
        [SerializeField] private Vector3 _endPopup = Vector3.zero;
        [SerializeField] private Ease _easePopup = Ease.InOutBack;
        private Vector3 _startPopup = Vector3.zero;
        private Tween _tweenPopup = null;

        private void OnEnable() 
        {
            _startPopup = _popup.transform.localPosition;
            _tweenPopup?.Kill();
            PlayPopup();
        }
        private void PlayPopup()
        {
            _popup.DOLocalMove(_endPopup,_durationPopup,false).SetEase(_easePopup).OnComplete(() =>
            {
                _tweenPopup =  _popup.DOLocalMove(_startPopup,_durationPopupClose,false).SetEase(_easePopup).SetDelay(_delayTimePopup).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            });        
        }
        private void OnDisable()
        {
            transform.localPosition = _startPopup;
        }
    }
}
