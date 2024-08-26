using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Boss.UI
{
    public class ButtonNextGame : MonoBehaviour,IPointerClickHandler
    {
        [SerializeField] private string _gameName;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) 
            {
                return;
            }
            
            SceneManager.LoadScene(_gameName,LoadSceneMode.Single);
        }
    }
}
