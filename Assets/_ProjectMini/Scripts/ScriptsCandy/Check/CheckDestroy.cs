using UnityEngine;

namespace Dtawan
{
    public enum NameTypeBar
    {
        HomeSolution,
        FMCG,
        HBA,
        CCC,
        HomeApp
    }
    
    public class CheckDestroy : MonoBehaviour
    {
        [SerializeField] private BarControl barControl;
        [SerializeField] private NameTypeBar nameTypeBar;
        [SerializeField] private float point;
        
        
        public BarControl BarControl
        {
            get => barControl;
            set => barControl = value;
        }

        private void OnDestroy()
        {
            //_barControl.HomeSolValue += 1.0f;
            switch (nameTypeBar)
            {
                case NameTypeBar.HomeSolution:
                    if (barControl.Bars[0].TypeBar.Value >= 100)
                    {
                        return;
                    }
                    barControl.Bars[0].TypeBar.Value = point;
                    break;
                case NameTypeBar.FMCG:
                    if (barControl.Bars[1].TypeBar.Value >= 100)
                    {
                        return;
                    }
                    barControl.Bars[1].TypeBar.Value = point;
                    break;
                case NameTypeBar.HBA:
                    if (barControl.Bars[2].TypeBar.Value >= 100)
                    {
                        return;
                    }
                    barControl.Bars[2].TypeBar.Value = point;
                    break;
                case NameTypeBar.CCC:
                    if (barControl.Bars[3].TypeBar.Value >= 100)
                    {
                        return;
                    }
                    barControl.Bars[3].TypeBar.Value = point;
                    break;
                case NameTypeBar.HomeApp:
                    if (barControl.Bars[4].TypeBar.Value >= 100)
                    {
                        return;
                    }
                    barControl.Bars[4].TypeBar.Value = point;
                    break;
            }
        }
    }
}
