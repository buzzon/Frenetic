using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 cameraBounds;

    private Camera _camera;

    void Awake()
    {
        _offset.z += transform.position.z;
        _camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        var desiredPosition = _target.position + _offset;


        //var height = _camera.orthographicSize * 2;
        //var bounds = new Bounds(Vector3.zero, new Vector3(height * _camera.aspect, height, 0));

        desiredPosition.x = DesiredPosition(desiredPosition.x,
            BoundaryManager.Boundary.bounds.min.x + _camera.orthographicSize * _camera.aspect,
            BoundaryManager.Boundary.bounds.max.x - _camera.orthographicSize * _camera.aspect);
        desiredPosition.y = DesiredPosition(desiredPosition.y,
            BoundaryManager.Boundary.bounds.min.y + _camera.orthographicSize, 
            BoundaryManager.Boundary.bounds.max.y - _camera.orthographicSize);

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


