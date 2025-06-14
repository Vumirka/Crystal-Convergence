using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_InputField guessInput;        // Поле, куди гравець вводить свою спробу
    [SerializeField] private TMP_Text[] crimsTexts;            // Ліва колонка: кількість Крімсів
    [SerializeField] private TMP_Text[] guessTexts;            // Центральна колонка: введені гравцем числа
    [SerializeField] private TMP_Text[] nerksTexts;            // Права колонка: кількість Нерксів
    [SerializeField] private GameObject errorPanel;            // Панель помилки при некоректному вводі
    [SerializeField] private GameObject victoryPanel;          // Панель перемоги, коли гравець вгадав число
    [SerializeField] private TMP_Text victoryNumberText;       // Виводить загадане число після перемоги
    [SerializeField] private TMP_Text totalAttemptsText;       // Показує кількість спроб
    [SerializeField] private GameObject settingsPanel;         // Панель налаштувань

    private string secretNumber;                               // Загадане 4-значне число без повторів
    private List<GuessRecord> guesses = new List<GuessRecord>(); // Список історії спроб
    private int attemptCount = 0;                              // Лічильник спроб

    // Клас для збереження однієї спроби (число + кількість крімсів і нерксів)
    private class GuessRecord
    {
        public string guessText;
        public int crims;
        public int nerks;
        public GuessRecord(string text, int cr, int nr)
        {
            guessText = text;
            crims = cr;
            nerks = nr;
        }
    }

    void Start()
    {
        GenerateSecretNumber(); // Генеруємо нове число при старті
        UpdateHistoryUI();      // Оновлюємо інтерфейс історії
    }

    // Створюємо випадкове 4-значне число з унікальними цифрами (перша ≠ 0)
    private void GenerateSecretNumber()
    {
        List<int> digits = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int firstDigit = Random.Range(1, 10); // Перша цифра не може бути нулем
        secretNumber = firstDigit.ToString();
        digits.Remove(firstDigit);

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, digits.Count);
            secretNumber += digits[index].ToString();
            digits.RemoveAt(index);
        }

        // Debug.Log("Секретне число: " + secretNumber);
    }

    // Перевіряємо, що ввів гравець
    public void CheckGuess()
    {
        string guess = guessInput.text.Trim();

        // Перевірка на коректність введення
        if (guess.Length != 4 || !IsAllDigits(guess) || guess[0] == '0' || HasDuplicateDigits(guess))
        {
            ShowErrorPanel(); // Якщо неправильне введення — показуємо помилку
            return;
        }

        // Підрахунок Крімсів і Нерксів
        int crims = 0;
        int nerks = 0;
        for (int i = 0; i < 4; i++)
        {
            if (guess[i] == secretNumber[i])
            {
                crims++; // Та сама цифра на тому самому місці
            }
            else if (secretNumber.Contains(guess[i].ToString()))
            {
                nerks++; // Та сама цифра, але не на тому місці
            }
        }

        attemptCount++; // Збільшуємо лічильник спроб

        // Додаємо спробу до історії
        guesses.Insert(0, new GuessRecord(guess, crims, nerks));
        if (guesses.Count > 4) // Обмежуємо історію останніми 4 спробами
        {
            guesses.RemoveAt(4);
        }
        UpdateHistoryUI(); // Оновлюємо вивід

        guessInput.text = ""; // Очищаємо поле вводу

        // Якщо всі 4 цифри правильні і на своїх місцях — перемога
        if (crims == 4)
        {
            ShowVictoryPanel();
        }
    }

    // Оновлення історії на екрані (лівий, центр, правий стовпці)
    private void UpdateHistoryUI()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < guesses.Count)
            {
                crimsTexts[i].text = guesses[i].crims.ToString();
                guessTexts[i].text = guesses[i].guessText;
                nerksTexts[i].text = guesses[i].nerks.ToString();
            }
            else
            {
                crimsTexts[i].text = "";
                guessTexts[i].text = "";
                nerksTexts[i].text = "";
            }
        }
    }

    // Показуємо панель помилки
    private void ShowErrorPanel()
    {
        errorPanel.SetActive(true);
    }

    // Закриваємо панель помилки (при натисканні кнопки)
    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false);
    }

    // Показуємо панель перемоги і результати
    private void ShowVictoryPanel()
    {
        victoryNumberText.text = secretNumber;              // Показуємо секретне число
        totalAttemptsText.text = "" + attemptCount.ToString(); // Показуємо скільки спроб було
        victoryPanel.SetActive(true);                       // Активуємо панель перемоги
    }

    // Перевірка: всі символи — цифри?
    private bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }

    // Перевірка на повтори в числі
    private bool HasDuplicateDigits(string s)
    {
        HashSet<char> set = new HashSet<char>();
        foreach (char c in s)
        {
            if (!set.Add(c)) // Якщо вже була така цифра — дубль
                return true;
        }
        return false;
    }

    // Кнопка "Продовжити" — запускає нову гру
    public void OnContinueButton()
    {
        attemptCount = 0;
        guesses.Clear();
        guessInput.text = "";
        GenerateSecretNumber();
        UpdateHistoryUI();
        victoryPanel.SetActive(false);
    }

    // Кнопка "Налаштування" — відкриває панель
    public void OnSettingsButton()
    {
        settingsPanel.SetActive(true);
    }

    // Кнопка "Вийти до меню" — повертає на сцену з меню
    public void OnExitToMenuButton()
    {
        SceneManager.LoadScene("MainMenu"); // Назва сцени меню
    }

    // Кнопка "Назад" з налаштувань
    public void OnBackFromSettingsButton()
    {
        settingsPanel.SetActive(false); // Просто ховаємо панель
    }
}
