/*
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LobbyUIcreat : MonoBehaviour
{
    [Header("Inputs")]
    public TMP_InputField lobbyNameInput;
    public TMP_InputField lobbyCodeInput;

    [Header("Buttons")]
    public Button generateNameButton;
    public Button generateCodeButton;
    public Button publicLobbyButton;
    public Button privateLobbyButton;
    public Button createLobbyButton;

    [Header("Error Panel")]
    public GameObject errorPanel;
    public TMP_Text errorText;
    public Button closeErrorButton;



    private bool isPublic = true;
    private const int MaxLobbyLength = 15;
    private Regex lobbyCodeRegex = new Regex("^[A-Z0-9]+$");
    private Regex lobbyNameRegex = new Regex("^[А-ЯІЇЄҐа-яіїєґ' ]+$");

    private List<string> randomLobbyNames = new List<string>
    {
        "Весела Хата", "Бойовий Гусак", "Зоряний Козак", "Сонячне Поле", "Бандерівська Фортеця",
        "Грім", "Сяйво", "Полум'я", "Кобза", "Шторм", "Буревій", "Сокіл", "Туман"
    };

    void Start()
    {
        generateNameButton.onClick.AddListener(GenerateLobbyName);
        generateCodeButton.onClick.AddListener(GenerateLobbyCode);
        publicLobbyButton.onClick.AddListener(() => SetLobbyType(true));
        privateLobbyButton.onClick.AddListener(() => SetLobbyType(false));
        createLobbyButton.onClick.AddListener(CreateLobby);
        closeErrorButton.onClick.AddListener(() => errorPanel.SetActive(false));


        errorPanel.SetActive(false);


        SetLobbyType(true); // За замовчуванням публічне
    }

    void GenerateLobbyName()
    {
        int index = Random.Range(0, randomLobbyNames.Count);
        lobbyNameInput.text = randomLobbyNames[index];
    }

    void GenerateLobbyCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string code = "";
        for (int i = 0; i < 8; i++)
        {
            code += chars[Random.Range(0, chars.Length)];
        }
        lobbyCodeInput.text = code;
    }

    void SetLobbyType(bool isPublicLobby)
    {
        isPublic = isPublicLobby;
        // Візуальна індикація
        Color activeColor = Color.green;
        Color inactiveColor = Color.white;

        publicLobbyButton.image.color = isPublic ? activeColor : inactiveColor;
        privateLobbyButton.image.color = isPublic ? inactiveColor : activeColor;
    }

    void CreateLobby()
    {
        string name = lobbyNameInput.text.Trim();
        string code = lobbyCodeInput.text.Trim().ToUpper();

        // Перевірка імені гравця
        string playerName = PlayerPrefs.GetString("PlayerName", "");
        if (string.IsNullOrEmpty(playerName))
        {
            ShowError("Спочатку введи ім'я гравця в налаштуваннях.");
            return;
        }

        // Валідація назви лобі
        if (string.IsNullOrEmpty(name) || name.Length > MaxLobbyLength || !lobbyNameRegex.IsMatch(name))
        {
            ShowError("Невірна назва лобі. Тільки українські літери, максимум 15 символів.");
            return;
        }

        // Валідація коду лобі
        if (string.IsNullOrEmpty(code) || code.Length > MaxLobbyLength || !lobbyCodeRegex.IsMatch(code))
        {
            ShowError("Невірний код лобі. Лише великі латинські літери та цифри, максимум 15 символів.");
            return;
        }

        // Перевіряємо чи є LobbyManager
        if (LobbyManager.Instance == null)
        {
            ShowError("Помилка: LobbyManager не знайдено!");
            return;
        }

        // Створюємо лобі
        bool success = LobbyManager.Instance.CreateLobby(name, code, isPublic);
        if (success)
        {
            Debug.Log($"Успішно створено лобі: Назва={name}, Код={code}, Публічне={isPublic}");
            Debug.Log($"Лобі '{name}' створено! Код: {code} Тип: {(isPublic ? "Публічне" : "Приватне")}");


            // Очищуємо поля
            lobbyNameInput.text = "";
            lobbyCodeInput.text = "";

            // TODO: Перехід до лобі або ігрової сцени
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            ShowError("Не вдалося створити лобі. Код вже існує!");
        }
    }

    void ShowError(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
    }

}
*/