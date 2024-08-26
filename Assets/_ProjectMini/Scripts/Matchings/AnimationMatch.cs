using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Dtawan.MatchingItem
{
    public class AnimationMatch : MonoBehaviour
    {
        [Header("Scriptable Item Manager")]
        [Tooltip("Assign ScriptableObject")]
        [SerializeField] private MatchingManagerSO matchingManager = null;

        [Header("Reduce Time")]
        [SerializeField] private RectTransform _reduce = null;
        [SerializeField] private TMP_Text _reduceText = null;

        /*[Tooltip("Duration Reduce Play")]
        [SerializeField] private float _durationReduce = 2f;*/

        [Tooltip("End Value is Relative")]
        [SerializeField] private float _endReduceValue = 70f;
        private Vector3 _startPosReduce = Vector3.zero;

        [Header("Item Select Anim")]
        [SerializeField] private Transform _startPosItemSelected0 = null;
        [SerializeField] private Transform _startPosItemSelected1 = null;
        [SerializeField] private float _durationItemSelect = 0.55f;
        [SerializeField] private float _durationItemSelectClose = 2f;

        [Tooltip("Offset When item select move to upper (endItemSelectValue + (vector.y * offset))")]
        [SerializeField] private float _offset = 10f;
        [SerializeField] private Vector3 _endItemSelectValue0 = Vector3.zero; //!Don't change vector3 (-1.35,0,-5)
        [SerializeField] private Vector3 _endItemSelectValue1 = Vector3.zero; //!Don't change vector3 (-1.35,0,-5)
        [SerializeField] private Ease _easeItemSelect = Ease.InOutBack; //!Don't change
        
        [Header("Close Anim")]
        [SerializeField] private SpriteRenderer _closeLine0 = null;
        [SerializeField] private SpriteRenderer _closeLine1 = null;
        [SerializeField] private float _durationCloseLine = 0.5f;
        [SerializeField] private int _loopCloseLine = 2;

        [SerializeField] private GameObject _popup = null;
        private Tween _tweenReduce = null;

        private void OnEnable()
        {
            _startPosReduce = _reduce.position;
        }
        public IEnumerator PlayAnimMatch()
        {
            AnimationPopup();
            yield return StartCoroutine(AnimationMatching());
        }
        private IEnumerator AnimationMatching()
        {
            matchingManager.ItemSelected[0].transform.DOMove(_endItemSelectValue0, _durationItemSelect, false).SetEase(_easeItemSelect);
            matchingManager.ItemSelected[1].transform.DOMove(_endItemSelectValue1, _durationItemSelect, false).SetEase(_easeItemSelect);

            yield return new WaitForSeconds(1.5f);//! Don't change
            
            matchingManager.ItemSelected[0].transform
                .DOMove(_endItemSelectValue0 + (Vector3.up * _offset), _durationItemSelectClose, false)
                .SetEase(_easeItemSelect)
                .OnComplete(() => 
            { 
                matchingManager.ItemSelected[0].transform.position = _startPosItemSelected0.position;
            });

            matchingManager.ItemSelected[1].transform
                .DOMove(_endItemSelectValue1 + (Vector3.up * _offset), _durationItemSelectClose, false)
                .SetEase(_easeItemSelect)
                .OnComplete(() => 
            {
                matchingManager.ItemSelected[1].transform.position = _startPosItemSelected1.position;
            });
        }
        public IEnumerator CloseItem()
        {
            AnimationNotMatch();
            yield return new WaitForSeconds(0.5f);
            matchingManager.FirstItem.RotateClose();
            matchingManager.LastItem.RotateClose();
        }
        private void AnimationPopup()
        {
            _popup.SetActive(true);
        }
        private void AnimationNotMatch()
        {
            _closeLine0.DOFade(1f, _durationCloseLine).SetLoops(_loopCloseLine).OnComplete(() => _closeLine0.DOFade(0, 0));
            _closeLine1.DOFade(1f, _durationCloseLine).SetLoops(_loopCloseLine).OnComplete(() => _closeLine1.DOFade(0, 0));

            matchingManager.ItemSelected[0].transform.DOShakePosition(_durationItemSelect, 1, 10, 90, false, true);
            matchingManager.ItemSelected[1].transform.DOShakePosition(_durationItemSelect, 1, 10, 90, false, true);
        }
        public void ReduceText(float reduceTime, float durationRe)
        {
            _reduceText.text = $"-{(int)reduceTime}";
            _tweenReduce.Kill();
            _tweenReduce = _reduce.DOMoveY(_endReduceValue, durationRe, false).SetRelative(true).OnPlay(() =>
            {
                _reduceText.DOFade(1f, 0).OnPlay(() =>
                {
                    _reduceText.DOFade(0f, durationRe);
                });
            }).OnComplete(() =>
            {
                _reduce.position = _startPosReduce;
            });
        }
    }
}
