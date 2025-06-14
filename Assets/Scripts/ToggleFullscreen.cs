using UnityEngine;
using TMPro;

public class ToggleFullscreen : MonoBehaviour
{
    public TextMeshProUGUI buttonText; // Текст кнопки для відображення режиму екрану
    private bool isFullscreen;          // Змінна для збереження поточного стану повного екрану

    private void Start()
    {
        // Ініціалізуємо стан fullscreen за поточними налаштуваннями екрану
        isFullscreen = Screen.fullScreen;
        UpdateButtonText();

        // Встановлюємо роздільну здатність і режим екрану при запуску гри
        if (isFullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; // Повний екран
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true); // Максимальна роздільність екрану
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed; // Віконний режим
            Screen.SetResolution(1280, 720, false);           // Фіксована роздільність для вікна
        }
    }

    // Метод, який викликається при натисканні кнопки — перемикає режими екрану
    public void ToggleScreenMode()
    {
        if (isFullscreen)
        {
            // Якщо зараз повний екран — переключаємо на віконний режим з фіксованою роздільністю
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.SetResolution(1280, 720, false);
            isFullscreen = false;
        }
        else
        {
            // Якщо зараз віконний режим — переключаємо на повний екран з максимальною роздільністю
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            isFullscreen = true;
        }
        UpdateButtonText(); // Оновлюємо текст кнопки відповідно до нового стану
    }

    // Оновлює текст на кнопці для індикації режиму екрану
    private void UpdateButtonText()
    {
        buttonText.text = isFullscreen ? "Вікно" : "Повний екран";
    }
}
