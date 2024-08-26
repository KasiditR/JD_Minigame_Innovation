using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Boss.Dotween
{
    public class ButtonGameOver : MonoBehaviour,IPointerExitHandler,IPointerEnterHandler
    {
        [SerializeField] private RectTransform _button = null;
        [SerializeField] private Vector2 _sizeValue = Vector2.zero;
        [SerializeField] private float _duration = 0f;
        [SerializeField] private Ease _ease = Ease.Linear;
        private Vector2 _startValue = Vector2.zero;
        private void Start()
        {
            _startValue = _button.sizeDelta;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _button.DOSizeDelta(_sizeValue,_duration).SetEase(_ease);      
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _button.DOSizeDelta(_startValue,_duration).SetEase(_ease);      
        }

    }
}