using UnityEngine;

namespace Game.UI
{
    public class AnimatorData
    {
        public readonly int MoveX = Animator.StringToHash(nameof(MoveX));
        public readonly int MoveZ = Animator.StringToHash(nameof(MoveZ));
        public readonly int Died = Animator.StringToHash(nameof(Died));
    }
}