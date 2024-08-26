using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Boss.UI
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private GameObject m_tutorialPanel = null;
        [SerializeField] private float m_endValueMoveY = 0f;
        [SerializeField] private float m_durationMoveY = 3f;
        [SerializeField] private Ease m_easeMoveY = Ease.Linear;
        private Vector3 _startPos = Vector3.zero;
        private void OnEnable()
        {
            _startPos = this.gameObject.transform.position;
            m_tutorialPanel.transform.DOLocalMoveY(m_endValueMoveY,m_durationMoveY,false).SetEase(m_easeMoveY);
        }
        private void OnDisable() 
        {
            m_tutorialPanel.transform.position = _startPos;
        }
    }
}
