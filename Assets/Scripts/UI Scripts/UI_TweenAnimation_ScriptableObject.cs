using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "UI", menuName = "ScriptableObject_UI/UI_Elements", order = 0)]
public class UI_TweenAnimation_ScriptableObject: ScriptableObject
{
    [Header("DOTween")]

    [SerializeField]
    private float[] tweenDurations;
    public float[] TweenDurations
    {
        get
        {
            return tweenDurations;
        }
        /*set
		{
			tweenDuration = value;
		}*/
    }

    [SerializeField]
    private bool[] tweenSnappings;
    public bool[] TweenSnappings
    {
        get
        {
            return tweenSnappings;
        }
        /*set
        {
            tweenSnappings = value;
        }*/
    }

    [SerializeField]
    private Ease easeType;
    public Ease EaseType
    {
        get
        {
            return easeType;
        }
        /*set
        {
            tweenDelayDurations = value;
        }*/
    }

    [SerializeField]
    private float[] tweenDelayDurations;

    public float[] TweenDelayDurations
    {
        get
        {
            return tweenDelayDurations;
        }
        /*set
        {
            tweenDelayDurations = value;
        }*/
    }


}
