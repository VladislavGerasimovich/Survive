using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Improvements", menuName = "improvement/create new improvement")]
public class ImprovementInfo : ScriptableObject
{
    [SerializeField] private int _level;
    [SerializeField] private string _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _englishText;
    [SerializeField] private string _turkishText;
    [SerializeField] private string _russianText;

    private string _text;

    public void ChangeLanguage(string languageCode)
    {
        switch (languageCode)
        {
            case Constants.English:
                _text = _englishText;
                break;
            case Constants.Turkish:
                _text = _turkishText;
                break;
            case Constants.Russian:
                _text = _russianText;
                break;
        }
    }

    public int Level => _level;
    public string Type => _type;
    public Sprite Icon => _icon;
    public string Text => _text;
}
