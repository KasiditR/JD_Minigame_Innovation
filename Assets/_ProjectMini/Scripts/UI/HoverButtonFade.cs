using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
namespace Boss.UI
{
    public class HoverButtonFade : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        [SerializeField] private Image _imageHover = null;
        [SerializeField] private Image _imageStoke = null;
        [SerializeField] private TMP_Text _textButton = null;

        [HorizontalLine]
        [SerializeField] private Color _colorRed = Color.red;
        [SerializeField] private Color _colorWhite = Color.white;

        [HorizontalLine]
        [SerializeField] private float _durationHover = 0.1f;
        [SerializeField] private Ease _easeHover = Ease.Linear;
        private bool _isClick = false;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) 
            {
                return;
            }
            _isClick = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isClick)
            {
                return;
            }
            _imageHover.DOColor(_colorRed,_durationHover).SetEase(_easeHover);
            _imageStoke.DOColor(_colorWhite,_durationHover).SetEase(_easeHover);
            _textButton.DOColor(_colorWhite,_durationHover).SetEase(_easeHover);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isClick)
            {
                return;
            }
            _imageHover.DOColor(_colorWhite,_durationHover).SetEase(_easeHover);
            _imageStoke.DOColor(_colorRed,_durationHover).SetEase(_easeHover);
            _textButton.DOColor(_colorRed,_durationHover).SetEase(_easeHover);
        }
    }
}
