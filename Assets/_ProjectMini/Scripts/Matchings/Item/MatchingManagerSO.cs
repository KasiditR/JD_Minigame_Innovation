using System.Collections.Generic;
using UnityEngine;
using static System.Decimal;
using Random = System.Random;

namespace Dtawan.MatchingItem
{
    public enum Mode
    {
        NextTime,
        ResetTime
    }
    
    [CreateAssetMenu(fileName = "New ItemManager", menuName = "Create ItemManager")]
    public sealed class MatchingManagerSO : ScriptableObject
    {
        public Item ItemPrefab => itemPrefab;
        public List<ItemSO> ItemsInfo => itemsInfo;
        public Mode StateMode => stateMode;

        [SerializeField] private Mode stateMode;
        [SerializeField] private int maxState;
        public int state;
        public int countItemList;
        public List<Item> ItemsList;
        public Item[] ItemSelected = new Item[2];
        public int countToCheckMatching = 0;
        public Item FirstItem;
        public Item LastItem;
        public bool IsFirstItem = false;
        public bool IsCooldown = false;

        [SerializeField] private Item itemPrefab;
        [SerializeField] private List<ItemSO> itemsInfo;

        private Random random = new Random();
        //[SerializeField] private int countReturn = 0;

        public void ClearData()
        {
            ItemsList.Clear();
            state = 1;
            countToCheckMatching = 0;
            IsFirstItem = false;
            IsCooldown = false;
            FirstItem = null;
            LastItem = null;
            //countReturn = 0;
        }
        
        public bool IsNewState()
        {
            return state <= maxState;
        }
        
        public void ResetState()
        {
            if (state > maxState || state == 0)
            {
                state = 1;
            }
        }

        public void GetInfoItem()
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                int index = random.Next(ItemsInfo.Count);

                while (ItemsInfo[index].CountCreate == 0)
                {
                    /*for (int j = 0; j < ItemsInfo.Count; j++)
                    {
                        if (ItemsInfo[j].CountCreate == 0)
                        {
                            countReturn += 1;
                        }
                    }

                    if (countReturn == ItemsInfo.Count)
                    {
                        return;
                    }*/
                    index = random.Next(ItemsInfo.Count);
                }

                ItemsInfo[index].CountCreate--;
                ItemsList[i].Init(index);
            }

            countItemList = ItemsList.Count;
        }

        public void CountCreate()
        {
            for (int i = 0; i < ItemsInfo.Count; i++)
            {
                ItemsInfo[i].CountCreate = ItemsList.Count / ItemsInfo.Count;
            }
        }

        public void ShowItem(bool isShow)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                ItemsList[i].gameObject.SetActive(isShow);
            }
        }
    }
}