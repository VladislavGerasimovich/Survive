using System;

public class Shop
{
    public event Action ItemBuyed;

    public int Money { get; private set; }

    public void Initialize(int money)
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
            return true;
        }

        return false;
    }
}
