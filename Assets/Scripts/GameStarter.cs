using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Скрипт відповідає за перевірку всіх налаштувань перед запуском гри та завантаження ігрової сцени
public class GameStarter : MonoBehaviour
{
    [Header("Імена гравців (отримані з NameManager)")]
    public TextMeshProUGUI player1NameText;  // Текст з ім’ям гравця 1
    public TextMeshProUGUI player2NameText;  // Текст з ім’ям гравця 2

    [Header("Toggle Groups")]
    public ToggleGroup startPlayerToggleGroup;  // Група кнопок: хто починає першим
    public ToggleGroup timerToggleGroup;        // Група кнопок: тривалість таймера
    public ToggleGroup digitsToggleGroup;       // Група кнопок: скільки цифр має бути у числі
    public ToggleGroup roundsToggleGroup;       // Група кнопок: кількість раундів

    [Header("Раунди")]
    public TMP_InputField customRoundsInput;    // Поле для введення кількості раундів вручну

    [Header("Панель помилки")]
    public GameObject errorPanel;               // Панель помилки, яка з’являється при неправильних налаштуваннях
    public TextMeshProUGUI errorText;           // Текст повідомлення про помилку
    public Button closeErrorButton;             // Кнопка для закриття панелі помилки

    [Header("Назва ігрової сцени")]
    public string gameSceneName = "GameScene";  // Назва сцени, яка буде завантажена при старті гри

    void Start()
    {
        // Призначаємо дію для кнопки закриття помилки
        closeErrorButton.onClick.AddListener(() => errorPanel.SetActive(false));
    }

    // Метод, який викликається при натисканні кнопки "Старт"
    public void OnStartButtonClick()
    {
        // === Перевірка імен гравців ===
        string name1 = player1NameText.text.Trim();
        string name2 = player2NameText.text.Trim();

        if (string.IsNullOrEmpty(name1) || name1 == "Гравець 1")
        {
            ShowError("Ім’я Гравця 1 не збережене.");
            return;
        }

        if (string.IsNullOrEmpty(name2) || name2 == "Гравець 2")
        {
            ShowError("Ім’я Гравця 2 не збережене.");
            return;
        }

        // === Перевірка, чи вибрано опції в кожному блоці ===
        if (!IsToggleGroupSelected(startPlayerToggleGroup))
        {
            ShowError("Оберіть, хто починає гру.");
            return;
        }

        if (!IsToggleGroupSelected(timerToggleGroup))
        {
            ShowError("Оберіть варіант таймера.");
            return;
        }

        if (!IsToggleGroupSelected(digitsToggleGroup))
        {
            ShowError("Оберіть кількість цифр.");
            return;
        }

        if (!IsToggleGroupSelected(roundsToggleGroup))
        {
            ShowError("Оберіть варіант кількості раундів.");
            return;
        }

        // === Обробка таймера ===
        Toggle selectedTimerToggle = GetSelectedToggle(timerToggleGroup);
        if (selectedTimerToggle != null)
        {
            // Встановлюємо відповідне значення в GameData
            if (selectedTimerToggle.name == "noTimerToggle") GameData.TimerDuration = 0;
            else if (selectedTimerToggle.name == "timer30Toggle") GameData.TimerDuration = 30;
            else if (selectedTimerToggle.name == "timer60Toggle") GameData.TimerDuration = 60;
        }

        // === Обробка кількості раундів ===
        Toggle selectedRoundsToggle = GetSelectedToggle(roundsToggleGroup);
        if (selectedRoundsToggle != null)
        {
            if (selectedRoundsToggle.name == "noRoundsToggle")
            {
                GameData.UseRounds = false;
                GameData.MaxRounds = 0;
            }
            else if (selectedRoundsToggle.name == "customRoundsToggle")
            {
                string input = customRoundsInput.text.Trim();

                // Якщо не введено число — помилка
                if (string.IsNullOrEmpty(input))
                {
                    ShowError("Введіть кількість раундів.");
                    return;
                }

                // Перевірка правильності введеного числа
                int customRounds;
                if (!int.TryParse(input, out customRounds) || customRounds < 1 || customRounds > 99)
                {
                    ShowError("Кількість раундів має бути числом від 1 до 99.");
                    return;
                }

                GameData.UseRounds = true;
                GameData.MaxRounds = customRounds;
            }
        }

        // === Обробка кількості цифр ===
        Toggle selectedDigitsToggle = GetSelectedToggle(digitsToggleGroup);
        if (selectedDigitsToggle != null)
        {
            if (selectedDigitsToggle.name == "digits3Toggle") GameData.NumberLength = 3;
            else if (selectedDigitsToggle.name == "digits4Toggle") GameData.NumberLength = 4;
            else if (selectedDigitsToggle.name == "digits5Toggle") GameData.NumberLength = 5;
        }

        // === Збереження імен гравців ===
        GameData.Player1Name = name1;
        GameData.Player2Name = name2;

        // === Завантаження ігрової сцени ===
        SceneManager.LoadScene(gameSceneName);
    }

    // Перевіряє, чи вибрано хоча б один toggle у групі
    bool IsToggleGroupSelected(ToggleGroup group)
    {
        return group.AnyTogglesOn();
    }

    // Повертає вибраний toggle з групи
    Toggle GetSelectedToggle(ToggleGroup group)
    {
        foreach (var toggle in group.ActiveToggles())
        {
            return toggle;
        }
        return null;
    }

    // Показує повідомлення про помилку
    void ShowError(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
    }
}
