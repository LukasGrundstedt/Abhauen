using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;

public class TMPTriggerUnityEvent : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI textBox;
    [SerializeField]
    private string comicPanelTween_Name;

    [Header("Link Events")]
    [SerializeField]
    private UnityEvent<string> spawnComicPanel; // Passes the linkID

    [Header("Despawn all ComicPanel")]
    [SerializeField]
    private string despawnLinkID = "reset";
    [SerializeField]
    private UnityEvent<string> despawnComicPanel;

    //Subscribe to the event
    private void OnEnable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(CheckForLinkEvent);
    }
    //Unsubscribe to the event
    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(CheckForLinkEvent);
    }


    private void CheckForLinkEvent(UnityEngine.Object obj)
    {
        var amountOfLinksInCurrentText = textBox.textInfo.linkCount;

        //If there is no LinkID in currentTMP -> Return
        if (amountOfLinksInCurrentText == 0)
        {
            return;
        }

        //If there is, we run as many loops as there is link in the currentText
        for (int linkIndex = 0;linkIndex < amountOfLinksInCurrentText;linkIndex++)
        {
            //Get linkInfo
            var linkInfo = textBox.textInfo.linkInfo[linkIndex];

            //The animation only trigger when the <link="linkInfo.GetLinkID()"> is the same as comicPanelTween_Name
            if (linkInfo.GetLinkID() == comicPanelTween_Name)
            {
                //Trigger all the event that is in
                spawnComicPanel?.Invoke(linkInfo.GetLinkID());

                break;
            }

            else if (linkInfo.GetLinkID() == despawnLinkID)
            {
                //Trigger despawning ComicPanel
                despawnComicPanel?.Invoke(linkInfo.GetLinkID());
                break;
            }

            else
            {
                return;
            }
            
        }
    }


}
