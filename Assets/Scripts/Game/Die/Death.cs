using UnityEngine;
using Game.UI;

namespace Game.DeathOfAllCharacters
{
    public class Death : MonoBehaviour
    {
        protected AnimatorData AnimatorData;
        protected Animator Animator;

        public virtual void Died()
        {
        }
    }
}