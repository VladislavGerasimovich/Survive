using System;
using System.Collections;
using System.Collections.Generic;

public class Shop
{
    public event Action ItemBuyed;

    public int Money { get; private set; }

    public Shop(int money)
    {
        Money = money;
    }

    public void AddMoney(int money)
    {
        Money += money;
    }

    public void SpentMoney(int money)
    {
        Money -= money;
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
