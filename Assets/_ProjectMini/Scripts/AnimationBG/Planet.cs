using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Boss.AnimationBG
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private Transform _planet = null;
        [SerializeField] private float _duration = 20f;
        [SerializeField] private Ease _ease = Ease.Linear;
        [SerializeField] private RotateMode _rotateMode = RotateMode.FastBeyond360;
        [SerializeField] private Vector3 _endValue;
        private void Start()
        {
            _planet.DORotate(_endValue,_duration,_rotateMode).SetEase(_ease).SetLoops(-1);
        }
    }
}
