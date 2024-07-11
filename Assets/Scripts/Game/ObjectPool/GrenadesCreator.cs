using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadesCreator : ObjectPool
{
    [SerializeField] private GameObject _grenade;
    [SerializeField] private List<Window> _windows;
    [SerializeField] private VideoAd _videoAd;
    [SerializeField] private InterstitialAd _interstitialAd;

    private void Awake()
    {
        _pool = new List<GameObject>();
        Initialize(_grenade);
    }

    private void OnEnable()
    {
        foreach (var window in _windows)
        {
            window.IsPanelOpen += PauseSound;
            window.IsPanelClose += PlaySound;
        }

        _videoAd.OnOpenAd += PauseSound;
        _interstitialAd.OnOpenAd += PauseSound;
        _videoAd.OnCloseAd += PlaySound;
    }

    private void OnDisable()
    {
        foreach (var window in _windows)
        {
            window.IsPanelOpen -= PauseSound;
            window.IsPanelClose -= PlaySound;
        }

        _videoAd.OnOpenAd -= PauseSound;
        _interstitialAd.OnOpenAd -= PauseSound;
        _videoAd.OnCloseAd -= PlaySound;
    }

    protected override void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject item = Instantiate(prefab, _container.transform);
            item.SetActive(false);
            _pool.Add(item);
        }
    }

    public void PauseSound()
    {
        foreach (GameObject item in _pool)
        {
            AudioSource audio = item.GetComponent<AudioSource>();

            if (audio.isPlaying)
            {
                audio.Pause();
            }
        }
    }

    public void PlaySound()
    {
        foreach (var window in _windows)
        {
            if (window.IsOpen == true)
            {
                return;
            }
        }

        foreach (GameObject item in _pool)
        {
            AudioSource audio = item.GetComponent<AudioSource>();

            if (audio.time != 0)
            {
                audio.Play();
            }
        }
    }
}
