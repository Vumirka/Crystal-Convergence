using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Цей скрипт змінює вигляд кнопки при наведенні курсора миші
public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage; // Компонент зображення кнопки

    public Sprite normalSprite;  // Зображення кнопки у звичайному стані
    public Sprite hoverSprite;   // Зображення кнопки при наведенні

    void Start()
    {
        // Отримуємо компонент Image з цієї кнопки
        buttonImage = GetComponent<Image>();

        // Встановлюємо початкове зображення (звичайне)
        buttonImage.sprite = normalSprite;
    }

    // Цей метод викликається, коли курсор наводиться на кнопку
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite; // Змінюємо зображення на "наведене"
    }

    // Цей метод викликається, коли курсор перестає бути над кнопкою
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite; // Повертаємо звичайне зображення
    }
}
