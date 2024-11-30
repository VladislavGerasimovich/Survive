using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Slider))]
    public class Bar : MonoBehaviour
    {
        [SerializeField] private int _value;

        private Slider SmoothSlider;

        private void Awake()
        {
            SmoothSlider = GetComponent<Slider>();
            OnValueChanged(_value);
        }

        public void OnValueChanged(float value)
        {
            SmoothSlider.value = value;
        }
    }
}