using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject panel;
    void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC gedrückt"); // TEST
            TogglePanel();
        }

        if (Input.anyKeyDown)
        {
            Debug.Log("Taste gedrückt");
        }
    }
    void TogglePanel()
    {
        if (panel == null) return;

        panel.SetActive(!panel.activeSelf);
    }

}
