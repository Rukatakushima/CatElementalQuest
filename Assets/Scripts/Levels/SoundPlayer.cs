using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance;

    [SerializeField] private AudioClip defaultSounds;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start() => audioSource.loop = false;

    public void PlaySound(AudioClip sound)
    {
        if (sound != null)
            audioSource.PlayOneShot(sound);
        else
            audioSource.PlayOneShot(defaultSounds);
    }
}
