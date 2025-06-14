/*
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuickStart : MonoBehaviour
{
    public Button quickStartButton;

    [Header("Error Panel")]
    public GameObject errorPanel;
    public TMP_Text errorText;
    public Button closeErrorButton;

    void Start()
    {
        quickStartButton.onClick.AddListener(OnQuickStart);
        closeErrorButton.onClick.AddListener(() => errorPanel.SetActive(false));

        errorPanel.SetActive(false);
    }

    void OnQuickStart()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "");
        if (string.IsNullOrEmpty(playerName))
        {
            ShowError("Спочатку введи ім'я гравця в налаштуваннях.");
            return;
        }

        Debug.Log("Шукаємо доступне лобі...");

        if (LobbyManager.Instance != null)
        {
            // Шукаємо доступні публічні лобі
            List<LobbyData> availableLobbies = LobbyManager.Instance.GetAvailablePublicLobbies();

            if (availableLobbies.Count > 0)
            {
                // Вибираємо випадкове доступне лобі
                LobbyData selectedLobby = availableLobbies[Random.Range(0, availableLobbies.Count)];

                bool success = LobbyManager.Instance.JoinLobby(selectedLobby.code);
                if (success)
                {
                    Debug.Log($"✅ Приєднався до лобі: {selectedLobby.name} ({selectedLobby.code})");
                    Debug.Log("Завантаження гри...");
                    // TODO: Перехід до ігрової сцени
                    // SceneManager.LoadScene("GameScene");
                    return;
                }
            }

            // Якщо немає доступних лобі - створюємо нове
            CreateNewQuickLobby();
        }
        else
        {
            ShowError("Помилка: LobbyManager не знайдено!");
        }
    }

    void CreateNewQuickLobby()
    {
        Debug.Log("Створюємо нове лобі...");

        string newLobbyName = GenerateRandomLobbyName();
        string newCode = GenerateRandomLobbyCode();

        // Переконуємось що код унікальний
        while (LobbyManager.Instance.FindLobbyByCode(newCode) != null)
        {
            newCode = GenerateRandomLobbyCode();
        }

        bool success = LobbyManager.Instance.CreateLobby(newLobbyName, newCode, true);
        if (success)
        {
            Debug.Log($"🆕 Створено нове публічне лобі: {newLobbyName} ({newCode})");
            Debug.Log("Завантаження гри...");
            // TODO: Перехід до ігрової сцени
            // SceneManager.LoadScene("GameScene");
        }
        else
        {
            ShowError("Не вдалося створити нове лобі.");
        }
    }

    void ShowError(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
    }

    string GenerateRandomLobbyName()
    {
        string[] names = {
            "Грім", "Сяйво", "Полум'я", "Кобза", "Шторм",
            "Буревій", "Сокіл", "Туман", "Блиск", "Зорепад"
        };
        return names[Random.Range(0, names.Length)];
    }

    string GenerateRandomLobbyCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] code = new char[6];
        for (int i = 0; i < code.Length; i++)
        {
            code[i] = chars[Random.Range(0, chars.Length)];
        }
        return new string(code);
    }
}
*/