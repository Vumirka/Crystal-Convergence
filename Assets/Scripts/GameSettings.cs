using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSettings : MonoBehaviour
{
    // ===== Налаштування: Хто починає гру =====
    public Toggle startPlayer1Toggle;     // Якщо ввімкнено — першим ходить Гравець 1
    public Toggle startPlayer2Toggle;     // Якщо ввімкнено — першим ходить Гравець 2
    public Toggle randomStartToggle;      // Якщо ввімкнено — перший хід випадковий

    // ===== Налаштування таймера на хід =====
    public Toggle timer30Toggle;          // Таймер на 30 секунд
    public Toggle timer60Toggle;          // Таймер на 60 секунд
    public Toggle noTimerToggle;          // Без таймера

    // ===== Кількість цифр у секретному числі =====
    public Toggle digits3Toggle;          // Грати з 3 цифрами
    public Toggle digits4Toggle;          // Грати з 4 цифрами (за замовчуванням)
    public Toggle digits5Toggle;          // Грати з 5 цифрами

    // ===== Кількість раундів =====
    public Toggle unlimitedRoundsToggle;  // Без обмеження за раундами
    public Toggle customRoundsToggle;     // Ввести власну кількість раундів
    public TMP_InputField customRoundsInput; // Поле для введення кількості раундів

    // ===== Панель з повідомленням про помилку =====
    public GameObject errorPanel;         // Панель, яка з'являється, якщо щось не так
    public TextMeshProUGUI errorText;     // Текст з повідомленням про помилку
    public Button closeErrorButton;       // Кнопка, щоб закрити панель помилки

    // ===== Змінні, в які записуються вибрані налаштування =====
    public int startingPlayer = 1;        // Хто починає гру: 1 або 2 (якщо випадково, буде вибрано через Random)
    public int moveTimer = 0;             // Тривалість таймера: 0 = без таймера, або 30, 60
    public int numberLength = 4;          // Кількість цифр у числі: 3, 4 або 5
    public int roundCount = -1;           // Кількість раундів: -1 = без обмежень

    void Start()
    {
        // Призначаю кнопку "Закрити" для панелі помилки
        closeErrorButton.onClick.AddListener(() => errorPanel.SetActive(false));
    }

    // Головний метод для перевірки і збереження вибраних налаштувань
    public bool ValidateAndSaveSettings()
    {
        // === Хто починає ===
        if (startPlayer1Toggle.isOn) startingPlayer = 1;
        else if (startPlayer2Toggle.isOn) startingPlayer = 2;
        else if (randomStartToggle.isOn) startingPlayer = Random.Range(1, 3); // Випадковий гравець (1 або 2)

        // === Таймер ===
        if (timer30Toggle.isOn) moveTimer = 30;
        else if (timer60Toggle.isOn) moveTimer = 60;
        else moveTimer = 0; // Якщо нічого не вибрано — вважаю, що без таймера

        // === Кількість цифр у числі ===
        if (digits3Toggle.isOn) numberLength = 3;
        else if (digits4Toggle.isOn) numberLength = 4;
        else if (digits5Toggle.isOn) numberLength = 5;

        // === Кількість раундів ===
        if (unlimitedRoundsToggle.isOn)
        {
            roundCount = -1; // -1 означає "без обмеження"
        }
        else if (customRoundsToggle.isOn)
        {
            string input = customRoundsInput.text.Trim(); // Витягаю текст з поля

            // Перевіряю чи число коректне
            if (int.TryParse(input, out int value))
            {
                if (value < 1 || value > 100)
                {
                    ShowError("Введіть кількість раундів від 1 до 100.");
                    return false; // Повертаю false, якщо некоректне число
                }
                roundCount = value; // Все добре — зберігаю
            }
            else
            {
                ShowError("Некоректне число раундів."); // Написано не число
                return false;
            }
        }

        return true; // Усі налаштування валідні — можна запускати гру
    }

    // Метод показує повідомлення про помилку
    void ShowError(string message)
    {
        errorText.text = message;         // Встановлюю текст помилки
        errorPanel.SetActive(true);       // Відкриваю панель
    }
}
