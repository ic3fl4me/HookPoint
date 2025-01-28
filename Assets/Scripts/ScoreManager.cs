using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TMP_Text currentTimeText;
    [SerializeField] public TMP_Text recordTimeText;

    public static ScoreManager instance;
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
        recordTime = GetRecord();
        
        // Set currentTime and recordTime
        currentTimeText.text = "YOUR TIME: " + (int)currentTime / 60 + ":" + (currentTime % 60).ToString("00.00");
        recordTimeText.text = "RECORD: " + (int)recordTime / 60 + ":" + (recordTime % 60).ToString("00.00");
    }

    // Update is called once per frame
    void Update()
    {
        // Update currentTime as long as level is not finished
        if (!isLevelFinished)
        {
            currentTime += Time.deltaTime;
            currentTimeText.text = "YOUR TIME: " + (int)currentTime / 60 + ":" + (currentTime % 60).ToString("00.00");
        }
    }

    // Function called by ScoreManager when level is finished
    public void FinishLevel()
    {
        isLevelFinished = true;

        // Save current time as new record if its faster than old record
        if (currentTime < recordTime || recordTime == 0f)
        {
            SaveNewRecord(currentTime);
            recordTimeText.text = "RECORD: " + (int)currentTime / 60 + ":" + (currentTime % 60).ToString("00.00");
        }
    }

    private void SaveNewRecord(float newRecord)
    {
        PlayerPrefs.SetFloat("recordTime", newRecord);
    }

    private float GetRecord()
    {
        return PlayerPrefs.GetFloat("recordTime");
    }
}
