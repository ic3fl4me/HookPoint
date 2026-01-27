using System;
using System.Collections;
using UnityEngine;

public class FinishLevelLogic : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource sfxSource;         // AudioSource
    [SerializeField] private AudioClip levelFinishClip;     // Finish-Sound
    [SerializeField, Range(0f, 1f)] private float volume = 1f;
    [SerializeField] private float minDelay = 0.2f;         // falls Clip sehr kurz/leer

    [SerializeField] private FinishMenu finishMenu;

    private void Awake()
    {
        if (sfxSource == null)
            sfxSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // Sound abspielen
            float delay = minDelay;
            if (sfxSource != null && levelFinishClip != null)
            {
                sfxSource.PlayOneShot(levelFinishClip, volume);
                delay = Mathf.Max(delay, levelFinishClip.length);
            }

            finishMenu.ActivateFinishPanel();

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
