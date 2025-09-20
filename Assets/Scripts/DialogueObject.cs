using UnityEngine;

[CreateAssetMenu]
public class DialogueObject : ScriptableObject
{
    [field: SerializeField] public DialogueText[] Dialogue { get; private set; }
}
