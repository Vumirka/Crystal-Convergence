using UnityEngine;
using TMPro; // Підключаю TMP для роботи з текстовими елементами TextMeshProUGUI

// Цей скрипт виводить статистику останньої гри (імена, кількість спроб, результат)
public class StatsDisplay : MonoBehaviour
{
    [Header("Рядок 1")] // Елементи для гравця 1
    public TextMeshProUGUI player1NameText;     // Поле для виводу імені гравця 1
    public TextMeshProUGUI player1AttemptsText; // Поле для виводу кількості спроб гравця 1
    public TextMeshProUGUI player1ResultText;   // Поле для виводу результату гравця 1 (перемога/поразка)

    [Header("Рядок 2")] // Елементи для гравця 2
    public TextMeshProUGUI player2NameText;     // Поле для виводу імені гравця 2
    public TextMeshProUGUI player2AttemptsText; // Поле для виводу кількості спроб гравця 2
    public TextMeshProUGUI player2ResultText;   // Поле для виводу результату гравця 2 (перемога/поразка)

    // Метод викликається автоматично при запуску об'єкта
    void Start()
    {
        ShowLastGameStats(); // Виводимо статистику останньої гри
    }

    // Метод для заповнення текстових полів статистикою з GameData
    void ShowLastGameStats()
    {
        // Перевірка, чи є щонайменше 2 записи в списку GameData.lastGameStats
        if (GameData.lastGameStats.Count >= 2)
        {
            // Отримую дані гравця 1 і гравця 2
            var p1 = GameData.lastGameStats[0];
            var p2 = GameData.lastGameStats[1];

            // Заповнюю текстові поля для гравця 1
            player1NameText.text = p1.playerName;
            player1AttemptsText.text = p1.attempts.ToString(); // Перетворюю число в текст
            player1ResultText.text = p1.isWin ? "Перемога" : "Поразка"; // Якщо виграв — пише "Перемога", інакше — "Поразка"

            // Заповнюю текстові поля для гравця 2
            player2NameText.text = p2.playerName;
            player2AttemptsText.text = p2.attempts.ToString();
            player2ResultText.text = p2.isWin ? "Перемога" : "Поразка";
        }
    }
}
