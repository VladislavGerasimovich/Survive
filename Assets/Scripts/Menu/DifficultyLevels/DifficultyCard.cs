using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PressButton))]
public class DifficultyCard : MonoBehaviour
{
    [SerializeField] private int _reward;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _level;

    private const string LEVEL = "DIFFICULTY_LEVEL";
    private const string REWARD = "CURRENTREWARD";

    private PressButton _pressButton;

    public event Action Click;

    private void Awake()
    {
        _pressButton = GetComponent<PressButton>();
        _text.text = _reward.ToString();
    }

    private void OnEnable()
    {
        _pressButton.Click += OnCardClick;
    }

    private void OnDisable()
    {
        _pressButton.Click -= OnCardClick;
    }

    private void OnCardClick()
    {
        Click?.Invoke();
        PlayerPrefs.SetInt(LEVEL, _level);
        PlayerPrefs.SetInt(REWARD, _reward);
        PlayerPrefs.Save();
    }
}
