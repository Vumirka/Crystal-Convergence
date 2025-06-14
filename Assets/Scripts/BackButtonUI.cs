using UnityEngine;
using UnityEngine.UI;

public class BackButtonUI : MonoBehaviour
{
    [Header("UI Панелі")]
    public GameObject currentPanel; // Поточна панель, яку потрібно закрити
    public GameObject previousPanel; // Панель, яку потрібно відкрити

    [Header("Кнопка")]
    public Button backButton;

    void Start()
    {
        // Перевірка, чи всі елементи призначені
        if (backButton == null || currentPanel == null || previousPanel == null)
        {
            Debug.LogError("Не призначено всі необхідні елементи в інспекторі!");
            return;
        }

        // Додавання слухача події для кнопки
        backButton.onClick.AddListener(BackToPreviousMenu);
    }

    void BackToPreviousMenu()
    {
        // Закриваємо поточну панель
        currentPanel.SetActive(false);

        // Відкриваємо попередню панель
        previousPanel.SetActive(true);
    }
}
