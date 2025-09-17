using UnityEngine;

public class audio : MonoBehaviour
{
    public static audio Instance { get; private set; } // Singleton Instance

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip stretchClip;
    [SerializeField] private AudioClip releaseClip;

    [Header("Audio Settings")]
    [SerializeField] private float stretchVolume = 0.7f;
    [SerializeField] private float releaseVolume = 1f;

    private bool isStretching;

    private void Awake()
    {
        // Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
    }

    private void Start()
    {
        if (audioSource == null)
        {
            // Automatically add an AudioSource if it's not set
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    /// <summary>
    /// Plays the stretch sound when the mouse is down.
    /// </summary>
    public void PlayStretchSound()
    {
        if (!isStretching && stretchClip != null)
        {
            audioSource.clip = stretchClip;
            audioSource.volume = stretchVolume;
            audioSource.loop = true;
            audioSource.Play();
            isStretching = true;
        }
    }

    /// <summary>
    /// Stops the stretch sound and plays the release sound when the mouse is released.
    /// </summary>
    public void PlayReleaseSound()
    {
        if (isStretching)
        {
            audioSource.Stop();
            isStretching = false;
        }

        if (releaseClip != null)
        {
            audioSource.PlayOneShot(releaseClip, releaseVolume);
        }
    }
}
