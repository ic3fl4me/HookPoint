using System;
using Unity.VisualScripting;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] private Player player;

    void Start()
    {
        player = FindAnyObjectByType(typeof(Player)).GetComponent<Player>();
        if (panel != null)
            panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
    }

    public void TogglePanel()
    {
        if (panel == null) return;

        panel.SetActive(!panel.activeSelf);
        player.TogglePlayerActive(panel.activeSelf);
    }

}
