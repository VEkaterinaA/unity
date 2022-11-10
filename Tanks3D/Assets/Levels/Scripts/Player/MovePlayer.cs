using UnityEngine;

namespace Assets.Levels.Scripts.Player
{
    public class MovePlayer
    {
        private Vector3 velocity = Vector3.zero;
        private Vector3 rotation = Vector3.zero;
        private float speed = 7f;
        private float rotationSpeed = 1f;
        private Rigidbody rigidbodyPlayer;
        private void Move(Vector3 velocity)
        {
            this.velocity = velocity;
            PerformMove();

        }
        private void PerformMove()
        {
            if (velocity != Vector3.zero)
            {
                rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + velocity * Time.fixedDeltaTime);
                velocity = Vector3.zero;
            }
        }

        private void Rotation(Vector3 rotation)
        {
            this.rotation = rotation;
            PerformRotaton();
        }
        private void PerformRotaton()
        {
            {
                if (rotation != Vector3.zero)
                    rigidbodyPlayer.MoveRotation(rigidbodyPlayer.rotation * Quaternion.Euler(rotation));
                rotation = Vector3.zero;
            }
        }
        public void PlayerMovement(float xMov, float zMov, Vector3 forward, Rigidbody rigidbody)
        {
            rigidbodyPlayer = rigidbody;
            Vector3 movVer = forward * zMov;
            Vector3 velocity = movVer.normalized * speed;

            Move(velocity);
            if (zMov < 0 & xMov != 0)
                xMov = -xMov;
            Vector3 rotation = new Vector3(0, xMov, 0) * rotationSpeed;
            Rotation(rotation);
        }
    }
}