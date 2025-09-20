using System;
using UnityEngine;

public class InputTextObject : TextObject
{
    [SerializeField] private ObjectMover mover;

    private DialogueType requiredType;
    public static event Action OnTextCollided;
    //public static event Action OnCorrectInput;
    //public static event Action OnWrongInput;

    private Player player;

    private void Awake()
    {
        InputManager.OnInput += CompareInput;
    }

    public void Setup(DialogueText message, Player player)
    {
        text.text = message.text;
        requiredType = message.dialogueType;
        this.player = player;

        switch (message.dialogueType)
        {
            case DialogueType.Nothing:
                text.color = Color.white;
                break;
            case DialogueType.Command:
                text.color = Color.red;
                break;
            case DialogueType.Accuse:
                text.color = Color.blue;
                break;
            case DialogueType.Insult:
                text.color = Color.green;
                break;
            case DialogueType.Compliment:
                text.color = Color.yellow;
                break;
        }
    }

    private void CompareInput(KeyCode key)
    {
        if (InputManager.Instance.InputMap[requiredType] == key)
        {
            //OnCorrectInput?.Invoke();
            Player.Instance.MakeScore();
            Destroy(gameObject);
        }
        else
        {
            //OnWrongInput?.Invoke();
            player.GetHit(1);
        }
        SpeedController.Instance.CountCount();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTextCollided?.Invoke();
            player.GetHit(1);
            SpeedController.Instance.CountCount();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        InputManager.OnInput -= CompareInput;
    }
}
