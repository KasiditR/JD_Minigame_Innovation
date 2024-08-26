using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Boss.AnimationBG
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private Transform _car = null;
        [SerializeField] private float _endValueX = 5f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Ease _ease = Ease.Linear;
        private void Start()
        {
            _car.DOLocalMoveX(_endValueX,_duration,false).SetEase(_ease).SetLoops(-1);
        }
    }
}
