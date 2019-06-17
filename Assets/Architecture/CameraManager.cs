using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;

    void Awake()
    {
        _offset.z += transform.position.z;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
    }
}
