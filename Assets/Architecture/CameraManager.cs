using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    [SerializeField] private const float SmoothSpeed = 0.125f;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private Camera _camera;

    void Awake()
    {
        _offset.z += transform.position.z;
        _camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        var desiredPosition = _target.position + _offset;

        desiredPosition.x = DesiredPosition(desiredPosition.x,
            BoundaryManager.Boundary.bounds.min.x + _camera.orthographicSize * _camera.aspect,
            BoundaryManager.Boundary.bounds.max.x - _camera.orthographicSize * _camera.aspect);
        desiredPosition.y = DesiredPosition(desiredPosition.y,
            BoundaryManager.Boundary.bounds.min.y + _camera.orthographicSize, 
            BoundaryManager.Boundary.bounds.max.y - _camera.orthographicSize);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
    }

    private static float DesiredPosition(float desiredPosition, float min, float max)
    {
        if (desiredPosition < min)
            desiredPosition = min;
        else if (desiredPosition > max)
            desiredPosition = max;
        return desiredPosition;
    }
}


