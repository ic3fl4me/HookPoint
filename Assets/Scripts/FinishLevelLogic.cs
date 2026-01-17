using System.Collections;
using UnityEngine;

public class FinishLevelLogic : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource sfxSource;         // AudioSource auf diesem Finish-Objekt
    [SerializeField] private AudioClip levelFinishClip;     // Finish-Sound
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    [Header("Finish Timing")]
    [SerializeField] private float minDelay = 0.2f;         // falls Clip sehr kurz/leer

    private bool triggered = false;

    private void Awake()
    {
        if (sfxSource == null)
            sfxSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.gameObject.name == "Player")
        {
            triggered = true;

            // Flag setzen (wie vorher)
            ScoreManager.instance.isLevelFinished = true;

            // Sound abspielen
            float delay = minDelay;
            if (sfxSource != null && levelFinishClip != null)
            {
                sfxSource.PlayOneShot(levelFinishClip, volume);
                delay = Mathf.Max(delay, levelFinishClip.length);
            }

            // Finish erst nach Sound
            StartCoroutine(FinishAfterDelay(delay));
        }
    }

    private IEnumerator FinishAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        ScoreManager.instance.FinishLevel();
    }
}
