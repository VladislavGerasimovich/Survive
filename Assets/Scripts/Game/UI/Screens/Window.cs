using System;
using UnityEngine;

namespace Game.UI.Screens
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup CanvasGroup;

        public event Action IsPanelOpen;
        public event Action IsPanelClose;

        public bool IsOpen { get; private set; }

        public virtual void Open()
        {
            IsOpen = true;
            IsPanelOpen.Invoke();
        }

        public virtual void Close()
        {
            IsOpen = false;
            IsPanelClose.Invoke();
        }
    }
}