using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Boss.AnimationBG
{
    public class JoyInSpace : MonoBehaviour
    {
        [SerializeField] private RectTransform m_joySpace = null;
        [SerializeField] private float m_durationPath = 30f;
        [SerializeField] private int m_resolutionPath = 10;
        [SerializeField] private Ease m_easePath = Ease.Linear;
        [SerializeField] private PathType m_pathType = PathType.CatmullRom;
        [SerializeField] private PathMode m_pathMode = PathMode.Full3D;
        [SerializeField] private Vector3[] m_paths;
        private void OnEnable()
        {
            m_joySpace.DOLocalPath(m_paths,m_durationPath,m_pathType,m_pathMode,m_resolutionPath).SetEase(m_easePath).SetLoops(-1);
        }
    }
}