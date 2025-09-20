[System.Serializable]
public struct DialogueText
{
    public string text;
    public DialogueType dialogueType;

    public bool isRespondableText;
    public string successResponse;
    public string failureResponse;
}