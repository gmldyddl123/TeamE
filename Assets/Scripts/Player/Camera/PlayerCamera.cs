using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float defaultDistance = 6f;
    private float minDistance = 1f;
    private float maxDistance = 6f;

    private float smoothing = 4;
    private float zoomSensitivity = 1f;

    private CinemachineFramingTransposer framingTransposer;
    private CinemachineInputProvider inputProvider;

    private float currentTargetDistance;

    private void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();

        currentTargetDistance = defaultDistance;
    }

    private void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        //z축의 2 축 색인을 전달
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minDistance, maxDistance);

        float currentDistance = framingTransposer.m_CameraDistance;

        if(currentDistance == currentTargetDistance)
        {
            return;
        }

        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

        framingTransposer.m_CameraDistance = lerpedZoomValue;
    }
}
