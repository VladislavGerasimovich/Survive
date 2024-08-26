using UnityEngine;

public class FirstAid : ConsumableItem
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string FIRST_AID = "FIRST_AID";

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

        _playerDataManager.Set(FIRST_AID, CurrentCount);
    }

    private void SetIndex(PlayerData playerData)
    {
        CurrentCount = playerData.FirstAidCount;

        if (CurrentCount >= MaxCount)
        {
            Button.interactable = false;
        }

        SetTextOfCount();
    }
}
