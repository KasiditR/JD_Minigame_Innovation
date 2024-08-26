using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dtawan
{
    public class ScoreManagerCandy : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public int score;

        // Update is called once per frame
        void Update()
        {
            //foreach (var item in scoreText)
            //{
                scoreText.text = "" + score;
            //}
        }
        public void increaseSocre(int amountToIncrease)
        {
            score += amountToIncrease;
            
        }
        public Image GetCurrentFill(Image fill, float value,float maxValue,float minValue)
        {
            float currentValue = value - minValue;
            float maximumOffSet = maxValue - minValue;
            float fillAmount = (float)currentValue/(float)maximumOffSet;
            fill.fillAmount = fillAmount;
            return fill;
        }
    }
}
