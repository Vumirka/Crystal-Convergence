using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text promptText;               // Текст, що показує, хто зараз ходить, наприклад "Гравець <Ім’я> вводить <N>-значне число"
    public TMP_InputField guessInput;         // Поле для введення числа-спроби від гравця

    [Header("History UI")]
    public TMP_Text[] crimsTextsPlayer1;      // Масив текстових полів для відображення кількості "Крімсів" (збігів за позицією) гравця 1 (ліва панель)
    public TMP_Text[] guessTextsPlayer1;      // Масив текстових полів для відображення введених чисел гравця 1
    public TMP_Text[] nerksTextsPlayer1;      // Масив текстових полів для відображення кількості "Нерксів" (збігів за цифрою, але не за позицією) гравця 1

    public TMP_Text[] crimsTextsPlayer2;      // Аналогічно для гравця 2 (права панель)
    public TMP_Text[] guessTextsPlayer2;
    public TMP_Text[] nerksTextsPlayer2;

    public GameObject errorPanel;              // Панель, що з'являється при помилці у введенні
    public TMP_Text errorText;                 // Текст повідомлення про помилку на панелі errorPanel
    public GameObject secretCodePanel;         // Панель введення секретних кодів (для прив’язки у інспекторі)

    private int roundCounterPlayer1 = 0;       // Лічильник раундів для гравця 1
    private int roundCounterPlayer2 = 0;       // Лічильник раундів для гравця 2

    private float timer;                       // Таймер для обмеження часу ходу
    private bool timerRunning;                 // Чи запущений таймер
    public TMP_Text timerText;                 // Текст для відображення часу (прикріпити у інспекторі)

    // *** Панель результатів гри ***
    [Header("Result Panel UI")]
    public GameObject resultPanel;             // Панель, що показує результати після завершення гри
    public TMP_Text victoryNameText;           // Текст для відображення імені переможця
    public TMP_Text victoryNumberText;         // Текст для відображення числа переможця (секретний код)
    public TMP_Text victoryAttemptsText;       // Текст для відображення кількості спроб переможця

    public TMP_Text defeatNameText;            // Аналогічно для програвшого
    public TMP_Text defeatNumberText;
    public TMP_Text defeatAttemptsText;

    public GameObject soundSettingsPanel;      // Панель налаштувань звуку

    private int currentPlayerTurn;              // Чий зараз хід (1 або 2)

    // Списки збережених спроб кожного гравця
    private List<GuessRecord> guessesPlayer1 = new List<GuessRecord>();
    private List<GuessRecord> guessesPlayer2 = new List<GuessRecord>();

    // Клас для зберігання даних про одну спробу
    private class GuessRecord
    {
        public string guessText;   // Введене число
        public int crims;          // Кількість Крімсів
        public int nerks;          // Кількість Нерксів

        public GuessRecord(string guess, int cr, int nr)
        {
            guessText = guess;
            crims = cr;
            nerks = nr;
        }
    }

    void Start()
    {
        // Визначаємо, хто ходить першим: або випадково, або з налаштувань
        if (GameData.StartingPlayer == 0)
        {
            currentPlayerTurn = Random.Range(1, 3);
        }
        else
        {
            currentPlayerTurn = GameData.StartingPlayer;
        }

        UpdateTimerUI(); // Оновлюємо UI таймера (щоб очистити текст)

        UpdatePrompt();      // Оновлюємо текст підказки, хто ходить
        UpdateHistoryUI();   // Оновлюємо історію спроб для обох гравців
    }

    // Запускає таймер, якщо він увімкнений у налаштуваннях
    private void StartTimerIfNeeded()
    {
        if (GameData.TimerDuration > 0)
        {
            timer = GameData.TimerDuration;
            timerRunning = true;
            UpdateTimerUI();
        }
    }

    void Update()
    {
        // Якщо таймер працює, оновлюємо його щокадрово
        if (timerRunning)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI();

            if (timer <= 0)
            {
                timerRunning = false;
                HandleTimerEnd();   // Якщо час вичерпано — обробляємо цей випадок
            }
        }
    }

    // Оновлює текст таймера у UI
    void UpdateTimerUI()
    {
        if (GameData.TimerDuration > 0 && timerRunning)
        {
            int seconds = Mathf.CeilToInt(timer);
            timerText.text = $": {seconds}";
        }
        else
        {
            // Якщо таймер вимкнено — очищаємо текст, щоб не показувати ": 0"
            timerText.text = "";
        }
    }

    // Скидає таймер на початковий час та запускає його
    void ResetTimer()
    {
        if (GameData.TimerDuration > 0)
        {
            timer = GameData.TimerDuration;
            timerRunning = true;
            UpdateTimerUI();
        }
    }

    // Обробка події підтвердження введеної спроби
    public void OnConfirmGuess()
    {
        string guess = guessInput.text.Trim();  // Отримуємо введене число і обрізаємо пробіли

        if (!IsValidGuess(guess))  // Перевіряємо валідність спроби
            return;

        int crims, nerks;
        CalculateCrimsAndNerks(guess, out crims, out nerks); // Обчислюємо кількість Крімсів і Нерксів

        AddGuessRecord(currentPlayerTurn, guess, crims, nerks);  // Додаємо спробу в історію

        // Збільшуємо лічильник раундів відповідного гравця
        if (currentPlayerTurn == 1)
            roundCounterPlayer1++;
        else
            roundCounterPlayer2++;

        // Якщо увімкнене обмеження по кількості раундів, перевіряємо чи не вичерпано ліміт
        if (GameData.UseRounds)
        {
            if ((currentPlayerTurn == 1 && roundCounterPlayer1 >= GameData.MaxRounds) ||
                (currentPlayerTurn == 2 && roundCounterPlayer2 >= GameData.MaxRounds))
            {
                ShowError($"Гравець {GetPlayerName(currentPlayerTurn)} вичерпав раунди.");
                SwitchTurn();        // Передаємо хід іншому гравцю
                UpdatePrompt();      // Оновлюємо підказку
                ResetTimer();        // Скидаємо таймер
                return;
            }
        }

        UpdateHistoryUI();    // Оновлюємо відображення історії спроб

        guessInput.text = "";  // Очищаємо поле вводу
        ResetTimer();          // Скидаємо таймер

        if (crims == GameData.NumberLength)
        {
            // Якщо гравець вгадав код повністю — показуємо панель перемоги
            ShowResultPanel(currentPlayerTurn);
        }
        else
        {
            SwitchTurn();      // Інакше передаємо хід наступному гравцю
            UpdatePrompt();
        }
    }

    // Обробка події закінчення таймера — хід пропускається
    void HandleTimerEnd()
    {
        ShowError($"Час вичерпано. Хід {GetPlayerName(currentPlayerTurn)} пропущено.");
        SwitchTurn();      // Передаємо хід іншому гравцю
        UpdatePrompt();
        ResetTimer();
    }

    // Перевірка чи є введене число валідним (довжина, цифри, без повторів, не починається з 0)
    private bool IsValidGuess(string guess)
    {
        if (guess.Length != GameData.NumberLength)
        {
            ShowError("Довжина числа має бути " + GameData.NumberLength);
            return false;
        }
        if (guess[0] == '0')
        {
            ShowError("Перша цифра не може бути нулем");
            return false;
        }
        if (!IsAllDigits(guess))
        {
            ShowError("Введіть тільки цифри");
            return false;
        }
        if (HasDuplicateDigits(guess))
        {
            ShowError("Цифри не повинні повторюватися");
            return false;
        }
        return true;
    }

    // Обчислення кількості Крімсів і Нерксів для введеної спроби
    private void CalculateCrimsAndNerks(string guess, out int crims, out int nerks)
    {
        crims = 0;
        nerks = 0;

        // Отримуємо секретний код суперника
        string secretCode = currentPlayerTurn == 1 ? GameData.SecretCode2 : GameData.SecretCode1;
        // Гравець 1 вгадує код гравця 2 і навпаки

        for (int i = 0; i < guess.Length; i++)
        {
            if (guess[i] == secretCode[i])
                crims++;     // Цифра і позиція збігаються
            else if (secretCode.Contains(guess[i].ToString()))
                nerks++;     // Цифра є, але позиція інша
        }
    }

    // Додає новий запис про спробу у відповідний список гравця (зберігаємо максимум 5 останніх)
    private void AddGuessRecord(int player, string guess, int crims, int nerks)
    {
        GuessRecord newRecord = new GuessRecord(guess, crims, nerks);

        if (player == 1)
        {
            guessesPlayer1.Insert(0, newRecord);  // Вставляємо на початок списку (останній хід зверху)
            if (guessesPlayer1.Count > 4)
                guessesPlayer1.RemoveAt(4);       // Обмежуємо довжину списку (макс 5)
        }
        else
        {
            guessesPlayer2.Insert(0, newRecord);
            if (guessesPlayer2.Count > 4)
                guessesPlayer2.RemoveAt(4);
        }
    }

    // Оновлює UI історії спроб обох гравців
    private void UpdateHistoryUI()
    {
        UpdatePlayerHistoryUI(guessesPlayer1, crimsTextsPlayer1, guessTextsPlayer1, nerksTextsPlayer1);
        UpdatePlayerHistoryUI(guessesPlayer2, crimsTextsPlayer2, guessTextsPlayer2, nerksTextsPlayer2);
    }

    // Оновлює UI історії спроб конкретного гравця — заповнює текстові поля або очищує їх
    private void UpdatePlayerHistoryUI(List<GuessRecord> guesses, TMP_Text[] crimsTexts, TMP_Text[] guessTexts, TMP_Text[] nerksTexts)
    {
        for (int i = 0; i < crimsTexts.Length; i++)
        {
            if (i < guesses.Count)
            {
                crimsTexts[i].text = guesses[i].crims.ToString();
                guessTexts[i].text = guesses[i].guessText;
                nerksTexts[i].text = guesses[i].nerks.ToString();
            }
            else
            {
                crimsTexts[i].text = "";
                guessTexts[i].text = "";
                nerksTexts[i].text = "";
            }
        }
    }

    // Змінює хід на іншого гравця (з 1 на 2, або навпаки)
    private void SwitchTurn()
    {
        currentPlayerTurn = currentPlayerTurn == 1 ? 2 : 1;
    }

    // Оновлює текст з інформацією, хто ходить і скільки цифр має вводити
    private void UpdatePrompt()
    {
        promptText.text = $"Гравець {GetPlayerName(currentPlayerTurn)} вводить {GameData.NumberLength}-значне число";
        // Запускаємо таймер (якщо увімкнено)
        FindObjectOfType<Game>().SendMessage("StartTimerIfNeeded");
    }

    // Повертає ім'я гравця по його номеру (1 або 2)
    private string GetPlayerName(int player)
    {
        return player == 1 ? GameData.Player1Name : GameData.Player2Name;
    }

    // Показує панель помилки з текстом message
    private void ShowError(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
    }

    // Закриває панель помилки
    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false);
    }

    // Перевіряє, чи рядок містить лише цифри
    private bool IsAllDigits(string s)
    {
        foreach (char c in s)
            if (!char.IsDigit(c)) return false;
        return true;
    }

    // Перевіряє, чи є повтори цифр у рядку
    private bool HasDuplicateDigits(string s)
    {
        HashSet<char> set = new HashSet<char>();
        foreach (char c in s)
        {
            if (!set.Add(c))
                return true; // Якщо додати не вдалось, значить повтор
        }
        return false;
    }


    // *** Методи для панелі результатів ***

    private void ShowResultPanel(int winner)
    {
        resultPanel.SetActive(true);
        guessInput.interactable = false; // Забороняємо вводити нові спроби після завершення

        int loser = winner == 1 ? 2 : 1;

        // Встановлюємо імена переможця і програвшого
        victoryNameText.text = GetPlayerName(winner);
        defeatNameText.text = GetPlayerName(loser);

        // Визначаємо секретні коди обох гравців
        string winnerSecretCode = winner == 1 ? GameData.SecretCode1 : GameData.SecretCode2;
        string loserSecretCode = loser == 1 ? GameData.SecretCode1 : GameData.SecretCode2;

        // Кількість спроб гравців
        int winnerAttempts = winner == 1 ? guessesPlayer1.Count : guessesPlayer2.Count;
        int loserAttempts = loser == 1 ? guessesPlayer1.Count : guessesPlayer2.Count;

        // Відображаємо їх у UI
        victoryNumberText.text = winnerSecretCode;
        victoryAttemptsText.text = winnerAttempts.ToString();

        defeatNumberText.text = loserSecretCode;
        defeatAttemptsText.text = loserAttempts.ToString();

        // Зберігаємо статистику останньої гри у GameData (для подальшого використання)
        GameData.lastGameStats.Clear();

        GameData.lastGameStats.Add(new GameData.GameStat(
            GetPlayerName(1),
            guessesPlayer1.Count,
            winner == 1 // isWin: true, якщо гравець 1 переміг
        ));

        GameData.lastGameStats.Add(new GameData.GameStat(
            GetPlayerName(2),
            guessesPlayer2.Count,
            winner == 2
        ));
    }


    // Кнопка "Продовжити" перезавантажує поточну сцену (нова гра)
    public void OnContinueButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Кнопка "Налаштування звуку" відкриває панель налаштувань звуку
    public void OnSoundSettingsButton()
    {
        if (soundSettingsPanel != null)
            soundSettingsPanel.SetActive(true);
    }

    // Кнопка "Повернутися в меню" завантажує сцену головного меню
    public void OnExitToMenuButton()
    {
        SceneManager.LoadScene("MainMenu"); // Вказати правильну назву сцени меню
    }
}
