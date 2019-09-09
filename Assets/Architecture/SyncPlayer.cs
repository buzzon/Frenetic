using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncPlayer : NetworkBehaviour
{
    [SyncVar] private Vector3 syncMovementDirection;
    [SyncVar] private Vector3 syncFacingDirection;
    [SyncVar] private Quaternion syncRotation;
    [SyncVar] private Vector3 syncPosition;

    [SerializeField] float speedLerpRotation = 0.1f;
    [SerializeField] float positionTolerance = 1.0f;

    private CharacterMotor motor;
    private Transform transform;

    void Start()
    {
        motor = GetComponent(typeof(CharacterMotor)) as CharacterMotor;
        if (motor == null) Debug.Log("Motor is null!!");

        transform = GetComponent(typeof(Transform)) as Transform;
        if (motor == null) Debug.Log("Transform is null!!");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TransmitMovementDirection();
        LerpPosition();
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            motor.desiredMovementDirection = syncMovementDirection;
            motor.desiredFacingDirection = syncFacingDirection;
            transform.rotation = Quaternion.Lerp(transform.rotation, syncRotation, Time.time * speedLerpRotation);
            if (Vector3.Distance(syncPosition, transform.position) > positionTolerance) transform.position = syncPosition;
        }
    }

    [Command]
    void CmdProviedMovementDirectionToServer(Vector3 vector)
    {
        syncMovementDirection = vector;
    }

    [Command]
    void CmdProviedFacingDirectionToServer(Vector3 vector)
    {
        syncFacingDirection = vector;
    }

    [Command]
    void CmdProviedRotationToServer(Quaternion quaternion)
    {
        syncRotation = quaternion;
    }

    [Command]
    void CmdProviedPositionToServer(Vector3 vector)
    {
        syncPosition = vector;
    }

    [ClientCallback]
    void TransmitMovementDirection()
    {
        if (isLocalPlayer)
        {
            CmdProviedMovementDirectionToServer(motor.desiredMovementDirection);
            CmdProviedFacingDirectionToServer(motor.desiredFacingDirection);
            CmdProviedRotationToServer(transform.rotation);
            CmdProviedPositionToServer(transform.position);
        }
    }


}
