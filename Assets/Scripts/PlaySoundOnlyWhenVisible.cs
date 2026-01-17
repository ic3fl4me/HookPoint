using UnityEngine;

public class PlaySoundOnlyWhenVisible : MonoBehaviour
{
    [SerializeField] private AudioSource src;
    [SerializeField] private SpriteRenderer sr;

    private bool started = false;

    void Awake()
    {
        if (src == null) src = GetComponent<AudioSource>();
        if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();

        if (src != null) src.playOnAwake = false;
    }

    void Update()
    {
        if (src == null || sr == null) return;

        bool visible = sr.isVisible; // im Kamerabild?

        if (visible)
        {
            if (!src.isPlaying)
            {
                if (started) src.UnPause();
                else { src.Play(); started = true; }
            }
        }
        else
        {
            if (src.isPlaying) src.Pause();
        }
    }
}