using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Boss.Dotween
{
    public class CloseLine : MonoBehaviour
    {
        [SerializeField] private Image _closeLine = null;
        [SerializeField] private float _durationCloseLine = 0.5f;
        [SerializeField] private int _loopCloseLine = 2;
        [SerializeField] private Ease _easeCloseLine;

        [ContextMenu("PlayCloseLine")]
        public void PlayCloseLine()
        {
            _closeLine.DOFade(1f, _durationCloseLine).SetLoops(_loopCloseLine).SetEase(_easeCloseLine).OnComplete(() =>  _closeLine.DOFade(0, 0));
        }
    }
}
