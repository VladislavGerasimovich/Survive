using UnityEngine;

namespace UI
{
    [ExecuteAlways]
    [DefaultExecutionOrder(-10)]
    public class OrientationChecker : MonoBehaviour
    {
        private void Awake()
        {
            HandleOrientation();
        }

        private void Update()
        {
            HandleOrientation();
        }

        private void HandleOrientation()
        {
            if (OrientationController.IsVertical &&
                Screen.width > Screen.height)
            {
                OrientationController.FireOrientationChanged(this, false);
            }
            else if (!OrientationController.IsVertical &&
                Screen.width < Screen.height)
            {
                OrientationController.FireOrientationChanged(this, true);
            }
        }
    }
}