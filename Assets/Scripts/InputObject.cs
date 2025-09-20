using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InputObject : MonoBehaviour
{
    [SerializeField] private ObjectMover mover;
    [SerializeField] private TMP_Text text;

    private DialogueType requiredType;
    public static event Action OnTextCollided;
    public static event Action OnCorrectInput;
    public static event Action OnWrongInput;

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
            case DialogueType.flavour:
                text.color = Color.white;
                break;
            case DialogueType.command:
                text.color = Color.red;
                break;
            case DialogueType.accuse:
                text.color = Color.blue;
                break;
            case DialogueType.insult:
                text.color = Color.green;
                break;
            case DialogueType.compliment:
                text.color = Color.yellow;
                break;
        }
    }

    

    private void CompareInput(KeyCode key)
    {
        if (InputManager.Instance.InputMap[requiredType] == key)
        {
            OnCorrectInput?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            OnWrongInput?.Invoke();
            player.GetHit(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTextCollided?.Invoke();
            player.GetHit(1);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        InputManager.OnInput -= CompareInput;
    }
}
