/*
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class JoinByCodeUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField joinCodeInput;
    public Button joinButton;

    [Header("Error Panel")]
    public GameObject errorPanel;
    public TMP_Text errorText;
    public Button closeErrorButton;

    private Regex codeRegex = new Regex("^[A-Z0-9]{1,15}$");

    void Start()
    {
        joinButton.onClick.AddListener(JoinLobbyByCode);
        closeErrorButton.onClick.AddListener(() => errorPanel.SetActive(false));
        errorPanel.SetActive(false);

        // Автоматично переводимо введений текст у верхній регістр
        joinCodeInput.onValueChanged.AddListener(OnCodeInputChanged);
    }

    void OnCodeInputChanged(string input)
    {
        joinCodeInput.text = input.ToUpper();
    }

    void JoinLobbyByCode()
    {
        string code = joinCodeInput.text.Trim().ToUpper();
        string playerName = PlayerPrefs.GetString("PlayerName", "");

        if (string.IsNullOrEmpty(playerName))
        {
            ShowError("Спочатку введи ім'я гравця в налаштуваннях.");
            return;
        }

        if (string.IsNullOrEmpty(code))
        {
            ShowError("Введи код лобі.");
            return;
        }

        if (!codeRegex.IsMatch(code))
        {
            ShowError("Код має містити лише латинські великі літери та цифри (до 15 символів).");
            return;
        }

        if (LobbyManager.Instance != null)
        {
            // Перевіряємо чи існує лобі
            LobbyData lobby = LobbyManager.Instance.FindLobbyByCode(code);
            if (lobby == null)
            {
                ShowError("Лобі з таким кодом не знайдено.");
                return;
            }

            if (lobby.IsFull())
            {
                ShowError("Це лобі вже заповнене.");
                return;
            }

            // Приєднуємося до лобі
            bool success = LobbyManager.Instance.JoinLobby(code);
            if (success)
            {
                Debug.Log($"Успішно приєднався до лобі: {lobby.name} ({code})");
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                ShowError("Не вдалося приєднатися до лобі.");
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
}
*/