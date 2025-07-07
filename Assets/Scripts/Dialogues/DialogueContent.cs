using UnityEngine;


[CreateAssetMenu(menuName = "Dialogue")]
public class DialogueContent : ScriptableObject
{
    public string speaker = "";
    public string[] content;
}
