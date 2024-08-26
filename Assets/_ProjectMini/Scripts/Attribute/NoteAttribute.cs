using UnityEngine;

public class NoteAttribute : PropertyAttribute
{
    public string text = null;

    public NoteAttribute(string text)
    {
        this.text = text;
    }
}
