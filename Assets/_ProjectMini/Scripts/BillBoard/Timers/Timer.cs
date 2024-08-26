using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
namespace Boss.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_timerText = null;
        [SerializeField] private float m_timer = 0f;
        [SerializeField] private int m_seconds = 0;
        [SerializeField] private float m_minutes = 0;
        [SerializeField] private UnityEvent m_onTimerZero;
        private bool m_isStopTimer;
        public void StartTime()
        {
            StartCoroutine(CoolDownGameOverTime());
        }
        public void StopTime()
        {
            m_isStopTimer = true;
        }
        private IEnumerator CoolDownGameOverTime()
        {
            float coolDownTime = 1;
            
            while (!m_isStopTimer)
            {
                yield return new WaitForSeconds(coolDownTime);
                m_timer -= coolDownTime;
                CalculateTimer();
                if (m_timer <= 0)
                {
                    m_onTimerZero?.Invoke();
                    m_isStopTimer = true;
                }
            }
        }
        private void CalculateTimer()
        {
            m_minutes = Mathf.FloorToInt(m_timer / 60F);
            m_seconds = Mathf.FloorToInt(m_timer - m_minutes * 60);
            m_timerText.text = $"{m_minutes:00}:{m_seconds:00}";
        }
    }
}
