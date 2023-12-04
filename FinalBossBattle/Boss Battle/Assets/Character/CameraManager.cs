using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    [Header("Transforms")]
    public Transform targetTransform;
    public Transform cameraPivot;
    [Header("Camera Follow")]
    private Vector3 cameraFollowVelocity = Vector3.zero;
    public float cameraFollowSpeed = 0.2f;
    [Header("Camera Rotation")]
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;
    public float lookAngle;
    public float pivotAngle;

    private void Awake()
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        lookAngle += (inputManager.cameraInputVertical * cameraLookSpeed);
        pivotAngle -= (inputManager.cameraInputHorizontal * cameraPivotSpeed);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
}
