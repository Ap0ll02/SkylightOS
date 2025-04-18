using UnityEngine;

[CreateAssetMenu(fileName = "NewNorthstarDialogue", menuName = "ScriptableObjects/NorthstarDialogue")]
public class NorthstarDialogueSO : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] dialogueLines;
}
