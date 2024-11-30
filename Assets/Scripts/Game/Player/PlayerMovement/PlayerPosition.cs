using UnityEngine;

namespace Game.Player.Movement
{
    public class PlayerPosition : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        private void Update()
        {
            transform.position = new Vector3(
                _playerMovement.transform.position.x,
                transform.position.y,
                _playerMovement.transform.position.z);
        }
    }
}