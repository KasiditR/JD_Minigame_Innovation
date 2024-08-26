using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Boss.Dotween
{
    public class BillboardPopup : MonoBehaviour
    {
        [SerializeField] private MoveBillboard _moveBillboard;
        [SerializeField] private Vector3 _endValue = Vector3.zero;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            _canvasGroup.DOFade(1,0.5f);
        }

        public void Scale()
        {
            this.transform.DOLocalMove(Vector3.zero,0.5f);
            _moveBillboard.IsMove = false;
            this.transform.DOScale(_endValue,_duration).OnComplete(() => 
            {
                _canvasGroup.DOFade(0,_duration).SetDelay(2.5f);
                Destroy(this.gameObject,3f);
            });
        }
    }
}
