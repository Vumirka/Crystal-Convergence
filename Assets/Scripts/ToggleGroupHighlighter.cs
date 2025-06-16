using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// Цей скрипт відповідає за зміну кольору фону в Toggle, якщо він активний (обраний)
public class ToggleHighlighter : MonoBehaviour
{
    // Список всіх тумблерів (Toggle), яким ми будемо змінювати колір
    public List<Toggle> toggles;

    // Колір за замовчуванням, коли тумблер не обраний
    public Color normalColor = Color.white;

    // Колір, коли тумблер обраний (світло-блакитний)
    public Color selectedColor = new Color(0.6f, 0.8f, 1f);

    // Метод викликається на початку (коли об'єкт активується)
    void Start()
    {
        // Для кожного тумблера зі списку додаємо слухача події зміни стану (вкл/викл)
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener((isOn) => UpdateVisuals());
        }

        // Оновлюємо кольори одразу після запуску, щоб усі тумблери були в актуальному стані
        UpdateVisuals();
    }

    // Цей метод оновлює кольори тумблерів залежно від того, активні вони чи ні
    public void UpdateVisuals()
    {
        foreach (var toggle in toggles)
        {
            // Знаходимо Image компонента фону тумблера (має назву "Background")
            Image bg = toggle.transform.Find("Background").GetComponent<Image>();

            // Якщо фон знайдено, змінюємо його колір на залежно від стану тумблера
            if (bg != null)
            {
                bg.color = toggle.isOn ? selectedColor : normalColor;
            }
        }
    }
}
