using UnityEngine;

// Цей скрипт відповідає за перемикання між двома панелями: одну показує, іншу ховає
public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject panelToShow;    // Панель, яка повинна з’явитися на екрані
    [SerializeField] private GameObject panelToHide;    // Панель, яку потрібно приховати

    // Цей метод викликається, наприклад, при натисканні кнопки
    public void SwitchPanels()
    {
        // Якщо задана панель для показу — показуємо її
        if (panelToShow != null)
            panelToShow.SetActive(true);

        // Якщо задана панель для приховування — ховаємо її
        if (panelToHide != null)
            panelToHide.SetActive(false);
    }
}
