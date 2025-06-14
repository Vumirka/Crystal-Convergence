using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;      // Панель меню паузи
    public GameObject settingsPanel;   // Панель налаштувань (в паузі)

    void Update()
    {
        // При натисканні клавіші Escape переключаємо стан паузи
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Метод для увімкнення/вимкнення паузи
    public void TogglePause()
    {
        // Якщо зараз відкрита панель налаштувань — закриваємо її і відкриваємо меню паузи
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            pausePanel.SetActive(true);
        }
        else
        {
            // Якщо налаштувань немає, просто переключаємо видимість панелі паузи
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
    }

    // Продовжити гру — просто приховуємо панель паузи
    public void ContinueGame()
    {
        pausePanel.SetActive(false);
    }

    // Відкрити панель налаштувань з меню паузи
    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // Вийти у головне меню (завантажити сцену меню)
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
