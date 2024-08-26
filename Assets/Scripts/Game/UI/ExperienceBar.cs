using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] protected Slider SmoothSlider;

    private void Awake()
    {
        SmoothSlider.value = 0;
    }

    public void OnValueChanged(float value)
    {
        SmoothSlider.value = value;
    }
}