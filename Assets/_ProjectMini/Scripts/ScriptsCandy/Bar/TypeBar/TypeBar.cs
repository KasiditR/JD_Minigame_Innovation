using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Dtawan
{
    public class TypeBar : BaseBar
    {
        [SerializeField] private Image _fill = null;
        [SerializeField] private TextMeshProUGUI _textAmount = null;
        [SerializeField] [Range(0,100)] private float _value = 0f;
        [SerializeField] private float _minValue = 0;
        [SerializeField] private float _maxValue = 100;
        [SerializeField] private NameTypeBar nameTypeBar;
        [SerializeField] private BarControl barControl;

        public float Value
        {
            get => _value;
            set
            {
                this._value += value;
                if (this._value >= _maxValue)
                {
                    barControl.CheckEnd(nameTypeBar);
                }
                base.ChangeValue(this._value);
            }
        }

        public void Awake()
        {
            base.Init(_fill, _textAmount, _value, _minValue, _maxValue);
        }
        
    }
}
