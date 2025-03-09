using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private RectTransform buttonTransform;

    private const string MusicPrefKey = "MusicEnabled";
    private Image musicButtonImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicButtonImage = GetComponent<Image>();

        if (audioSource == null)
        {
            return;
        }

        AudioClip clip = Resources.Load<AudioClip>("jungle-style");

        if (PlayerPrefs.GetInt(MusicPrefKey, 1) == 0)
            audioSource.mute = true;

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }

        UpdateMusicIcon();
    }

    public void ToggleMusic()
    {
        audioSource.mute = !audioSource.mute;
        PlayerPrefs.SetInt(MusicPrefKey, audioSource.mute ? 0 : 1);
        UpdateMusicIcon();
    }

    private void UpdateMusicIcon()
    {
        if (musicButtonImage != null)
        {
            musicButtonImage.sprite = audioSource.mute ? musicOffSprite : musicOnSprite;
        }
    }
}