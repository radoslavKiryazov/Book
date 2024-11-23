using UnityEngine;

public class RulesScreenManager : MonoBehaviour
{
    public GameObject rulesPanel;
    public GameObject joystickCanvas;

    public void CloseRulesPanel()
    {
        rulesPanel.SetActive(false);
        joystickCanvas.SetActive(true);
    }
}
