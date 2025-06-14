using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    // Джерело звуку для музики
    public AudioSource musicSource;
    // Джерело звуку для звукових ефектів
    public AudioSource effectsSource;

    // Повзунки гучності
    public Slider masterVolumeSlider;   // Загальна гучність
    public Slider musicVolumeSlider;    // Гучність музики
    public Slider effectsVolumeSlider;  // Гучність ефектів

    private void Start()
    {
        // Коли змінюється значення повзунка, викликається відповідна функція
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsVolumeSlider.onValueChanged.AddListener(SetEffectsVolume);

        // Встановлюю початкові значення гучності при запуску
        SetMasterVolume(masterVolumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);
        SetEffectsVolume(effectsVolumeSlider.value);
    }

    // Змінює загальну гучність гри
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    // Змінює гучність тільки музики з урахуванням загальної гучності
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume * AudioListener.volume;
    }

    // Змінює гучність ефектів з урахуванням загальної гучності
    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = volume * AudioListener.volume;
    }
}
