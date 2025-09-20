using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject inputTextObjectPrefab;
    [SerializeField] private GameObject staticTextObjectPrefab;

    private DialogueObject currentDialogue;
    private int currentIndex = 0;
    private TextObject currentTextObject;
    private bool nextTextIsResponse;

    [SerializeField] private Player player;
    private Enemy activeEnemy;

    public bool PlayerSucceeded { get; set; }

    public static Action OnTextProgress;

    private void Awake()
    {
        Instance = this;
        OnTextProgress += ProgressDialogue;
    }

    private void Update()
    {

    }

    private void ProgressDialogue()
    {
        if (nextTextIsResponse)
        {
            SpawnResponse(currentIndex - 1, PlayerSucceeded);
        }
        else
        {
            if (currentIndex == currentDialogue.Dialogue.Length)
            {
                activeEnemy.EndDialogue();
                activeEnemy = null;
                currentDialogue = null;
                return;
            }

            SpawnNextDialogue(currentIndex);
            currentIndex++;
        }
    }

    private void SpawnResponse(int index, bool success)
    {
        Vector3 spawnPos = player.transform.position + new Vector3(-0.8f, 1f, 1.5f);
        currentTextObject = Instantiate(staticTextObjectPrefab, spawnPos, Quaternion.identity).GetComponent<StaticTextObject>();
        if (currentTextObject is StaticTextObject staticTextObject)
        {
            string message = success 
                ? currentDialogue.Dialogue[index].successResponse 
                : currentDialogue.Dialogue[index].failureResponse;

            staticTextObject.Setup(message, player);
        }
        nextTextIsResponse = false;
    }

    private void SpawnNextDialogue(int index)
    {
        DialogueText textToSpawn = currentDialogue.Dialogue[index];

        if (textToSpawn.isRespondableText)
        {
            nextTextIsResponse = true;
        }

        Vector3 spawnPos;
        if (IsPlayerText(textToSpawn.dialogueType))
        {
            spawnPos = player.transform.position + new Vector3(-0.8f, 1f, 1.5f);
        }
        else
        {
            spawnPos = activeEnemy.transform.position + new Vector3(0f, 1.5f, 0f);
        }

        if (textToSpawn.dialogueType == DialogueType.Nothing || IsPlayerText(textToSpawn.dialogueType))
        {
            currentTextObject = Instantiate(staticTextObjectPrefab, spawnPos, Quaternion.identity).GetComponent<TextObject>();
            if (currentTextObject is StaticTextObject staticTextObject)
            {
                staticTextObject.Setup(textToSpawn.text, player);
            }
        }
        else
        {
            currentTextObject = Instantiate(inputTextObjectPrefab, spawnPos, Quaternion.identity).GetComponent<TextObject>();
            if (currentTextObject is InputTextObject inputTextObject)
            {
                inputTextObject.Setup(textToSpawn, player);
            }
        }
    }

    public void RegisterActiveEnemy(Enemy enemy)
    {
        activeEnemy = enemy;
    }

    public void StartDialogue(DialogueObject dialogueObject)
    {
        currentDialogue = dialogueObject;
        currentIndex = 0;
    }

    private bool IsPlayerText(DialogueType type)
    {
        return type == DialogueType.PlayerText
            || type == DialogueType.PlayerResponseFail
            || type == DialogueType.PlayerResponseSuccess;
    }
}