using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;  // Статичний екземпляр для реалізації патерну Singleton
    public AudioSource musicSource;               // Аудіоджерело для музики

    void Awake()
    {
        // Перевірка, чи існує вже екземпляр SoundManager
        if (instance == null)
        {
            instance = this;  // Якщо ні — призначаємо поточний екземпляр
        }
        else if (instance != this)
        {
            // Якщо екземпляр вже є, а це не він — знищуємо дубліката
            Destroy(gameObject);
        }

        // Не знищувати цей об'єкт при завантаженні нової сцени
        DontDestroyOnLoad(gameObject);
    }
}
