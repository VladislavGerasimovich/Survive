using System;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroup;

    public bool IsOpen { get; private set; }

    public event Action IsPanelOpen;
    public event Action IsPanelClose;

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
