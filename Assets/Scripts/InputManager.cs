using System;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Dictionary<DialogueType, KeyCode> InputMap { get; private set; } = new()
    {
        { DialogueType.command, KeyCode.Y },
        { DialogueType.accuse, KeyCode.A },
        { DialogueType.insult, KeyCode.B },
        { DialogueType.compliment, KeyCode.X }
    };

    public static event Action<KeyCode> OnInput;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    private void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GameManager.Instance.State == GameState.MainMenu)
            return;

        if (GameManager.Instance.State == GameState.GameOver)
            return;

        if (Input.anyKeyDown)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                //GameManager.Instance.HandleEscapeButton();
            }

            if (GameManager.Instance.State == GameState.Paused)
                return;

            if (GameManager.Instance.State == GameState.Playing)
            {
                CheckInput();
            }
        }
    }

    private void CheckInput()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            KeyCode pressedKey = Event.current.keyCode;

            if (pressedKey == KeyCode.None) return;

            OnInput?.Invoke(pressedKey);
        }
        //switch (TargetCategory)
        //{
        //    case TextCategory.Commando:
        //        TargetCategory = TextCategory.None; // reset
        //        if (Input.GetKey(KeyCode.Y))
        //        {
        //            Player.Instance.MakeScore();
        //        }
        //        else
        //            Player.Instance.GetHit(1);
        //        break;

        //    case TextCategory.Gaslight:
        //        TargetCategory = TextCategory.None; // reset
        //        if (Input.GetKey(KeyCode.A))
        //        {
        //            Player.Instance.MakeScore();
        //        }
        //        else
        //            Player.Instance.GetHit(1);
        //        break;

        //    case TextCategory.Insult:
        //        TargetCategory = TextCategory.None; // reset
        //        if (Input.GetKey(KeyCode.B))
        //        {
        //            Player.Instance.MakeScore();
        //        }
        //        else
        //            Player.Instance.GetHit(1);
        //        break;

        //    case TextCategory.Compliment:
        //        TargetCategory = TextCategory.None; // reset
        //        if (Input.GetKey(KeyCode.X))
        //        {
        //            Player.Instance.MakeScore();
        //        }
        //        else
        //            Player.Instance.GetHit(1);
        //        break;
    }
}
