using Game.UI;
using UnityEngine;

namespace Game.DeathOfAllCharacters
{
    public class Death : MonoBehaviour
    {
        protected AnimatorData AnimatorData;
        protected Animator Animator;

        public virtual void Died() { }
    }
}