/*
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PublicLobbiesUI : MonoBehaviour
{
    [System.Serializable]
    public class LobbyEntryUI
    {
        public GameObject root;
        public TMP_Text nameText;
        public TMP_Text playersText;
        public TMP_Text codeText;
        public Button joinButton;
    }

    [Header("UI References")]
    public List<LobbyEntryUI> lobbyEntries;
    public Button refreshButton;

    [Header("Error Panel")]
    public GameObject errorPanel;
    public TMP_Text errorText;
    public Button closeErrorButton;

    private void Start()
    {
        refreshButton.onClick.AddListener(RefreshLobbies);
        closeErrorButton.onClick.AddListener(() => errorPanel.SetActive(false));

        errorPanel.SetActive(false);
        RefreshLobbies();
    }

    void RefreshLobbies()
    {
        List<LobbyData> publicLobbies = new List<LobbyData>();

        if (LobbyManager.Instance != null)
        {
            publicLobbies = LobbyManager.Instance.GetPublicLobbies();
        }
        else
        {
            // Якщо LobbyManager не існує, створюємо тестові дані
            publicLobbies = GenerateMockLobbies();
        }

        DisplayLobbies(publicLobbies);
    }

    void DisplayLobbies(List<LobbyData> lobbies)
    {
        for (int i = 0; i < lobbyEntries.Count; i++)
        {
            if (i < lobbies.Count)
            {
                var lobby = lobbies[i];
                var entry = lobbyEntries[i];

                entry.root.SetActive(true);
                entry.nameText.text = lobby.name;
                entry.playersText.text = $"{lobby.players}/{lobby.maxPlayers}";
                entry.codeText.text = lobby.code;

                entry.joinButton.interactable = !lobby.IsFull();
                entry.joinButton.onClick.RemoveAllListeners();

                string lobbyCode = lobby.code;
                entry.joinButton.onClick.AddListener(() => TryJoinLobby(lobbyCode));
                entry.joinButton.image.color = lobby.IsFull() ? Color.red : Color.green;
            }
            else
            {
                lobbyEntries[i].root.SetActive(false);
            }
        }
    }

    void TryJoinLobby(string code)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "");
        if (string.IsNullOrEmpty(playerName))
        {
            ShowError("Спочатку введи ім'я гравця в налаштуваннях.");
            return;
        }

        if (LobbyManager.Instance != null)
        {
            bool success = LobbyManager.Instance.JoinLobby(code);
            if (success)
            {
                Debug.Log($"Успішно приєднався до лобі з кодом: {code}");
                // TODO: Перехід до ігрової сцени
                // SceneManager.LoadScene("GameScene");
            }
            else
            {
                ShowError("Не вдалося приєднатися до лобі.");
                RefreshLobbies();
            }
        }
        else
        {
            ShowError("Помилка: LobbyManager не знайдено!");
        }
    }

    void ShowError(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
    }

    List<LobbyData> GenerateMockLobbies()
    {
        List<LobbyData> mockLobbies = new List<LobbyData>();

        mockLobbies.Add(new LobbyData("Грізна Вишня", "ABC123", true));
        mockLobbies.Add(new LobbyData("Козацький Бій", "QWE456", true));
        mockLobbies[1].players = 2;
        mockLobbies.Add(new LobbyData("Січ", "ZXC789", true));

        return mockLobbies;
    }
}
*/