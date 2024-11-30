using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Shop.Items
{
    public class Boots : Item
    {
        private const string BOOTS = "BOOTS";

        private void Awake()
        {
            Text = BOOTS;

            Colors = new List<Color>
            {
                new Color32(125, 120, 126, 255),
                new Color32(170, 238, 147, 255),
                new Color32(54, 189, 240, 255),
                new Color32(244, 105, 255, 255),
                new Color32(229, 214, 75, 255),
            };

            Background = GetComponent<Image>();
            Button = GetComponent<Button>();
            Class = Type;
            string languageCode = YandexGamesSdk.Environment.i18n.lang;
            ChangeLanguage(languageCode);
            CostCountMultiplier = 1;
        }
    }
}