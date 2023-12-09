using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 lookAtOffset;
    [SerializeField] private float SmoothCameraFollowStrength;
    
    private Transform cameraTransform;
    private void Awake()
    {
        cameraTransform = transform;
    }

    private Vector3 GetPlayerPosition()
    {
        Vector3 playerPosition = Vector3.up;
        if(PlayerController.Instance != null )
        {
            playerPosition = PlayerController.Instance.GetPlayerTop();
        }
        return playerPosition;
    }
    private void LateUpdate()
    {
        if(cameraTransform == null)
        {
            return;
        }
        SetCameraPositionAndOrientation();
    }

    private void SetCameraPositionAndOrientation()
    {
        Vector3 playerPosition = GetPlayerPosition();

        Vector3 offset = playerPosition + this.offset;
        Vector3 lookAtOffset = playerPosition + this.lookAtOffset;

        float lerpAmount = Time.deltaTime * SmoothCameraFollowStrength;

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, offset, lerpAmount);
        cameraTransform.LookAt(Vector3.Lerp(cameraTransform.position + cameraTransform.forward,lookAtOffset,lerpAmount));
        
        cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, offset.z);
    }
}
