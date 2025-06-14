using UnityEngine;

public class PanelController : MonoBehaviour

{
    public GameObject panelToOpen;
    public GameObject panelToClose;

    public void SwitchPanel()
    {
        if (panelToClose != null)
            panelToClose.SetActive(false);

        if (panelToOpen != null)
            panelToOpen.SetActive(true);
    }
}
