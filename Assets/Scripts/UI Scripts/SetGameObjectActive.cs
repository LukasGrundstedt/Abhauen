using UnityEngine;
using UnityEngine.UI;

public class SetGameObjectActive : MonoBehaviour
{
    [SerializeField]
    private Image chosenImage;

    private void Start()
    {
        InActiveImage();
    }

    public void InActiveImage()
    {
        chosenImage.CrossFadeAlpha(0, 0, true);
    }

    public void SetChosenGOActive()
    {
        chosenImage.CrossFadeAlpha(100, 3, true);
    }
}
