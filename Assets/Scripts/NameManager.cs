using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

// Цей скрипт відповідає за введення/генерацію/перевірку імен гравців та відображення помилок
public class NameManager : MonoBehaviour
{
    [Header("Гравець 1")]
    public TMP_InputField player1Input;              // Поле для вводу імені гравця 1
    public TextMeshProUGUI player1NameText;          // Текст, де відображатиметься збережене ім’я гравця 1
    public Button generatePlayer1Button;             // Кнопка "Згенерувати ім’я" для гравця 1
    public Button savePlayer1Button;                 // Кнопка "Зберегти ім’я" для гравця 1

    [Header("Гравець 2")]
    public TMP_InputField player2Input;              // Поле для вводу імені гравця 2
    public TextMeshProUGUI player2NameText;          // Текст, де відображатиметься збережене ім’я гравця 2
    public Button generatePlayer2Button;             // Кнопка "Згенерувати ім’я" для гравця 2
    public Button savePlayer2Button;                 // Кнопка "Зберегти ім’я" для гравця 2

    [Header("Панель помилки")]
    public GameObject errorPanel;                    // Панель, яка з’являється при помилці
    public TextMeshProUGUI errorText;                // Текст помилки, який виводиться на панелі
    public Button closeErrorButton;                  // Кнопка "Закрити" на панелі помилки

    // Список випадкових українських імен для генерації
    private string[] ukrainianNames = {
    // Чоловічі імена
        "Іван", "Тарас", "Юрій", "Артем", "Мирон", "Олег", "Сергій", "Богдан", "Дмитро", "Олександр", "Андрій", "Ростислав", "Віталій", "Максим", "Євген", "Роман", "Володимир", "Ігор", "Назар", "Леонід",
    
    // Жіночі імена
        "Оля", "Марія", "Леся", "Світлана", "Зоряна", "Наталя", "Ірина", "Ганна", "Катерина", "Оксана", "Тетяна", "Людмила", "Юлія", "Ярина", "Дарина", "Софія", "Вероніка", "Єва", "Алла", "Ніна"    };

    // Метод, що запускається при старті сцени
    void Start()
    {
        // Призначаю дії для кнопок генерації імен
        generatePlayer1Button.onClick.AddListener(() => GenerateName(player1Input));
        generatePlayer2Button.onClick.AddListener(() => GenerateName(player2Input));

        // Призначаю дії для кнопок збереження імен
        savePlayer1Button.onClick.AddListener(() => SaveName(player1Input, player1NameText));
        savePlayer2Button.onClick.AddListener(() => SaveName(player2Input, player2NameText));

        // Призначаю дію для кнопки закриття помилки
        closeErrorButton.onClick.AddListener(CloseErrorPanel);
    }

    // Генерує випадкове ім’я та вставляє його у відповідне поле
    void GenerateName(TMP_InputField input)
    {
        int randIndex = Random.Range(0, ukrainianNames.Length); // Випадковий індекс
        input.text = ukrainianNames[randIndex];                 // Вставляю ім’я в поле
    }

    // Перевіряє та зберігає ім’я, якщо воно правильне
    void SaveName(TMP_InputField input, TextMeshProUGUI nameDisplay)
    {
        string name = input.text.Trim(); // Видаляю зайві пробіли на початку/кінці

        // Якщо ім’я не проходить перевірку — показуємо помилку
        if (!IsValidName(name))
        {
            ShowError("Ім’я має містити лише українські літери.");
            return;
        }

        // Якщо все ок — відображаємо ім’я у відповідному полі
        nameDisplay.text = name;
    }

    // Перевірка імені на коректність (тільки українські літери, довжина від 2 до 11)
    bool IsValidName(string name)
    {
        if (name.Length < 2 || name.Length > 11)
            return false;

        string pattern = @"^[А-ЩЬЮЯЄІЇҐа-щьюяєіїґ]+$"; // Тільки українські літери
        return Regex.IsMatch(name, pattern);
    }

    // Відображає панель помилки з переданим повідомленням
    void ShowError(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
    }

    // Закриває панель помилки
    void CloseErrorPanel()
    {
        errorPanel.SetActive(false);
    }
}
