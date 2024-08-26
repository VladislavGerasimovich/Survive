using UnityEngine;

public class Immortality : ConsumableItem
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string IMMORTALITY = "IMMORTALITY";

    private void OnEnable()
    {
        Button.onClick.AddListener(OnClick);
        _playerDataManager.DataReceived += SetIndex;
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnClick);
        _playerDataManager.DataReceived -= SetIndex;
    }

    public override void SetStatus()
    {
        base.SetStatus();

        _playerDataManager.Set(IMMORTALITY, CurrentCount);
    }

    private void SetIndex(PlayerData playerData)
    {
        CurrentCount = playerData.ImmortalityCount;

        if (CurrentCount >= MaxCount)
        {
            Button.interactable = false;
        }

        SetTextOfCount();
    }
}
