using UnityEngine;

namespace Appside.Scripts.Test
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float speedMultiplier;
    
        void Update()
        {
            Vector3 targetPosition = target.position + offset;
            var transform1 = transform;
            var position = transform1.position;
            Vector3 moveDirection = targetPosition - position;
            float speed = moveDirection.magnitude * moveDirection.magnitude;
            position += speed * speedMultiplier * moveDirection.normalized;
            transform1.position = position;
        }
    }
}
