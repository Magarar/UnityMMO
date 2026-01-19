using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public float useX;
        public float useY;
        public float useZ;

        public Transform targetPlayer;
        public float moveSpeed;

        private void CameraMovement()
        {
            if(targetPlayer == null)
                return;
            transform.position = new Vector3(targetPlayer.position.x + useX, targetPlayer.position.y + useY, targetPlayer.position.z + useZ);
        }

        private void Update()
        {
            CameraMovement();
        }
    }
}
