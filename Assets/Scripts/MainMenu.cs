using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;     // Панель головного меню (призначаю вручну в інспекторі)
    public GameObject settingsPanel;     // Панель налаштувань (також призначаю в інспекторі)

    // Кнопка "Грати" — переходжу на сцену з вибором режиму
    public void SelectionScene()
    {
        SceneManager.LoadScene("ModeSelection"); // Назва сцени з вибором режиму гри
    }

    // Кнопка "Налаштування" — ховаю головне меню і показую панель з налаштуваннями
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);     // Ховаю головне меню
        settingsPanel.SetActive(true);      // Показую налаштування
    }

    // Кнопка "Назад" у налаштуваннях — повертаюся до головного меню
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);     // Ховаю налаштування
        mainMenuPanel.SetActive(true);      // Показую головне меню
    }

    // Кнопка "Вийти" — вихід з гри
    public void ExitGame()
    {
        Application.Quit();                 // Закриває гру (працює тільки у збірці)
        Debug.Log("Гра закрита");           // Для перевірки в редакторі Unity
    }
}
