using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(NoteAttribute))]
public class NoteDrawer : DecoratorDrawer
{
    private const float K_PADDING = 20f;
    private float m_height = 0f;
    public override float GetHeight()
    {
        NoteAttribute noteAttribute = attribute as NoteAttribute;
        GUIStyle style = EditorStyles.helpBox;
        style.alignment = TextAnchor.MiddleCenter;
        style.wordWrap = true;
        style.padding = new RectOffset(10,10,10,10);
        style.fontSize = 12;

        m_height = style.CalcHeight(new GUIContent(noteAttribute.text),Screen.width);
        return m_height + K_PADDING;
    }
    public override void OnGUI(Rect position)
    {
        NoteAttribute noteAttribute = attribute as NoteAttribute;

        position.height = m_height;
        position.y += K_PADDING * 0.5f;
        EditorGUI.HelpBox(position,noteAttribute.text,MessageType.None);
    }
}
