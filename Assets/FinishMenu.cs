using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FinishMenu : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] private Player player;

    public void ActivateFinishPanel()
    {
        if (panel == null) return;

        panel.SetActive(true);
        player.TogglePlayerActive(false);
    }
}
