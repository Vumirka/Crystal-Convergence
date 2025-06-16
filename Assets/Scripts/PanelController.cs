using UnityEngine;

// Цей скрипт дозволяє керувати двома панелями: одну відкриває, іншу закриває
public class PanelController : MonoBehaviour
{
    public GameObject panelToOpen;  // Панель, яку потрібно показати
    public GameObject panelToClose; // Панель, яку потрібно приховати

    // Цей метод можна викликати при натисканні кнопки
    public void SwitchPanel()
    {
        // Якщо є панель для закриття — ховаємо її
        if (panelToClose != null)
            panelToClose.SetActive(false);

        // Якщо є панель для відкриття — показуємо її
        if (panelToOpen != null)
            panelToOpen.SetActive(true);
    }
}
