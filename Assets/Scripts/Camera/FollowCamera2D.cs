using UnityEngine;

public class FollowCamera2D : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        var targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
