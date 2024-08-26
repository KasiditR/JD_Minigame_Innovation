#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HorizontalLineAttribute))]

public class HorizontalLineDrawer : DecoratorDrawer
{
    public override float GetHeight()
    {
        HorizontalLineAttribute attr = attribute as HorizontalLineAttribute;
        return Mathf.Max(attr.padding,attr.thickness);
    }
    public override void OnGUI(Rect position)
    {
        HorizontalLineAttribute attr = attribute as HorizontalLineAttribute;

        position.height = attr.thickness;
        position.y += attr.padding * 0.5f;
        
        EditorGUI.DrawRect(position,EditorGUIUtility.isProSkin ? new Color(0.3f,0.3f,0.3f,1) : new Color(0.7f,0.7f,0.7f,1f));
    }
}

#endif