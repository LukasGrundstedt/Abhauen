using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ObjectMover mover;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private DialogueText[] dialogues;
    [SerializeField] private GameObject textPrefab;

    private bool inDialogue;
    private int currentTextIndex;
    private InputObject currentText;
    private Player player;

    private void Update()
    {
        if (!inDialogue) return;
        if (currentText != null) return;
        if (currentTextIndex > dialogues.Length - 1)
        {
            EndDialogue();
            return;
        }

        SpawnText(currentTextIndex);
        currentTextIndex++;
    }

    public void EngageDialogue(Player player)
    {
        this.player = player;
        mover.Stop();
        inDialogue = true;
    }

    public void EndDialogue()
    {
        mover.Resume();
        inDialogue = false;
        player = null;
    }

    private void SpawnText(int index)
    {
        currentText = Instantiate(textPrefab, sprite.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity).GetComponent<InputObject>();
        currentText.Setup(dialogues[index], player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            EngageDialogue(player);
        }
    }
}
