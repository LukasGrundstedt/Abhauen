using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ObjectMover mover;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private DialogueObject dialogue;

    public SpriteRenderer SpriteRenderer => sprite;

    private bool inDialogue;
    private int currentTextIndex;
    private InputTextObject currentText;
    private Player player;

    public void EngageDialogue(Player player)
    {
        this.player = player;
        DialogueManager.Instance.RegisterActiveEnemy(this);
        DialogueManager.Instance.StartDialogue(dialogue);
        mover.Stop();
        inDialogue = true;
    }

    public void EndDialogue()
    {
        mover.Resume();
        inDialogue = false;
        player = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            EngageDialogue(player);
            DialogueManager.OnTextProgress?.Invoke();
        }
    }
}
