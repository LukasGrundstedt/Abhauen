using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject textPrefab;
    [SerializeField] private float textCooldown = 2f;
    private float timer;

    private DialogueObject currentDialogue;
    private int currentIndex = 0;
    private TextObject currentTextObject;

    [SerializeField] private Player player;
    private Enemy activeEnemy;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (currentDialogue != null)
        {
            //SpeedController.Instance.Speed = 0;
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = textCooldown;
                ProgressDialogue();
            }
        }
    }

    private void ProgressDialogue()
    {
        if (currentIndex == currentDialogue.Dialogue.Length - 1)
        {
            activeEnemy.EndDialogue();
            activeEnemy = null;
            return;
        }

        SpawnText(currentIndex);
        currentIndex++;
    }

    private void SpawnText(int index)
    {
        DialogueText textToSpawn = currentDialogue.Dialogue[index];

        Vector3 spawnPos;
        if (textToSpawn.dialogueType == DialogueType.PlayerText)
        {
            spawnPos = player.transform.position;
        }
        else if (textToSpawn.dialogueType == DialogueType.Nothing)
        {
            spawnPos = activeEnemy.transform.position;
        }
        else
        {


            //Instantiate(textPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void RegisterActiveEnemy(Enemy enemy)
    {
        activeEnemy = enemy;
    }

    public void StartDialogue(DialogueObject dialogueObject)
    {
        currentDialogue = dialogueObject;
    }
}