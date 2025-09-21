using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private StudioEventEmitter eventEmitter;

    private FMOD.Studio.Bus musicBus;
    private FMOD.Studio.Bus soundBus;

    public float MusicVolume
    {
        get
        {
            musicBus.getVolume(out float volume);
            return volume;
        }
        set => musicBus.setVolume(value);
    }

    public float SoundVolume
    {
        get
        {
            soundBus.getVolume(out float volume);
            return volume;
        }
        set => soundBus.setVolume(value);
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        musicBus = RuntimeManager.GetBus("bus:/Music");
        soundBus = RuntimeManager.GetBus("bus:/SFX");
    }

    public void SetIntensity(float intensity)
    {
        eventEmitter.SetParameter("Intensity", intensity);
    }
}
