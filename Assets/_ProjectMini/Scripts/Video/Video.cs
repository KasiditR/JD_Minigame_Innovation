using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Boss.Panel
{
    public class Video : MonoBehaviour
    {
        [SerializeField] private VideoPlayer m_videoPlayer;
        [SerializeField] private string m_vdoName;
        [SerializeField] private GameObject m_loading;
        private void Start() 
        {
            StartCoroutine(WaitForPlay());
            m_videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath,m_vdoName);
            
        }
        private IEnumerator WaitForPlay()
        {
            while (m_videoPlayer.frame > 1)
            {
                m_loading.SetActive(true);
            }
            yield return new WaitForSeconds(0.5f);
            m_loading.SetActive(false);
        }
    }
}
