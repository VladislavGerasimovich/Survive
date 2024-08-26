using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Joystick : MonoBehaviour
{
    [SerializeField] private RectTransform _handler;

    private RectTransform _rectTransform;
    
    public Vector2 Size {  get; private set; }
    public Vector2 Position { get; private set; }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        Size = _rectTransform.rect.size;
    }

    public void SetPosition(Vector2 position)
    {
        _rectTransform.anchoredPosition = position - _rectTransform.rect.size / 2;
        Position = position;
    }

    public void SetHandlerPosition(Vector2 position)
    {
        _handler.anchoredPosition = position;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
