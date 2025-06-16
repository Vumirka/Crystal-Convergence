using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // Посилання на панель мультиплеєру, яку будемо ховати
    public GameObject multiplayerPanel;
    // Посилання на головне меню, яке будемо показувати
    public GameObject mainMenuPanel;
    // Кнопка "Назад", яка буде реагувати на натискання
    public Button backButton;

    void Start()
    {
        // Додаємо слухача на кнопку backButton,
        // щоб при кліку викликалась функція OnBack
        backButton.onClick.AddListener(OnBack);
    }

    // Метод, який виконується при натисканні кнопки "Назад"
    void OnBack()
    {
        // Ховаємо панель мультиплеєру
        multiplayerPanel.SetActive(false);
        // Показуємо головне меню
        mainMenuPanel.SetActive(true);
    }
}
