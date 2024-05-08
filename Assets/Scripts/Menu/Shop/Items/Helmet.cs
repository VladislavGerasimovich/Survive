using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Item
{
    private const string HELMET = "HELMET";

    private void Start()
    {
        _indexOfCost = PlayerPrefs.GetInt(HELMET);
        SetCost();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(HELMET, _indexOfCost);
        PlayerPrefs.Save();
    }
}
