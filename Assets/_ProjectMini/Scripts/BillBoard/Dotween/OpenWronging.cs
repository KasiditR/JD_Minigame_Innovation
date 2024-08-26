using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Boss.Dotween
{
    public class OpenWronging : MonoBehaviour
    {   
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _slider;
        [SerializeField] private float _durationStart;
        [SerializeField] private float _durationEnd;
        [SerializeField] private float _delayEnd;
        [SerializeField] private Ease _ease = Ease.Linear;
        private Tween _tween;
        
        [ContextMenu("PlaySlider")]
        public void StartPlay()
        {
            this.gameObject.SetActive(true);
            _canvasGroup.alpha = 1f;
            _slider.DOValue(1f, _durationStart, false).SetEase(_ease).OnComplete(() =>
            {
                EndPlay();
            });
        }

        private void EndPlay()
        {
            _tween.Kill();

            _tween = _slider.DOValue(0f, _durationEnd, false).SetEase(_ease).SetDelay(_delayEnd).OnStart(() => 
            {
                _canvasGroup.DOFade(0f, _durationEnd).SetEase(_ease);
            }).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        }
    }
}