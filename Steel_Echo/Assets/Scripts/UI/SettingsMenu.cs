using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;
    public TextMeshProUGUI controlsText;
    
    [Header("Audio")]
    public AudioMixer audioMixer;
    
    private const string VolumeKey = "MasterVolume";
    
    void Start()
    {
        // Загружаем сохранённую громкость
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.75f);
        volumeSlider.value = savedVolume;
        
        // Устанавливаем громкость
        SetVolume(savedVolume);
        
        // Обновляем текст
        UpdateVolumeText(savedVolume);
        
        // Добавляем слушатель на слайдер
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        
        // Устанавливаем текст управления
        // SetControlsText();
    }
    
    void OnVolumeChanged(float value)
    {
        SetVolume(value);
        UpdateVolumeText(value);
        
        // Сохраняем настройку
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
    
    void SetVolume(float volume)
    {
        // Преобразуем линейное значение (0-1) в децибелы (-80 до 0)
        float volumeDB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        audioMixer.SetFloat("MasterVolume", volumeDB);
    }
    
    void UpdateVolumeText(float volume)
    {
        int percentage = Mathf.RoundToInt(volume * 100);
        volumeValueText.text = $"{percentage}%";
    }
    
    // void SetControlsText()
    // {
    //     controlsText.text = 
    //         "Управление\n\n" +
    //         "A: Влево\n" +
    //         "D: Вправо\n" +
    //         "Space: Прыжок\n";
    // }
}