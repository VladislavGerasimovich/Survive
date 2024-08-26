using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class VideoAdForImprovement : MonoBehaviour
{
    [SerializeField] private GameTime _gameTime;

    private AudioSource _mainMusic;

    public event UnityAction RewardReceived;
    public event UnityAction OnCloseAd;

    public bool IsOpen { get; private set; }

    private void Awake()
    {
        _mainMusic = GetComponent<AudioSource>();
    }

    public virtual void Show() =>
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);

    private void OnOpenCallback()
    {
        IsOpen = true;
        Time.timeScale = 0;
        _gameTime.Stop();
        AudioListener.volume = 0;
        _mainMusic.Pause();
    }

    private void OnRewardCallback()
    {
        RewardReceived.Invoke();
    }

    private void OnCloseCallback()
    {
        IsOpen = false;
        OnCloseAd?.Invoke();
        _gameTime.Stop();
        AudioListener.volume = 1;

        if (_mainMusic.enabled == true)
        {
            _mainMusic.Play();
        }
    }
}
