using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Transform _target;
    [SerializeField] private float _offset;

    private Camera _camera;
    private float rotateconst;

    void Awake()
    {
        _camera = GetComponent<Camera>();
        rotateconst = (float)System.Math.Sqrt(2);
    }

    void FixedUpdate()
    {
        var desiredPosition = _target.position;
        desiredPosition.y += 0.5f;

        desiredPosition.x = DesiredPosition(desiredPosition.x,
            BoundaryManager.Boundary.bounds.min.x + _camera.orthographicSize * _camera.aspect,
            BoundaryManager.Boundary.bounds.max.x - _camera.orthographicSize * _camera.aspect);

        var front = _camera.orthographicSize * rotateconst;

        desiredPosition.z = DesiredPosition(desiredPosition.z - _offset,
            BoundaryManager.Boundary.bounds.min.z - _offset + front, 
            BoundaryManager.Boundary.bounds.max.z - _offset - front);
        desiredPosition.y += _offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
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


