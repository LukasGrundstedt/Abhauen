using System.Collections;
using TMPro;
using UnityEngine;

public class Intro_TextDisplay : MonoBehaviour
{
    [Header("Text Settings")]
    public TextMeshProUGUI textComponent;
    [TextArea(2, 5)]
    public string[] introTexts;

    [Header("Timing")]
    public float delaySeconds = 3f;

    [SerializeField]
    private int introTextID=0;

    [Header("Auto Start")]
    public bool startOnAwake = true;

    void Awake()
    {
        // Get TextMeshPro component if not assigned
        if (textComponent == null)
            textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        if (startOnAwake)
        {
            StartTextReplacement();
        }
    }

    public void StartTextReplacement()
    {
        StartCoroutine(ReplaceTextAfterDelay());      
    }

    //The first text start and after X second delay, index +1
    private IEnumerator ReplaceTextAfterDelay()
    {
        for (introTextID = 0;introTextID < introTexts.Length;introTextID++)
        {
            if (introTextID == introTexts.Length)
            {
                StopCoroutine(ReplaceTextAfterDelay());
            }

            else
            {
                // Set initial text
                textComponent.text = introTexts[introTextID];

                // Wait for specified seconds
                yield return new WaitForSeconds(delaySeconds);

                // Save new introTextID + Replace with new text
                //introTextID += 1;
                textComponent.text = introTexts[introTextID];
            }
            

            
        }
        
        

    }
}
