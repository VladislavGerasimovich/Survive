using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] protected Slider _smoothSlider;

    private void Awake()
    {
        _smoothSlider.value = 0;
    }

    public void OnValueChanged(float value)
    {
        _smoothSlider.value = value;
    }
}