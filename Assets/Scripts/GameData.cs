using System.Collections.Generic;

public static class GameData
{
    // Імена гравців, що зберігаються для доступу між сценами
    public static string Player1Name;
    public static string Player2Name;

    // Довжина секретного коду (3,4 або 5 цифр)
    public static int NumberLength;

    // Секретні коди, які гравці вводять на початку гри
    public static string SecretCode1;
    public static string SecretCode2;

    // Хто починає гру: 1 - гравець 1, 2 - гравець 2, 0 - випадковий вибір
    public static int StartingPlayer = 0;

    // Тривалість таймера в секундах: 0 - без таймера, 30 або 60 сек
    public static int TimerDuration = 0;

    // Чи використовується обмеження на кількість раундів
    public static bool UseRounds = false;

    // Максимальна кількість раундів, якщо обмеження включене (наприклад, 6)
    public static int MaxRounds = 0;

    // Клас для збереження статистики одного гравця в грі
    public class GameStat
    {
        public string playerName;  // Ім’я гравця
        public int attempts;       // Кількість спроб
        public bool isWin;         // Чи виграв гравець (true = перемога)

        public GameStat(string name, int attempts, bool isWin)
        {
            this.playerName = name;
            this.attempts = attempts;
            this.isWin = isWin;
        }
    }

    // Список статистики останньої зіграної гри для двох гравців
    public static List<GameStat> lastGameStats = new List<GameStat>();
}
