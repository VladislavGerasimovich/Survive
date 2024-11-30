using System;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Shop.Items
{
    public class ConsumableItem : Item
    {
        private const string FIRST_AID = "FIRST_AID";
        private const string IMMORTALITY = "IMMORTALITY";

        [SerializeField] protected TMP_Text TextOfCount;
        [SerializeField] protected int CurrentCount;
        [SerializeField] protected int MaxCount;

        public override event Action<string, string> Clicked;

        public override void SetStatus()
        {
            CurrentCount++;
            SetTextOfCount();

            if (CurrentCount == MaxCount)
            {
                Button.interactable = false;
            }

            _playerDataManager.Set(Text, CurrentCount);
        }

        public void SetTextOfCount()
        {
            TextOfCount.text = $"{CurrentCount} / {MaxCount}";
            InfoText.text = Cost[0];
        }

        public override void OnClick()
        {
            Clicked?.Invoke(Cost[0], Type);
        }

        public override void SetCost()
        {
            InfoText.text = Cost[0];

            if (CurrentCount == MaxCount)
            {
                Button.interactable = false;
            }
        }

        protected override void SetIndex(PlayerData playerData)
        {
            switch (Text)
            {
                case FIRST_AID:
                    CurrentCount = playerData.FirstAidCount;
                    break;
                case IMMORTALITY:
                    CurrentCount = playerData.ImmortalityCount;
                    break;
                default:
                    break;
            }

            if (CurrentCount >= MaxCount)
            {
                Button.interactable = false;
            }

            SetTextOfCount();
        }
    }
}