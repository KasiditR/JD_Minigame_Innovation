using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using Boss.Dotween;
namespace Boss.Billboard
{
    public class ClickEvent : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private GameObject _outLine = null;
        [SerializeField] private Image _outLineIMG = null;
        [SerializeField] private Button _button = null;
        [SerializeField] private GameObject _closeLine = null;

        [HorizontalLine]

        [SerializeField] private UnityEvent _onScale;
        private BillboardItems _billboardRef;
        public BillboardItems BillboardRef
        {
            get => _billboardRef;
            set => _billboardRef = value;
        }
        public event Action onClickCorrect;
        public event Action onClickInCorrect;
        
        public void DoEvent()
        {
            if (BillboardRef.coreValue != CoreValue.Innovation)
            {
                Debug.Log("This Not Innovation");
                _closeLine.SetActive(true);
                var incorrect = this.gameObject.GetComponent<MoveBillboard>();
                incorrect?.RunFlee();
                onClickInCorrect?.Invoke();
                return;
            }
            Debug.Log("This Innovation");
            _button.enabled = false;
            _outLine.SetActive(false);
            _onScale?.Invoke();
            onClickCorrect?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _outLineIMG.DOFade(1,0);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _outLineIMG.DOFade(0,0);
        }
    }
}
