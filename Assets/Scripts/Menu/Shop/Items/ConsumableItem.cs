using System;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Shop.Items
{
    public class ConsumableItem : Item
    {
        [SerializeField] protected TMP_Text TextOfCount;
        [SerializeField] protected int CurrentCount;
        [SerializeField] protected int MaxCount;

        public override event Action<string, string> Clicked;

        public override void SetStatus()
        {
            Debug.Log("setstatus");
            CurrentCount++;
            SetTextOfCount();

            if (CurrentCount == MaxCount)
            {
                Button.interactable = false;
            }
        }

        public void SetTextOfCount()
        {
            TextOfCount.text = $"{CurrentCount} / {MaxCount}";
            Text.text = Cost[0];
        }

        public override void OnClick()
        {
            Clicked?.Invoke(Cost[0], Type);
        }

        public override void SetCost()
        {
            Text.text = Cost[0];

            if (CurrentCount == MaxCount)
            {
                Button.interactable = false;
            }
        }

        protected override void SetIndex(PlayerData playerData)
        {
            if (CurrentCount >= MaxCount)
            {
                Button.interactable = false;
            }

            SetTextOfCount();
        }
    }
}