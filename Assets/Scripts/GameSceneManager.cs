using UnityEngine;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    // Поля для відображення імен гравців на ігровій сцені
    public TextMeshProUGUI player1NameText; // Тут буде показано ім’я першого гравця
    public TextMeshProUGUI player2NameText; // Тут буде показано ім’я другого гравця

    void Start()
    {
        // Коли завантажується сцена, автоматично підставляю збережені імена з GameData
        player1NameText.text = GameData.Player1Name;
        player2NameText.text = GameData.Player2Name;
    }
}
