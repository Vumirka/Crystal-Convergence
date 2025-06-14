/*
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class PlayerNameUI : MonoBehaviour
{
    [Header("Main UI")]
    public TMP_InputField nameInputField;
    public Button generateNameButton;
    public Button saveNameButton;
    public TMP_Text savedNameText;

    [Header("Error Panel")]
    public GameObject errorPanel;
    public TMP_Text errorText;
    public Button closeErrorButton;

    private List<string> ukrainianNames = new List<string>
    {
        "Андрій", "Марія", "Олена", "Тарас", "Іван", "Оксана", "Юрій", "Катерина", "Назар", "Лілія"
    };

    private const int MaxNameLength = 11;
    private Regex ukrainianRegex = new Regex("^[А-ЯІЇЄҐа-яіїєґ']+$");

    void Start()
    {
        generateNameButton.onClick.AddListener(GenerateRandomName);
        saveNameButton.onClick.AddListener(SaveName);
        closeErrorButton.onClick.AddListener(() => errorPanel.SetActive(false));

        errorPanel.SetActive(false);

        // Завантажити ім’я, якщо воно вже є
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            string savedName = PlayerPrefs.GetString("PlayerName");
            savedNameText.text = "Ім’я: " + savedName;
        }
    }

    void GenerateRandomName()
    {
        int index = Random.Range(0, ukrainianNames.Count);
        nameInputField.text = ukrainianNames[index];
    }

    void SaveName()
    {
        string inputName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(inputName))
        {
            ShowError("Будь ласка, введіть ім’я.");
            return;
        }

        if (inputName.Length > MaxNameLength)
        {
            ShowError("Ім’я повинно містити не більше 11 символів.");
            return;
        }

        if (!ukrainianRegex.IsMatch(inputName))
        {
            ShowError("Ім’я має містити лише українські літери.");
            return;
        }

        // Успішно
        PlayerPrefs.SetString("PlayerName", inputName);
        savedNameText.text = ": " + inputName;
        Debug.Log("Ім’я збережено: " + inputName);
    }

    void ShowError(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
    }
}
*/