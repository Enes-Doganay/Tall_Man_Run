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
        // Set the player position defaulting to the up vector
        Vector3 playerPosition = Vector3.up;
        if (PlayerController.Instance != null)
        {
            // Get the player's top position
            playerPosition = PlayerController.Instance.GetPlayerTop();
        }
        return playerPosition; // Return the player position
    }
    private void LateUpdate()
    {
        if (cameraTransform == null)
        {
            return;
        }

        // Set the camera position and orientation
        SetCameraPositionAndOrientation();
    }

    private void SetCameraPositionAndOrientation()
    {
        // Get the player position
        Vector3 playerPosition = GetPlayerPosition();

        // Set the camera position and look direction with a specific offset
        Vector3 offset = playerPosition + this.offset;
        Vector3 lookAtOffset = playerPosition + this.lookAtOffset;

        // Determine the amount of interpolation (multiplied by Time.deltaTime for smooth tracking)
        float lerpAmount = Time.deltaTime * SmoothCameraFollowStrength;

        // Set the camera position with smooth transition
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, offset, lerpAmount);

        // Set the camera look direction with a specific offset (multiplied by Time.deltaTime for smooth tracking)
        cameraTransform.LookAt(Vector3.Lerp(cameraTransform.position + cameraTransform.forward, lookAtOffset, lerpAmount));

        // Lock only the z-axis of the camera (only track on x and y axes)
        cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, offset.z);
    }
}