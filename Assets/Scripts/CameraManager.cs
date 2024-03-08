using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    MouseSensitivity mouseSensitivity;

    [SerializeField]
    CameraAngle cameraAngle;

    CameraRotation _cameraRotation;
    Vector2 _input;

    float _distanceToTarget;

    [SerializeField]
    float verticalOffset = 2.0f;
    [SerializeField]
    float backwardOffset = 1.0f; 

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
        float pitch = _cameraRotation.getPitch();
        float yaw = _cameraRotation.getYaw();

        pitch = Mathf.Clamp(pitch, cameraAngle.getMin(), cameraAngle.getMax());

        Vector3 cameraPosition = target.position - transform.forward * (_distanceToTarget - backwardOffset);
        cameraPosition.y += verticalOffset; 
        transform.position = cameraPosition;

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0F);
    }

    private void HandleInputs()
    {
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

        yaw += _input.x * mouseSensitivity.getHorizontal() * mouseSensitivity.getInvertHorizontal() * Time.deltaTime;
        pitch -= _input.y * mouseSensitivity.getVertical() * mouseSensitivity.getInvertVertical() * Time.deltaTime; 

        _cameraRotation.setYaw(yaw);
        _cameraRotation.setPitch(pitch);
    }
}

