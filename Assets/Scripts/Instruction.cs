using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Instruction : MonoBehaviour
{
    [Header("Panels")]
    public GameObject instructionPanel;      // Панель, яка показує інструкцію
    public GameObject modeSelectionPanel;    // Панель з вибором режиму гри

    [Header("UI Elements")]
    public TextMeshProUGUI instructionText;  // Текст, де виводиться поточний крок інструкції
    public Button nextButton;                // Кнопка "Вперед"
    public Button prevButton;                // Кнопка "Назад"

    private int currentStep = 0;             // Поточний крок інструкції (індекс)

    // Масив з кроками інструкції — текст, який показується по черзі
    private readonly string[] steps = new string[]
    {
        "Вітаємо у грі!\r\nПеред початком — коротка інструкція:",
        "Крімси — це цифри, які ти вгадав правильно і поставив на правильне місце.",
        "Неркси — це цифри, які є у загаданому числі, але стоять не на своїй позиції.",
        "Тобі потрібно вгадати унікальне 4-значне число (перша цифра — не нуль). Вводь свої припущення і отримуй підказки у вигляді Крімсів і Нерксів.",
        "Готовий почати? Обери режим гри!"
    };

    private const string PREFS_KEY = "InstructionShown"; // Ключ у PlayerPrefs — зберігає, чи вже показували інструкцію

    void Start()
    {
        // Спочатку очищаю слухачі, щоб не задвоювались при повторному старті
        nextButton.onClick.RemoveAllListeners();
        prevButton.onClick.RemoveAllListeners();

        // Додаю методи, які викликаються при натисканні на кнопки
        nextButton.onClick.AddListener(NextStep);
        prevButton.onClick.AddListener(PrevStep);

        // Перевіряю, чи вже показували інструкцію (0 — ще ні, 1 — вже так)
        bool shownBefore = PlayerPrefs.GetInt(PREFS_KEY, 0) == 1;

        if (!shownBefore)
        {
            // Якщо інструкцію ще не бачили — показую її
            currentStep = 0;
            instructionPanel.SetActive(true);
            modeSelectionPanel.SetActive(false);
            UpdateInstruction();
        }
        else
        {
            // Якщо вже бачили — одразу показую вибір режиму
            instructionPanel.SetActive(false);
            modeSelectionPanel.SetActive(true);
        }
    }

    void Update()
    {
        // Додатково дозволяю переходити на наступний крок інструкції клавішею Enter
        if (instructionPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            NextStep();
        }
    }

    // Коли натискається кнопка "Вперед"
    public void NextStep()
    {
        if (currentStep < steps.Length - 1)
        {
            // Переходжу на наступний крок інструкції
            currentStep++;
            UpdateInstruction();
        }
        else
        {
            // Якщо це був останній крок — закриваю інструкцію і переходжу до режимів
            instructionPanel.SetActive(false);
            modeSelectionPanel.SetActive(true);

            // Запам’ятовую, що інструкцію вже показували
            PlayerPrefs.SetInt(PREFS_KEY, 1);
            PlayerPrefs.Save();
        }
    }

    // Метод, щоб вручну показати інструкцію (наприклад, з кнопки "Інструкція" в головному меню)
    public void ShowInstructionsManually()
    {
        currentStep = 0;
        instructionPanel.SetActive(true);
        modeSelectionPanel.SetActive(false);
        UpdateInstruction();
    }

    // Коли натискається кнопка "Назад"
    public void PrevStep()
    {
        if (currentStep > 0)
        {
            // Повертаюсь до попереднього кроку
            currentStep--;
            UpdateInstruction();
        }
    }

    // Оновлення тексту інструкції і стану кнопок відповідно до кроку
    private void UpdateInstruction()
    {
        instructionText.text = steps[currentStep]; // Показую текст відповідного кроку

        // Якщо це перший крок — ховаю кнопку "Назад", "Вперед" показує "Вперед"
        if (currentStep == 0)
        {
            prevButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Вперед";
        }
        // Якщо це проміжний крок — показую обидві кнопки
        else if (currentStep > 0 && currentStep < steps.Length - 1)
        {
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Вперед";
        }
        // Якщо останній крок — змінюю текст кнопки "Вперед" на "Закрити"
        else if (currentStep == steps.Length - 1)
        {
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Закрити";
        }
    }
}
