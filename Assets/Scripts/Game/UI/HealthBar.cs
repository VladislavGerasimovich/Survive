using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class HealthBar : ExperienceBar
{
    private void Awake()
    {
        _smoothSlider.value = 1;
    }
}