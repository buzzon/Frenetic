using UnityEngine;

public class MiniCameraManager : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Transform _target;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _smoothSpeed);
    }

    public void setTarget(Transform _transform)
    {
        _target = _transform;
    }
}


