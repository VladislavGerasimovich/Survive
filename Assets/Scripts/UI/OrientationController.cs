using System;
using UnityEngine;

namespace UI
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public sealed class OrientationController : MonoBehaviour
    {
        public static bool IsVertical;

        public SavedRect VerticalRect = new SavedRect();
        public SavedRect HorizontalRect = new SavedRect();

        private RectTransform _rect;

        static OrientationController()
        {
            OrientationChanged += (s, e) => IsVertical = e;
        }

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            OrientationChanged += OnOrientationChanged;
            OnOrientationChanged(this, IsVertical);
        }

        public void SaveCurrentState()
        {
            if (IsVertical)
                VerticalRect.SaveDataFromRectTransform(_rect);
            else
                HorizontalRect.SaveDataFromRectTransform(_rect);
        }

        public void PutCurrentState()
        {
            OnOrientationChanged(this, IsVertical);
        }

        private void OnOrientationChanged(object sender, bool isVertical)
        {
            if (isVertical)
                VerticalRect.PutDataToRectTransform(_rect);
            else
                HorizontalRect.PutDataToRectTransform(_rect);
        }

        private void OnDestroy()
        {
            OrientationChanged -= OnOrientationChanged;
        }

        // Static
        private static event EventHandler<bool> OrientationChanged;
        public static void FireOrientationChanged(object s, bool isVertical) => OrientationChanged?.Invoke(s, isVertical);
    }
}