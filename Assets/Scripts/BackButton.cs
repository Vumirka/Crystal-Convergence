using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    // Цей метод викликається при натисканні кнопки "Назад"
    public void BackToMenu()
    {
        // Завантажує сцену з головним меню
        SceneManager.LoadScene("MainMenu");
    }
}
