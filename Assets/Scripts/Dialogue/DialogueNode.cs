using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Scriptable Objects/DialogueNode")]
public class DialogueNode : ScriptableObject
{
    public DialogueOptions [] Dialogues;
}
