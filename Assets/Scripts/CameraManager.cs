using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // ISSUE => OJBECT INSIDE
    [SerializeField]
    Transform target;

    [SerializeField]
    MouseSensitivity mouseSensitivity;

    [SerializeField]
    CameraAngle cameraAngle;

    CameraRotation _cameraRotation;
    Vector2 _input;

    float _distanceToTarget;

    private void Awake()
    {
        _distanceToTarget = Vector3.Distance(transform.position, target.position);
    }

    private void Update()
    {
        HandleInputs();
        HandleRotation();
    }

    private void LateUpdate()
    {
        transform.eulerAngles =
            new Vector3(_cameraRotation.getPitch(), _cameraRotation.getYaw(), 0.0F);
        transform.position = target.position - transform.forward * _distanceToTarget;
    }

    private void HandleInputs()
    {
        // ISSUE
        if (Input.GetMouseButton(0))
        {
            _input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            return;
        }

        _input = Vector2.zero;
    }

    private void HandleRotation()
    {
        float yaw = _cameraRotation.getYaw();
        float pitch = _cameraRotation.getPitch();

        yaw += _input.x * mouseSensitivity.getHorizontal()
                * mouseSensitivity.getInvertHorizontal() * Time.deltaTime;

        pitch += _input.y * mouseSensitivity.getVertical()
                    * mouseSensitivity.getInvertVertical() * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, cameraAngle.getMin(), cameraAngle.getMax());

        _cameraRotation.setYaw(yaw);
        _cameraRotation.setPitch(pitch);
    }
}
