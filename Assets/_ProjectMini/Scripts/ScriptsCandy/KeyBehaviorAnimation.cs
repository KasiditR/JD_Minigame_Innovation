using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Dtawan
{
    public class KeyBehaviorAnimation : MonoBehaviour
    {
        [SerializeField]private CanvasGroup _canvasGroup;
        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        private void OnEnable()
        {
            _canvasGroup.DOFade(1, 1);
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
