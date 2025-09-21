using DG.Tweening;
using UnityEngine;

public class UI_TweenAnimation_Script: MonoBehaviour
{
    [Header("UI_SO to access Tween Value")]
    [SerializeField]
    private UI_TweenAnimation_ScriptableObject comic_UI_SO;
    [Header("~~TweenGoal_Container~~")]
    [SerializeField]
    private RectTransform[] tweenGoals;

    [Header("Elements to Tween")]
    [Tooltip("Amount of UI Elements to tween")]
    [SerializeField]
    private int elementCountAmount;
    [SerializeField]
    private RectTransform[] element_RectTransforms;

    [Tooltip("Which direction should Element go to? -> UP, DOWN, LEFT, RIGHT")]
    [SerializeField]
    private string[] directionNames;

    [SerializeField]
    private int additionalTweenValueToHide;

    [Header("_READONLY")]
    [SerializeField]
    private float[] currentTweenDurations;
    [SerializeField]
    private bool[] currentTweenSnappings;
    [SerializeField]
    private Ease currentEaseType;
    [SerializeField]
    private float[] currentDelayDurations;
    [SerializeField]
    private Vector2[] currentVector2;
    [SerializeField]
    private Vector2[] currentWidthXHeight;


    // Start is called before the first frame update
    void Start()
    {

        //Copy past value from SO to local var
        currentEaseType = comic_UI_SO.EaseType;
        for (int i = 0;i < elementCountAmount;i++)
        {
            currentTweenDurations[i] = comic_UI_SO.TweenDurations[i];
            currentTweenSnappings[i] = comic_UI_SO.TweenSnappings[i];
            currentDelayDurations[i] = comic_UI_SO.TweenDelayDurations[i];
        }

        //First make sure all Comic panel is hidden
        Hide_UIElements();

    }


    //First we would like to hide all the UI Elements outside the visible area
    //To do so, we simply add very big number to X and Y-value of the element
    //DO NOT deactivate the GO (as other scripts depends on its Start())
    //To hide it, just turn the Alpha to 0
    public void Hide_UIElements()
    {

        for (int i = 0;i < elementCountAmount;i++)
        {
            if (currentVector2[i] == null || element_RectTransforms[i] == null || currentWidthXHeight[i] == null)
                return;

            currentVector2[i].x = element_RectTransforms[i].anchoredPosition.x;
            currentVector2[i].y = element_RectTransforms[i].anchoredPosition.y;

            currentWidthXHeight[i].x = element_RectTransforms[i].rect.width;
            currentWidthXHeight[i].y = element_RectTransforms[i].rect.height;

            switch (directionNames[i])
            {
                case "UP":

                    element_RectTransforms[i].DOAnchorPosY(currentVector2[i].y - (currentWidthXHeight[i].y + additionalTweenValueToHide),
                        0,
                        currentTweenSnappings[i]).SetEase(currentEaseType);
                    //Debug.Log("Moving Up to hide " + i);
                    break;

                case "DOWN":
                    element_RectTransforms[i].DOAnchorPosY(currentVector2[i].y + (currentWidthXHeight[i].y + additionalTweenValueToHide),
                        0,
                        currentTweenSnappings[i]).SetEase(currentEaseType);
                    //Debug.Log("Moving Down to hide " + i);
                    break;

                case "LEFT":
                    element_RectTransforms[i].DOAnchorPosX(currentVector2[i].x + (currentWidthXHeight[i].x + additionalTweenValueToHide),
                        0,
                        currentTweenSnappings[i]).SetEase(currentEaseType);
                    //Debug.Log("Moving left to hide " + i);
                    break;

                case "RIGHT":
                    element_RectTransforms[i].DOAnchorPosX(currentVector2[i].x - (currentWidthXHeight[i].x + additionalTweenValueToHide),
                        0,
                        currentTweenSnappings[i]).SetEase(currentEaseType);
                    //Debug.Log("Moving right to hide" + i);
                    break;

                default:
                    //If not a single direction is chosen
                    Debug.Log("Not a single direction for UI was chosen or direction is not in UPPERCASE. ERROR!!!");
                    break;
            }
        }


    }


    //The UI now tween towards the TweenGoal
    //Doing the OPPOSITE of that was hidden
    //UP <=> DOWN
    //LEFT <=> RIGHT
    public void ShowUI_Element()
    {
        for (int i = 0;i < elementCountAmount;i++)
        {
            if (currentVector2[i] == null || element_RectTransforms[i] == null || currentWidthXHeight[i] == null)
                return;

            var currentIndex = i;
            switch (directionNames[i])
            {
                case "UP":
                    //We go DOWN
                    element_RectTransforms[i].DOAnchorPosY(tweenGoals[i].anchoredPosition.y,
                        currentTweenDurations[i],
                        currentTweenSnappings[i]).SetEase(currentEaseType);
                    //Debug.Log("Moving DOWN to show " + i);
                    break;

                case "DOWN":
                    //We go UP
                    element_RectTransforms[i].DOAnchorPosY(tweenGoals[i].anchoredPosition.y,
                        currentTweenDurations[i],
                        currentTweenSnappings[i]).SetEase(currentEaseType);
                    //Debug.Log("Moving UP to show " + i);
                    break;

                case "LEFT":
                    //We go RIGHT
                    element_RectTransforms[i].DOAnchorPosX(tweenGoals[i].anchoredPosition.x,
                        currentTweenDurations[i],
                        currentTweenSnappings[i]).SetEase(currentEaseType);
                    //Debug.Log("Moving RIGHT to show " + i);
                    break;

                case "RIGHT":
                    //We go LEFT
                    element_RectTransforms[i].DOAnchorPosX(tweenGoals[i].anchoredPosition.x,
                        currentTweenDurations[i],
                        currentTweenSnappings[i]).SetEase(currentEaseType);
                    //Debug.Log("Moving LEFT to show" + i);
                    break;

                default:
                    //If not a single direction is chosen
                    Debug.Log("Not a single direction for UI was chosen or direction is not in UPPERCASE. ERROR!!!");
                    break;
            }
        }
    }
}
