using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Boss.Dotween
{
    public class MoveBillboard : MonoBehaviour
    {
        #region Field
        [SerializeField] private Ease _ease = Ease.Linear;
        [SerializeField] private float _coolDown;
        [SerializeField] private RectTransform _billboard;
        [SerializeField] private RectTransform[] _boosters;
        [SerializeField] private List<Vector3> _targetFlees = new List<Vector3>();
        [SerializeField] private List<Vector3> _targetMoves = new List<Vector3>();
        private Vector3 _getTargetFlee;
        private float _duration;
        private int _currentIndex;
        private bool _isMove = false;
        private Tween _tweenMove = null;
        private Tween _tweenBooster = null;
        private Tween _tweenRun = null;
        private readonly Vector3 offset = new Vector3(500,200,0);
        private const float DURATION_ROTATE = 0.5f;
        private const int DEGREE_ROTATE = 90;
        private const int ZERO_INDEX = 0;
        
        public List<Vector3> TargetMoves { get => _targetMoves; set => _targetMoves = value; }
        public float Duration { get => _duration; set => _duration = value; }
        public bool IsMove 
        { 
            get => _isMove; 
            set 
            {  
                _isMove = value; 
                if (_isMove == true)
                {
                    StartCoroutine(Move());
                }
                else
                {
                    _tweenMove?.Kill();
                    StopAllCoroutines();
                    foreach (Transform item in _boosters)
                    {
                        if (item.rotation != Quaternion.identity)
                        {
                            item.rotation = Quaternion.Euler(0f, 0f, 0f);
                        }
                        Debug.Log("Rotation");
                    }
                }
            }
        }

        #endregion
        public void StopMove()
        {
            _tweenMove.Kill();
            StopCoroutine(Move());
        }

        private void OnDisable() 
        {
            _targetMoves.Clear();
        }
        private void OnDestroy() 
        {
            _tweenMove?.Kill();
            _tweenBooster?.Kill();
            _tweenRun?.Kill();
        }
        private IEnumerator Move()
        {
            BoosterLookAt(_targetMoves[_currentIndex]);
            yield return new WaitForSeconds(_coolDown);
            _tweenMove = _billboard.DOMove(_targetMoves[_currentIndex] + offset,Duration,false).SetEase(_ease).OnComplete(() => 
            {
                _currentIndex++;
                if (_currentIndex > _targetMoves.Count - 1)
                {
                    _currentIndex = ZERO_INDEX;
                }
                StartCoroutine(Move());
            });
        }
        private void BoosterLookAt(Vector3 target)
        {
            foreach (Transform item in _boosters)
            {
                RotateTowards(target ,item);
            }
        }
        private void RotateTowards (Vector3 target, Transform booster)
        {
            Vector3 direction = target - _billboard.position + offset;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _tweenBooster = booster.DORotateQuaternion(Quaternion.AngleAxis(angle - DEGREE_ROTATE, booster.forward), DURATION_ROTATE);
        }
        public void RunAlways()
        {
            _tweenMove?.Kill();
            StopAllCoroutines();

            Vector3 targetRunAlways = Vector3.zero + offset;
            _tweenRun = _billboard.DOMove(targetRunAlways, 0.75f, false).SetEase(_ease).OnPlay(() => 
            {
                BoosterLookAt(targetRunAlways);
                _billboard.DOScale(new Vector3(0.5f,0.5f,0.5f), 0.75f);
            }).OnComplete(() =>
            {
                Destroy(this.gameObject,0.5f);
            });
        }
        public void RunFlee()
        {
            _tweenMove?.Kill();
            StopAllCoroutines();
            Vector3 targetRunAlways = Vector3.zero + offset;
            Vector3 dir = (_billboard.transform.position - targetRunAlways).normalized;
            if (dir.x >= 0 && dir.y >= 0)
            {
                //(+,+)
                _getTargetFlee = _targetFlees[0];
            }
            if (dir.x < 0 && dir.y > 0)
            {
                //(-,+)
                _getTargetFlee = _targetFlees[1];
            }
            if (dir.x < 0 && dir.y < 0)
            {
                //(-,-)
                _getTargetFlee = _targetFlees[2];
            }
            if (dir.x > 0 && dir.y < 0)
            {
                //(+,-)
                _getTargetFlee = _targetFlees[3];
            }
            
            _billboard.DOMove(_getTargetFlee + offset, 0.5f, false).SetDelay(0.5f).SetEase(_ease).OnPlay(() => 
            {
                BoosterLookAt(_getTargetFlee + offset);
            }).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        }
    }
}