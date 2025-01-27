using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Boss.UI
{
    public class OpenTutorial : MonoBehaviour
    {
        [SerializeField] private RectTransform _tutorialPanel = null;
        [SerializeField] private GameObject _bgScreen = null;
        [SerializeField] private float _endValueMoveY = -400f;
        [SerializeField] private float _durationMoveY = 0.5f;
        [SerializeField] private Ease _easeMoveY = Ease.Linear;

        public void TutorialTrue()
        {
            this.gameObject.transform.DOLocalMoveY(_endValueMoveY,_durationMoveY,false).OnPlay(() =>
            {
                BGScreenOff();
            }).SetEase(_easeMoveY).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
            _tutorialPanel.gameObject.SetActive(true);
        }
        public void BGScreenOff()
        {
            _bgScreen.SetActive(false);
        }
    }
}
