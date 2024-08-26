using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

namespace Dtawan.MatchingItem
{
    public class CountDown : MonoBehaviour
    {
        [SerializeField] private MatchingManagerSO matchingManager;
        [SerializeField] private TextMeshProUGUI _textCount = null;
        [SerializeField] private TextMeshProUGUI _textStage = null;
        [SerializeField] private GameObject _stageBox = null;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private int _count = 3;
        [SerializeField] private Ease _ease = Ease.Linear;
        public UnityEvent onCountDown;

        private int count;

        private void Awake()
        {
            matchingManager.ClearData();
        }

        // private void OnEnable()
        // { 
        //     StartGame();   
        //     Debug.Log("OnEnable CountDown");
        // }

        private void StartGame()
        {
            matchingManager.ResetState();
            count = _count;
            _stageBox.SetActive(true);
            _textStage.text = $"Stage : {matchingManager.state} / 2";
            StartCoroutine(Count());
        }
        
        public void OpenCountDown()
        {
            // this.gameObject.SetActive(true);
            StartGame();
        }

        private IEnumerator Count()
        {
            while (count >= 0)
            {
                _textCount.DOFade(1,_duration).SetEase(_ease).OnComplete(() => _textCount.DOFade(0,0));
                _textCount.text = $"{count}";
                count--;
                yield return new WaitForSeconds(1f);
            }
            
            onCountDown?.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
