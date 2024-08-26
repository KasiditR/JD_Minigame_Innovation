using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using DG.Tweening;
namespace Dtawan.MatchingItem
{
    public sealed class BoardManager : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float spacingX;
        [SerializeField] private float spacingY;
        [SerializeField] private float _timeLook;
        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform[] positionsSelected = new Transform[2];
        [SerializeField] private MatchingManagerSO matchingManager;
        [SerializeField] private UnityEvent onPlay;
        [SerializeField] private Vector3 _endValue;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private Transform resetStartPosition;

        private void OnEnable()
        {
            resetStartPosition = startPosition;
            AwakeBoard();
            Debug.Log("OnEnable BoardManager");
        }
        
        public void Generator()
        {
            GeneratorBoard();
            Debug.Log("OnEnable Generator");
            //GeneratorCardSelected();
        }
        
        private void AwakeBoard()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector3 tempPosition = new Vector2(i + startPosition.position.x, j + startPosition.position.y)
                                           + new Vector2(i * spacingX, j * spacingY);
                    Item item = Instantiate(matchingManager.ItemPrefab, tempPosition, Quaternion.identity);
                    matchingManager.ItemsList.Add(item);
                    item.transform.SetParent(startPosition);
                    item.gameObject.SetActive(false);
                }
            }
        }
        
        private void GeneratorBoard()
        {
            matchingManager.CountCreate();
            matchingManager.GetInfoItem();
            Invoke(nameof(GeneratorCardSelected), 0.2f);
        }

        private void GeneratorCardSelected()
        {
            for (int i = 0; i < matchingManager.ItemSelected.Length; i++)
            {
                Item item = Instantiate(matchingManager.ItemPrefab, positionsSelected[i].position, Quaternion.identity);
                item.transform.localScale = new Vector3(3, 3, 3);
                item.GetComponent<BoxCollider2D>().enabled = false;
                item.ClearImage();
                matchingManager.ItemSelected[i] = item;
            }
            StartCoroutine(OpenCard());
        }
        
        private IEnumerator OpenCard()
        {
            if (matchingManager.state != 1)
            {
                startPosition = resetStartPosition;
            }

            matchingManager.ShowItem(true);
            
            startPosition.transform.DOMove(_endValue,_duration,false).SetEase(_ease);
            yield return new WaitForSeconds(1f);

            for (var i = 0; i < matchingManager.ItemsList.Count; i++)
            {
                matchingManager.ItemsList[i].RotateOpen();
                yield return new WaitForSeconds(0.3f);
            }
            yield return StartCoroutine(CloseCard());
        }
        
        private IEnumerator CloseCard()
        {
            yield return new WaitForSeconds(_timeLook);
            
            for (var i = 0; i < matchingManager.ItemsList.Count; i++)
            {
                matchingManager.ItemsList[i].RotateClose();
                // yield return new WaitForSeconds(0.3f);
            }

            for (var i = 0; i < matchingManager.ItemsList.Count; i++)
            {
                matchingManager.ItemsList[i].GetComponent<BoxCollider2D>().enabled = true;
            }
            onPlay?.Invoke();
        }
    }
}