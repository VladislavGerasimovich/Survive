using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;

public class Shop
{
    private const string MONEY = "MONEY";
    public event Action ItemBuyed;

    public int Money { get; private set; }

    public Shop()
    {
        Money = PlayerPrefs.GetInt(MONEY, 0);
    }

    public void AddMoney(int money)
    {
        Money += money;
        PlayerPrefs.SetInt(MONEY, Money);
        PlayerPrefs.Save();
    }

    public void SpentMoney(int money)
    {
        Money -= money;
        PlayerPrefs.SetInt(MONEY, Money);
        PlayerPrefs.Save();
        ItemBuyed?.Invoke();
    }

    public bool TrySpentMoney(int money)
    {
        if (Money >= money)
        {
            SpentMoney(money);
            return true;
        }

        return false;
    }
}
