using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Boss.UI;
namespace Dtawan.MatchingItem
{
    public sealed class MatchingManager : MonoBehaviour
    {
        [SerializeField] private ScoreManagerSO scoreManager;
        [SerializeField] private PanelGameOver _panelGameOver;
        [SerializeField] private CountDown _countDown;
        [SerializeField] private MatchingManagerSO matchingManager;
        [SerializeField] private AnimationMatch _animationMatch;
        [SerializeField] private float _cooldownMatch = 3.71f;
        [SerializeField] private float _cooldownNotMatch = 2f;
        [SerializeField] private float cooldown; // wait Edit
        [SerializeField] private float gameOverTime;
        [SerializeField] private List<float> reduceTimes;
        [SerializeField] private bool isOutOfTime = false;
        [SerializeField] private TextMeshProUGUI timeTmp;
        [SerializeField] private TMP_Text _matchText;
        private float time;
        private float reduceTime;
        private bool _isMatch;
        private bool unCheck;

        private void Awake()
        {
            unCheck = true;
        }
        // private void Awake()
        // {
        //     itemManager.IsCooldown = false;
        //     time = gameOverTime;
        //     ShowTimeTMP();
        // }

        // private void Start()
        // {
        //     StartCoroutine(nameof(CooldownGameOverTime));
        // }
        public void StartMatchManager()
        {
            reduceTime = reduceTimes[matchingManager.state - 1];
            isOutOfTime = false;
            unCheck = false;
            matchingManager.IsCooldown = false;
            
            if (matchingManager.state == 1)
            {
                time = gameOverTime;
            }
            else
            {
                if (matchingManager.StateMode == Mode.ResetTime)
                {
                    time = gameOverTime;
                }
            }
            
            ShowTimeTMP();
            // StartCoroutine(nameof(CooldownGameOverTime));
        }
        public void StartCoolDown()
        {
            StartCoroutine(nameof(CooldownGameOverTime));
        }
        private void Update()
        {
            // Debug.Log(time);
            if (matchingManager.countToCheckMatching == 2)
            {
                CheckMatching();
                matchingManager.countToCheckMatching = 0;
            }
            // change endgame to restart
            if (!unCheck)
            {
                CheckEndGame(); 
            }
        }

        private void CheckMatching()
        {
            if (matchingManager.FirstItem.Type == matchingManager.LastItem.Type)
            {
                _isMatch = true;
                Debug.Log("Matching");
                CheckTypeForPrint(matchingManager.FirstItem.Type);
                StartCoroutine(Matching());
            }
            else
            {
                _isMatch = false;
                NotMatching();
                Debug.Log("No");
            }
            
            StartCoroutine(Cooldown());
            Debug.Log("DoMatching");

        }
        private IEnumerator Cooldown()
        {
            matchingManager.IsCooldown = true;
            if (_isMatch)
            {
                cooldown = _cooldownMatch;
            }
            else
            {
                cooldown = _cooldownNotMatch;
            }
            yield return new WaitForSeconds(cooldown);
            matchingManager.IsCooldown = false;
            matchingManager.FirstItem = null;
            matchingManager.LastItem = null;
            ClearItemSelected();
        }

        private IEnumerator CooldownGameOverTime()
        {
            float cooldownTime = 1;
            
            while (!isOutOfTime)
            {
                yield return new WaitForSeconds(cooldownTime);
                time -= cooldownTime;
                CheckTime();
            }
        }
        
        private IEnumerator Matching()
        {
            yield return StartCoroutine(_animationMatch.PlayAnimMatch());
            // yield return new WaitForSeconds(cooldown);
            matchingManager.FirstItem.gameObject.SetActive(false);
            matchingManager.LastItem.gameObject.SetActive(false);
            matchingManager.countItemList -= 2;
            /*matchingManager.ItemsList.Remove(matchingManager.FirstItem);
            matchingManager.ItemsList.Remove(matchingManager.LastItem);*/
        }
        private void NotMatching()
        {
            time -= reduceTime;
            CheckTime();
            _animationMatch.ReduceText(reduceTime,_cooldownNotMatch);
            StartCoroutine(_animationMatch.CloseItem());
            // AnimationNotMatch();
        }
        private void CheckTime()
        {
            ShowTimeTMP();
            
            if (time <= 0)
            {
                isOutOfTime = true;
            }
            
            //CheckEndGame();
        }
        
        private void CheckEndGame()
        {
            if (matchingManager.countItemList == 0 || isOutOfTime)
            {
                unCheck = true;
                
                if (isOutOfTime)
                {
                    //OpenPanelGameOver With Not Success
                    EndGame(false);
                }
                else
                {
                    isOutOfTime = true;
                    Invoke(nameof(NewState), 1f);
                }
                //matchingManager.ItemsList.Clear();
            }
        }
        
        
        private void NewState()
        {
            matchingManager.state++;
            
            if (matchingManager.IsNewState())
            {
                matchingManager.ShowItem(false);
                _countDown.gameObject.SetActive(true);
                _countDown.OpenCountDown();
                for (var i = 0; i < matchingManager.ItemsList.Count; i++)
                {
                    matchingManager.ItemsList[i].GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else
            {
                //OpenPanelGameOver With Success
                EndGame(true);
            }
            
        }

        private void EndGame(bool isSuccess)
        {
            _panelGameOver.OpenPanel();
            _panelGameOver.OpenButton(isSuccess);
        }

        private void ClearItemSelected()
        {
            foreach (var item in matchingManager.ItemSelected)
            {
                item.ClearImage();
            }
        }

        private void ShowTimeTMP()
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);

            timeTmp.text = $"{minutes:00}:{seconds:00}";
        }
        private void CheckTypeForPrint(ItemTypes itemTypes)
        {
            string text = null;
            switch (itemTypes)
            {
                case ItemTypes.BetterResult:
                    text = "Better Result";
                    break;
                case ItemTypes.ContinuousLearning:
                    text = "Continuous Learning";
                    break;
                case ItemTypes.Creativity:
                    text = "Creativity";
                    break;
                case ItemTypes.Don_tBlameMistake:
                    text = "Don't Blame Mistake";
                    break;
                case ItemTypes.EmbraceChange:
                    text = "Embrace Change";
                    break;
                case ItemTypes.FindNewSolutions:
                    text = "Find New Solutions";
                    break;
                case ItemTypes.Growth:
                    text = "Growth";
                    break;
                case ItemTypes.Improvement:
                    text = "Improvement";
                    break;
                case ItemTypes.TheLimitationIntoPossibilities:
                    text = "The Limitation Into Possibilities";
                    break;
                case ItemTypes.TryNewWays:
                    text = "Try New Ways";
                    break;
                default:
                    break;
            }
            _matchText.text = text;
        }
        /*private void AnimOnClick()
        {
            if (itemManager.FirstItem.IsReverse)
            {
                
            }
            
            if (itemManager.LastItem.IsReverse)
            {
                
            }
        }*/
    }
}