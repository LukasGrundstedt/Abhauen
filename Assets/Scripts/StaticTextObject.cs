using UnityEngine;

public class StaticTextObject : TextObject
{
    [SerializeField] private float lifetime = 3f;

    public void Setup(string text, Player player)
    {
        this.text.color = Color.black;
        this.text.text = text;
    }

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnDestroy()
    {
        DialogueManager.OnTextProgress?.Invoke();
    }
}