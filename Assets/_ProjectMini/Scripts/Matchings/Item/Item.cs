using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Dtawan.MatchingItem
{
    public sealed class Item : MonoBehaviour ,IPointerClickHandler
    {
        [SerializeField] private MatchingManagerSO matchingManager;
        
        [SerializeField] private SpriteRenderer image;
        [SerializeField] private TextMeshPro text;
        [SerializeField] private Vector3 _endValue;
        [SerializeField] private float _durationOpen = 0.5f;
        [SerializeField] private float _durationClose = 0.25f;
        [SerializeField] private RotateMode _rotateMode;

        public ItemTypes Type;
        public bool IsReverse = false;

        private int indexItem;

        private Item thisItem;
        private Vector3 _startRot = Vector3.zero;

        private void OnEnable()
        {
            thisItem = this.gameObject.GetComponent<Item>();
            IsReverse = false;
        }

        public void Init(int index)
        {
            indexItem = index;
            image.sprite = matchingManager.ItemsInfo[indexItem].ImageFront;
            Type = matchingManager.ItemsInfo[indexItem].Type;
            text.text = matchingManager.ItemsInfo[indexItem].Text;
            this.transform.rotation = Quaternion.Euler(0,180f,0);
        }
        public void RotateOpen()
        {
            this.transform.DORotate(_startRot,_durationOpen,_rotateMode);
        }
        public void RotateClose()
        {
            this.transform.DORotate(_endValue,_durationClose,_rotateMode);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            if (matchingManager.IsCooldown) return;

            if (!matchingManager.IsFirstItem)
            {
                int index = 0;
                
                matchingManager.FirstItem = thisItem;
                
                matchingManager.ItemSelected[index].image.sprite = image.sprite;
                matchingManager.ItemSelected[index].text.text = text.text;
                matchingManager.ItemSelected[index].text.enabled = true;
                
                matchingManager.countToCheckMatching++;
                matchingManager.IsFirstItem = !matchingManager.IsFirstItem;
                RotateOpen();
                // StartCoroutine(AnimOnClick());
            }
            else
            {
                if (matchingManager.FirstItem != thisItem)
                {
                    int index = 1;
                    matchingManager.LastItem = thisItem;
                    
                    matchingManager.ItemSelected[index].image.sprite = image.sprite;
                    matchingManager.ItemSelected[index].text.text = text.text;
                    matchingManager.ItemSelected[index].text.enabled = true;
                    
                    matchingManager.countToCheckMatching++;
                    matchingManager.IsFirstItem = !matchingManager.IsFirstItem;
                    RotateOpen();
                    // StartCoroutine(AnimOnClick());
                }
            }
        }

        public void ClearImage()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            image.sprite = null;
            text.enabled = false;
        }

        // private IEnumerator AnimOnClick()
        // {
            
        // }
    }
}