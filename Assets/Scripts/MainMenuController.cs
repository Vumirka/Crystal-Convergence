using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Кнопки головного меню — призначаю через інспектор
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    // Назва сцени гри, яку завантажую при натисканні "Грати"
    [SerializeField] private string gameSceneName = "GameScene";

    private void Start()
    {
        // Прив'язую методи до кнопок (перевіряю, чи вони не null)
        if (playButton != null)
            playButton.onClick.AddListener(StartGame);      // Кнопка "Грати"

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OpenSettings); // Кнопка "Налаштування"

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);       // Кнопка "Вийти"
    }

    // Завантаження сцени гри
    private void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // Відкриття налаштувань — поки просто виводжу повідомлення в консоль
    private void OpenSettings()
    {
        Debug.Log("Settings opened");
    }

    // Вихід з гри (в редакторі просто зупиняє Play Mode)
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Для Unity Editor
#else
        Application.Quit(); // Для збірки гри
#endif
    }
}
