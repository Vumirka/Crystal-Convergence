using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeEntryPanel : MonoBehaviour
{
    [Header("UI елементи")]
    public TextMeshProUGUI player1LabelText;  // Текст з інструкцією для гравця 1
    public TextMeshProUGUI player2LabelText;  // Текст з інструкцією для гравця 2
    public TMP_InputField player1CodeInput;   // Поле введення секретного коду гравцем 1
    public TMP_InputField player2CodeInput;   // Поле введення секретного коду гравцем 2
    public Button confirmButton;               // Кнопка підтвердження введених кодів

    [Header("Панель введення кодів")]
    public GameObject codePanel;               // Панель, де гравці вводять коди

    [Header("Наступна панель після введення кодів")]
    public GameObject nextPanel;               // Панель, яка відкривається після введення та підтвердження кодів

    [Header("Панель помилки")]
    public GameObject errorPanel;              // Панель для виводу помилок
    public TextMeshProUGUI errorText;         // Текст з описом помилки
    public Button closeErrorButton;            // Кнопка закриття панелі помилки

    private int expectedDigits;                // Очікувана довжина коду (3,4 або 5)
    private string player1Name;                // Ім'я гравця 1 (з GameData)
    private string player2Name;                // Ім'я гравця 2 (з GameData)

    // Збережені коди після успішного введення (закриті для запису ззовні)
    public string secretCode1 { get; private set; }
    public string secretCode2 { get; private set; }

    void Start()
    {
        // Прив’язуємо закриття панелі помилки до кнопки
        closeErrorButton.onClick.AddListener(() => errorPanel.SetActive(false));
        // Прив’язуємо обробник кнопки підтвердження
        confirmButton.onClick.AddListener(OnConfirmClicked);

        // Завантажуємо з GameData необхідні дані для відображення
        expectedDigits = GameData.NumberLength;
        player1Name = GameData.Player1Name;
        player2Name = GameData.Player2Name;

        // Відображаємо інструкції для кожного гравця з ім'ям і кількістю цифр
        player1LabelText.text = $"Гравець {player1Name}, введіть {expectedDigits}-значне число";
        player2LabelText.text = $"Гравець {player2Name}, введіть {expectedDigits}-значне число";

        // Маскуємо введення коду (показуємо зірочки замість цифр)
        player1CodeInput.contentType = TMP_InputField.ContentType.Password;
        player2CodeInput.contentType = TMP_InputField.ContentType.Password;
    }

    // Метод, який виконується при натисканні кнопки підтвердження
    void OnConfirmClicked()
    {
        // Отримуємо введені коди та обрізаємо пробіли
        string code1 = player1CodeInput.text.Trim();
        string code2 = player2CodeInput.text.Trim();

        // Перевірка коректності коду гравця 1
        if (!IsValidCode(code1))
        {
            ShowError($"Гравець {player1Name} ввів неправильний код.");
            return;
        }

        // Перевірка коректності коду гравця 2
        if (!IsValidCode(code2))
        {
            ShowError($"Гравець {player2Name} ввів неправильний код.");
            return;
        }

        // Якщо в налаштуваннях вибрано випадкового гравця, обираємо тут
        if (GameData.StartingPlayer == 0)
            GameData.StartingPlayer = Random.Range(1, 3); // 1 або 2

        // Зберігаємо введені коди і в локальні змінні, і в GameData для подальшого доступу
        secretCode1 = code1;
        secretCode2 = code2;

        GameData.SecretCode1 = code1;
        GameData.SecretCode2 = code2;

        // Приховуємо панель введення кодів
        codePanel.SetActive(false);

        // Відкриваємо наступну панель — це може бути екран початку гри або подальші інструкції
        nextPanel.SetActive(true); // <<< Ось тут відкриваємо наступну панель
    }

    // Метод перевірки валідності введеного коду
    bool IsValidCode(string code)
    {
        // Перевірка довжини коду
        if (code.Length != expectedDigits) return false;

        // Перша цифра не може бути 0
        if (code[0] == '0') return false;

        // Перевірка унікальності цифр (немає повторів)
        for (int i = 0; i < code.Length; i++)
        {
            for (int j = i + 1; j < code.Length; j++)
            {
                if (code[i] == code[j]) return false;
            }
        }

        return true; // Якщо всі перевірки пройшли — код валідний
    }

    // Вивід повідомлення про помилку на екран
    void ShowError(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
    }
}
