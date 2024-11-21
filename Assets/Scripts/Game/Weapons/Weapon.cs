using DG.Tweening;
using UnityEngine;

namespace Game.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float _startScaleValue;
        [SerializeField] private float _endScaleValue;
        [SerializeField] private int _duration;

        public virtual void Show()
        {
            if (transform != null)
            {
                transform.DOScale(_startScaleValue, _duration).SetLink(gameObject);
            }
        }

        public virtual void Hide()
        {
            if (transform != null)
            {
                transform.DOScale(_endScaleValue, _duration).SetLink(gameObject);
            }
        }
    }
}