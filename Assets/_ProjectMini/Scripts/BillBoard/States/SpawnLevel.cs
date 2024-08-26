using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Boss.Billboard;
using Boss.Dotween;
using TMPro;

namespace Boss.State
{
    public enum Level
    {
        One,
        Two,
        Three,
        Four,
        Five
    }
    public class SpawnLevel : MonoBehaviour
    {
        [SerializeField] private Level _level = Level.One;
        [SerializeField] private bool _isEnglish = false;

        [HorizontalLine]
        [SerializeField] private OpenWronging _openWronging = null;
        [SerializeField] private TMP_Text _textCount = null;
        [SerializeField] private StateBar _stateBar = null;
        [SerializeField] private GameObject _spawnArea = null;
        [SerializeField] private Vector3 _size;
        [SerializeField] private UnityEvent _onEndGame;

        [HorizontalLine]
        [SerializeField] private List<GameObject> _childArea;
        [SerializeField] private BillboardItems[] _levelItemsOnes = null;
        [SerializeField] private BillboardItems[] _levelItemsTwos = null;
        [SerializeField] private BillboardItems[] _levelItemsThrees = null;
        [SerializeField] private BillboardItems[] _levelItemsFours = null;
        [SerializeField] private BillboardItems[] _levelItemsFives = null;
        private int _numberEnum = 0;
        private int _numWronging = 0;
        private bool _isEndGame = false;


        public void Spawn() 
        {
            SpawnBillBoard();    
        }

        private void ClickInCorrect()
        {
            StartCoroutine(OpenWrong());
        }
        private void ClickCorrect()
        {
            StartCoroutine(WhenClick());
        }
        private IEnumerator OpenWrong()
        {
            _numWronging++;
            _textCount.text = $"{_numWronging}";
            _openWronging.StartPlay();
            yield return new WaitForSeconds(3f);
        }
        private IEnumerator WhenClick()
        {
            _stateBar.Bar();
            yield return StartCoroutine(RemoveItemInArea());
            if (_level == Level.Five)
            {
                _onEndGame?.Invoke();
                Debug.Log("Complete this state");
                yield break;
            }
            if (_isEndGame == true)
            {
                yield break;
            }
            yield return new WaitForSeconds(2.5f);
            NextLevel(++_numberEnum);
        }
        public void RemoveItem()
        {
            _isEndGame = true;
            if (_spawnArea.transform != null)
            {
                StartCoroutine(ClearItem(0.5f));
            }
        }

        private IEnumerator ClearItem(float timeDelay)
        {
            for (var i = 0; i < _childArea.Count; i++)
            {
                yield return new WaitForSeconds(timeDelay);
                _childArea[i].GetComponent<MoveBillboard>().RunAlways();
            }
            _childArea.Clear();
        }

        private IEnumerator RemoveItemInArea()
        {
            _childArea[0].transform.SetAsLastSibling();
            _childArea.RemoveAt(0);
            if (_childArea != null)
            {
                yield return StartCoroutine(ClearItem(0.5f));
            }
            yield return new WaitForSeconds(1);
        }
        private void NextLevel(int num)
        {
            _level = (Level)num;
            SpawnBillBoard();
        }
        private void SpawnBillBoard()
        {
            switch (_level)
            {
                case Level.One:
                    Spawn(_levelItemsOnes);
                    break;
                case Level.Two:
                    Spawn(_levelItemsTwos);
                    break;
                case Level.Three:
                    Spawn(_levelItemsThrees);
                    break;
                case Level.Four:
                    Spawn(_levelItemsFours);
                    break;
                case Level.Five:
                    Spawn(_levelItemsFives);
                    _numberEnum = 0;
                    break;
                default:
                    break;
            }
        }
        private void Spawn(BillboardItems[] billboardItems)
        {
            for (var i = 0; i < billboardItems.Length; i++)
            {
                //Random spawn position X Y in Area
                float ranX = UnityEngine.Random.Range(-_size.x, _size.x);
                float ranY = UnityEngine.Random.Range(-_size.y, _size.y);
                Vector3 pos = _spawnArea.transform.position + new Vector3(ranX, ranY ,0);

                //Instantiate Prefab in area and set parent
                GameObject spawn = Instantiate(billboardItems[i].billboardObj, pos, Quaternion.identity);
                AssignData(spawn,billboardItems[i]);
                spawn.transform.SetParent(_spawnArea.transform);
            }
            foreach (Transform item in _spawnArea.transform)
            {
                _childArea.Add(item.gameObject);
            }
        }
        public void AssignData(GameObject Obj ,BillboardItems RefItem) 
        {
            MoveBillboard moveBillboard = Obj.GetComponent<MoveBillboard>();
            TMP_Text textDetail = Obj.GetComponentInChildren<TMP_Text>();    
            ClickEvent clickEvent = Obj.GetComponent<ClickEvent>();

            moveBillboard.Duration = RefItem.duration;

            // textDetail.text = RefItem.detail;
            if (_isEnglish)
            {
                textDetail.text = RefItem.textDetails[0].detail;
            }
            else
            {
                textDetail.text = RefItem.textDetails[1].detail;
            }

            clickEvent.BillboardRef = RefItem;
            clickEvent.onClickInCorrect += ClickInCorrect;
            clickEvent.onClickCorrect += ClickCorrect;

            foreach (Vector3 item in RefItem.targets)
            {
                moveBillboard.TargetMoves.Add(item);
            }
            moveBillboard.IsMove = true;
        }
    }
}