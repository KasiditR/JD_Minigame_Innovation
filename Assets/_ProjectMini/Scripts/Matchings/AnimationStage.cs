using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dtawan
{
    public class AnimationStage : MonoBehaviour
    {
        [SerializeField] private float _timeWait = 1f;

        [HorizontalLine]
        [SerializeField] private RectTransform _joyRun = null;
        [SerializeField] private float _durationJoyRun = 0.5f;
        [SerializeField] private Vector3 _endValueJoyRun = Vector3.zero;
        [SerializeField] private Ease _easeJoyRun = Ease.Linear;

        [HorizontalLine]
        [SerializeField] private Image _stageBox = null;
        [SerializeField] private float _durationFillBox = 0.5f;
        [SerializeField] private Ease _easeBox = Ease.Linear;

        private Vector3 _startPosJoy = Vector3.zero;

        private void OnEnable()
        {
            _startPosJoy = _joyRun.position;
            PlayStage();
        }
        private void PlayStage()
        {
            _joyRun.DOLocalMoveX(_endValueJoyRun.x,_durationJoyRun,false).OnPlay(() =>
            {
                StartCoroutine(PlayWaitForJoy());
            }).SetEase(_easeJoyRun).OnComplete(() => 
            {
                Debug.Log("Joy Run Finish");
                _joyRun.position = _startPosJoy;
                this.gameObject.SetActive(false);
            });
        }
        private IEnumerator PlayWaitForJoy()
        {
            yield return new WaitForSeconds(_timeWait);
            _stageBox.DOFillAmount(1,_durationFillBox).SetEase(_easeBox).OnComplete(() =>
            {
                Debug.Log(" FillAmount");
            });
        }
        private void OnDisable() 
        {
            _stageBox.fillAmount = 0;    
        }
    }
}
