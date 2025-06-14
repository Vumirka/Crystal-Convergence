using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleInstruction : MonoBehaviour
{
    [Header("UI")]
    public GameObject instructionPanel;          // Панель з інструкцією, яка показується/ховається
    public TextMeshProUGUI instructionText;      // Текстове поле для відображення поточного кроку інструкції
    public Button nextButton;                     // Кнопка "Вперед" або "Закрити"
    public Button prevButton;                     // Кнопка "Назад"
    public Button openInstructionButton;         // Кнопка для відкриття інструкції з будь-якого місця

    private int currentStep = 0;                  // Індекс поточного кроку інструкції

    // Масив рядків — кроки інструкції
    private readonly string[] steps = new string[]
    {
        "Крімси — це цифри, які ти вгадав правильно і поставив на правильне місце.",
        "Неркси — це цифри, які є у загаданому числі, але стоять не на своїй позиції."
    };

    void Start()
    {
        instructionPanel.SetActive(false);  // Спочатку приховуємо панель інструкції

        // Відписуємо всі існуючі слухачі, щоб уникнути дублювань
        nextButton.onClick.RemoveAllListeners();
        prevButton.onClick.RemoveAllListeners();
        openInstructionButton.onClick.RemoveAllListeners();

        // Підписуємо кнопки на відповідні методи
        nextButton.onClick.AddListener(NextStep);
        prevButton.onClick.AddListener(PrevStep);
        openInstructionButton.onClick.AddListener(OpenInstructions);
    }

    // Відкриває панель інструкції і показує перший крок
    public void OpenInstructions()
    {
        currentStep = 0;
        instructionPanel.SetActive(true);
        UpdateInstruction();
    }

    // Переходить до наступного кроку або закриває інструкцію, якщо кроків більше немає
    private void NextStep()
    {
        if (currentStep < steps.Length - 1)
        {
            currentStep++;
            UpdateInstruction();
        }
        else
        {
            instructionPanel.SetActive(false);  // Закриваємо інструкцію
        }
    }

    // Повертається до попереднього кроку, якщо він існує
    private void PrevStep()
    {
        if (currentStep > 0)
        {
            currentStep--;
            UpdateInstruction();
        }
    }

    // Оновлює текст інструкції та стан кнопок відповідно до поточного кроку
    private void UpdateInstruction()
    {
        instructionText.text = steps[currentStep];

        if (currentStep == 0)
        {
            prevButton.gameObject.SetActive(false);  // На першому кроці кнопка "Назад" неактивна
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Вперед";
        }
        else if (currentStep == 1)
        {
            prevButton.gameObject.SetActive(true);   // На другому кроці кнопка "Назад" активна
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Закрити";
        }
    }
}
