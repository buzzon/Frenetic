using UnityEngine;
using UnityEngine.Networking;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Transform _target;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _smoothSpeed);
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"), Space.World);
    }

    public void setTarget(Transform _transform)
    {
        _target = _transform;
    }
}


