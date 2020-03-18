using UnityEngine;

public class AudioController : MonoBehaviour
{
    public enum Sounds
    {
        Button,
        GameOver,
        OpenUi, 
        Invade
    }

    public AudioClip ButtonSounds;
    public AudioClip GameOverSound;
    public AudioClip OpenUISound;
    public AudioClip InvadeSound;

    public static AudioController Instance;
    public AudioSource UISoundSource;
    public AudioSource ThemeSoundSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(Sounds type)
    {
        switch (type)
        {
            case Sounds.Button:
                UISoundSource.PlayOneShot(ButtonSounds);
                break;
            case Sounds.GameOver:
                UISoundSource.PlayOneShot(GameOverSound);
                break;
            case Sounds.OpenUi:
                UISoundSource.PlayOneShot(OpenUISound);
                break; 
            case Sounds.Invade:
                UISoundSource.PlayOneShot(InvadeSound);
                break;
        }
    }

    public void PlayTheme()
    {
        // ThemeSoundSource.Play();
    }
}
