using UnityEngine;

namespace Utility.Npc
{
    public class PointAToPointBMove : MonoBehaviour
    {
        [SerializeField]private Transform pointA;
        [SerializeField]private Transform pointB;
        [SerializeField]private bool curPoint;
        [SerializeField]private float moveSpeed = 2f;
        [SerializeField]private float rotateSpeed = 10f;
        [SerializeField]private float distanceThresholdSquared = 0.01f;

        private void Update()
        {
            MovementUpdate();
        }

        private void MovementUpdate()
        {
            if (pointA == null || pointB == null)
            {
                Debug.LogError("MovementUpdate: pointA or pointB is null.");
                return;
            }

            if (moveSpeed <= 0)
            {
                Debug.LogWarning("MovementUpdate: moveSpeed is non-positive, object will not move.");
                return;
            }

            Transform targetPoint = curPoint ? pointB : pointA;
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, Time.deltaTime * moveSpeed);
            RotateUpdate(targetPoint);

            if (Vector3.SqrMagnitude(transform.position - targetPoint.position) < distanceThresholdSquared)
            {
                curPoint = !curPoint;
            }
        }

        private void RotateUpdate(Transform targetPoint)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }
}
