using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSetter : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.value = AudioManager.Instance.MusicVolume;
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.MusicVolume = volume;
    }
}
