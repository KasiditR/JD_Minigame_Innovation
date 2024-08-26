using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dtawan
{
    public class BaseBar : MonoBehaviour
    { 
        protected Image fill { get; set; }
        protected TextMeshProUGUI textAmount {get; set;}
        protected float value { get; set; }
        protected float minValue { get; set; }
        protected float maxValue { get; set; }
        private bool isMax = false;
        private void LateUpdate()
        {
            if (!isMax)
            {
                GetCurrentFill(fill,value,maxValue,minValue);
                float amount = GetCurrentFill(fill,value,maxValue,minValue).fillAmount;
                textAmount.text = $"{GetCalculateValue(amount,maxValue,minValue)}";
                if (value >= maxValue)
                {
                    isMax = true;
                }
            }
        }
        protected virtual void Init(Image fill, TextMeshProUGUI textAmount, float value, float minValue, float maxValue)
        {
            this.fill = fill;
            this.textAmount = textAmount;
            this.value = value;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public virtual void ChangeValue(float value)
        {
            this.value = value;
        }
        public void IncreaseValue(float value)
        {
            ChangeValue(this.value += value);
        }
        public void DecreaseValue(float value)
        {
            ChangeValue(this.value -= value);
        }
    
        protected virtual Image GetCurrentFill(Image fill, float value,float maxValue,float minValue)
        {
            float currentValue = value - minValue;
            float maximumOffSet = maxValue - minValue;
            float fillAmount = (float)currentValue/(float)maximumOffSet;
            fill.fillAmount = fillAmount;
            return fill;
        }
        protected virtual int GetCalculateValue(float value,float maxValue,float minValue)
        {
            return (int)((value * (maxValue - minValue)) + minValue);
        }
    }
}
