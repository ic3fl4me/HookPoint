using UnityEngine;

public class FinishLevelLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Call ScoreManagers function to finish if the OnTriggerEnter2D function below was executed
        if (ScoreManager.instance.isLevelFinished)
        {
            ScoreManager.instance.FinishLevel();
        }
    }

    // Upon player contact set isLevelFinished
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            ScoreManager.instance.isLevelFinished = true;
        }
    }
}
