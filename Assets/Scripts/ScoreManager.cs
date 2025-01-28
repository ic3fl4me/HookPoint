using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] public TMP_Text currentTimeText;
    [SerializeField] public TMP_Text recordTimeText;
    private float currentTime = 0;
    private float recordTime = 0;
    public bool isLevelFinished = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recordTime = PlayerPrefs.GetFloat("recordTime");
        currentTimeText.text = "YOUR TIME: " + (int)currentTime / 60 + ":" + (currentTime % 60).ToString("00.00");
        recordTimeText.text = "RECORD: " + (int)recordTime / 60 + ":" + (recordTime % 60).ToString("00.00");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLevelFinished)
        {
            currentTime += Time.deltaTime;
            currentTimeText.text = "YOUR TIME: " + (int)currentTime / 60 + ":" + (currentTime % 60).ToString("00.00");
        }
    }

    public void FinishLevel()
    {
        isLevelFinished = true;
        if (currentTime < recordTime || recordTime == 0f)
        {
            PlayerPrefs.SetFloat("recordTime", currentTime);
            recordTimeText.text = "RECORD: " + (int)currentTime / 60 + ":" + (currentTime % 60).ToString("00.00");
        }
    }
}
