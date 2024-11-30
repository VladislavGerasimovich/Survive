using System;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class SavedRect
    {
        public bool IsInitialized = false;

        public Vector3 AnchoredPosition;
        public Vector2 SizeDelta;
        public Vector2 MinAnchor;
        public Vector2 MaxAnchor;
        public Vector2 Pivot;
        public Vector3 Rotation;
        public Vector3 Scale;

        public void SaveDataFromRectTransform(RectTransform rect)
        {
            if (rect == null)
                return;

            IsInitialized = true;

            AnchoredPosition = rect.anchoredPosition3D;
            SizeDelta = rect.sizeDelta;
            MinAnchor = rect.anchorMin;
            MaxAnchor = rect.anchorMax;
            Pivot = rect.pivot;
            Rotation = rect.localEulerAngles;
            Scale = rect.localScale;
        }

        public void PutDataToRectTransform(RectTransform rect)
        {
            if (rect == null || !IsInitialized)
                return;

            rect.anchoredPosition3D = AnchoredPosition;
            rect.sizeDelta = SizeDelta;
            rect.anchorMin = MinAnchor;
            rect.anchorMax = MaxAnchor;
            rect.pivot = Pivot;
            rect.localEulerAngles = Rotation;
            rect.localScale = Scale;
        }
    }
}