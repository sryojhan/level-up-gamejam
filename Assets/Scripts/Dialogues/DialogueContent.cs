using UnityEngine;


[CreateAssetMenu(menuName = "Dialogue")]
public class DialogueContent : ScriptableObject
{
    public Sprite speakerSprite;
    public string speaker = "";

    public string[] content;
}
