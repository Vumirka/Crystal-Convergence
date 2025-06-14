using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{

    // Завантажуємо класичний режим гри (основну сцену гри)
    public void LoadClassicMode()
    {
        SceneManager.LoadScene("GameScene");  // Назва сцени з класичним режимом
    }

    // Кнопка "Назад" — повернення до головного меню
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");   // Назва сцени з головним меню
    }
}
