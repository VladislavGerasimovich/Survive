using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Improvements", menuName = "improvement/create new improvement")]
public class Improvements : ScriptableObject
{
    [SerializeField] private int _level;
    [SerializeField] private string _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _englishText;
    [SerializeField] private string _turkishText;
    [SerializeField] private string _russianText;

    private const string English = "en";
    private const string Turkish = "tr";
    private const string Russian = "ru";

    private string _text;

    public void ChangeLanguage(string languageCode)
    {
        switch (languageCode)
        {
            case English:
                _text = _englishText;
                break;
            case Turkish:
                _text = _turkishText;
                break;
            case Russian:
                _text = _russianText;
                break;
        }
    }

    public int Level => _level;
    public string Type => _type;
    public Sprite Icon => _icon;
    public string Text => _text;
}
