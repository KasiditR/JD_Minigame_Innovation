using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Dtawan
{
    public class BarControl : MonoBehaviour
    {
        [SerializeField] private List<Bar> bars;
        private bool isHomeSolution = false;
        private bool isFMCG = false;
        private bool isHBA = false;
        private bool isCCC = false;
        private bool isHomeApp = false;
        [SerializeField] private UnityEvent onEndGame;
        [SerializeField]private CanvasGroup[] canvasGroup;
        //[SerializeField] private Image[] checkComplete;
        //[SerializeField] private float alpha = 255.0f;

        public List<Bar> Bars
        {
            get => bars;
            set => bars = value;
        }

        public void CheckEnd(NameTypeBar nameTypeBar)
        {
            switch (nameTypeBar)
            {
                case NameTypeBar.HomeSolution:
                    isHomeSolution = true;
                    //checkComplete[0].GetComponent<Image>().color = new Color(255, 255, 225, alpha);
                    canvasGroup[0].DOFade(1, 1);
                    Debug.Log("isHomeSolution");
                    break;
                case NameTypeBar.FMCG:
                    isFMCG = true;
                    //checkComplete[1].GetComponent<Image>().color = new Color(255, 255, 225, alpha);
                    canvasGroup[1].DOFade(1, 1);
                    Debug.Log("isFMCG");
                    break;
                case NameTypeBar.HBA:
                    isHBA = true;
                    //checkComplete[2].GetComponent<Image>().color = new Color(255, 255, 225, alpha);
                    canvasGroup[2].DOFade(1, 1);
                    Debug.Log("isHBA");
                    break;
                case NameTypeBar.CCC:
                    isCCC = true;
                    //checkComplete[3].GetComponent<Image>().color = new Color(255, 255, 225, alpha);
                    canvasGroup[3].DOFade(1, 1);
                    Debug.Log("isCCC");
                    break;
                case NameTypeBar.HomeApp:
                    isHomeApp = true;
                    //checkComplete[4].GetComponent<Image>().color = new Color(255, 255, 225, alpha);
                    canvasGroup[4].DOFade(1, 1);
                    Debug.Log("isHomeApp");
                    break;
            }

            if (isHomeSolution && isFMCG && isHBA && isCCC && isHomeApp)
            { 
                Debug.Log("Checkend");
                onEndGame?.Invoke();
            }
        }
    }
}
