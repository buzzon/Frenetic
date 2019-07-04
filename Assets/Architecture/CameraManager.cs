using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        var desiredPosition = _target.position + _offset;

        // Работает (ограничение левой и правой границы коллайдера BoundaryManager.Boundary.bounds)
        desiredPosition.x = DesiredPosition(desiredPosition.x,
            BoundaryManager.Boundary.bounds.min.x + _camera.orthographicSize * _camera.aspect,
            BoundaryManager.Boundary.bounds.max.x - _camera.orthographicSize * _camera.aspect);

        // Ебанина которую нужно починить (ограничение камеры по верхней и нижней границе коллайдера BoundaryManager.Boundary.bounds)
        desiredPosition.z = DesiredPosition(desiredPosition.z,
            BoundaryManager.Boundary.bounds.min.z - _camera.orthographicSize, 
            BoundaryManager.Boundary.bounds.max.z - _camera.orthographicSize);
        ////

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


