using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ExperienceBar : MonoBehaviour
    {
        [SerializeField] protected Slider SmoothSlider;

        private void Awake()
        {
            OnValueChanged(0);
        }

        public void OnValueChanged(float value)
        {
            SmoothSlider.value = value;
        }
    }
}