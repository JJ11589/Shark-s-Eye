using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip stretchClip;
    [SerializeField] private AudioClip releaseClip;

    [Header("Audio Settings")]
    [SerializeField] private float stretchVolume = 0.7f;
    [SerializeField] private float releaseVolume = 1f;


    public void PlayStrech() 
    {
        audioSource.clip = stretchClip;
        audioSource.volume = stretchVolume;
        audioSource.PlayOneShot(stretchClip);
        
        
    }

    public void PlayRelease() 
    {
        audioSource.clip = releaseClip;
        audioSource.volume = releaseVolume;
        audioSource.PlayOneShot(releaseClip);
    }
}
