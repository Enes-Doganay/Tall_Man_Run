using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerHead;
    [SerializeField] private float customPlayerSpeed = 10.0f;
    [SerializeField] private float accelerationSpeed = 10.0f;
    [SerializeField] private float decelerationSpeed = 20.0f;
    [SerializeField] private float horizontalSpeedFactor = 0.5f;
    [SerializeField] private float maxXPosition = 5f;
    [SerializeField] private float defaultMoveSpeed = 10;

    private Vector3 lastPosition;
    private bool canMove;
    private bool hasInput;
    private float xPos;
    private float zPos;

    private float moveSpeed;
    private float targetMoveSpeed;
    private float targetPosition;

    public float MoveSpeed => moveSpeed;
    public float TargetMoveSpeed => targetMoveSpeed;
    public float MaxXPoisiton => maxXPosition;
    public float TargetPosition => targetPosition;

    protected override void Awake()
    {
        canMove = true; // Enable movement ability initially

        // Save initial x and z positions
        xPos = transform.position.x;
        zPos = transform.position.z;

        // Reset speed
        ResetSpeed();
    }

    private void Update()
    {
        // If movement ability is disabled, exit the operation
        if (!canMove)
            return;

        // Start death process if the player falls
        if (transform.position.y < -2f)
        {
            GetComponent<PlayerDead>().Death();
        }

        // Store a variable related to time
        float deltaTime = Time.deltaTime;

        // Update Speed
        if (!hasInput)
        {
            Decelerate(deltaTime, 0.0f);
        }
        else if (targetMoveSpeed < moveSpeed)
        {
            Decelerate(deltaTime, targetMoveSpeed);
        }
        else if (targetMoveSpeed > moveSpeed)
        {
            Accelerate(deltaTime, targetMoveSpeed);
        }
        float speed = moveSpeed * deltaTime;

        // Update Position
        zPos += speed;

        if (hasInput)
        {
            float horizontalSpeed = speed * horizontalSpeedFactor;
            float newPositionTarget = Mathf.Lerp(xPos, targetPosition, horizontalSpeed);
            float newPositionDifference = newPositionTarget - xPos;

            newPositionDifference = Mathf.Clamp(newPositionDifference, -horizontalSpeed, horizontalSpeed);
            xPos += newPositionDifference;
        }
        transform.position = new Vector3(xPos, transform.position.y, zPos);

        // Update Animator's speed if it exists and deltaTime is positive
        if (animator != null && deltaTime > 0.0f)
        {
            float distanceTravelledSinceLastFrame = (transform.position - lastPosition).magnitude;
            float distancePerSecond = distanceTravelledSinceLastFrame / deltaTime;

            animator.SetFloat("Speed", distancePerSecond);
        }

        // Update character direction if position has changed
        if (transform.position != lastPosition)
        {
            transform.forward = Vector3.Lerp(transform.forward, (transform.position - lastPosition).normalized, speed);
        }
        lastPosition = transform.position;
    }

    public void AdjustMoveSpeed(float speed)
    {
        // Increase or decrease target movement speed
        targetMoveSpeed += speed;

        // Clamp target movement speed to minimum of 0
        targetMoveSpeed = Mathf.Max(0, targetMoveSpeed);
    }
    public void ResetSpeed()
    {
        // Reset target movement speed to default movement speed
        targetMoveSpeed = defaultMoveSpeed;
    }
    public void SetDeltaPosition(float normalizedDeltaPosition)
    {
        // Calculate full width on X axis
        float fullWidth = maxXPosition * 2;

        // Update target position with normalized delta position
        targetPosition = targetPosition + fullWidth * normalizedDeltaPosition;

        // Clamp target position within certain limits
        targetPosition = Mathf.Clamp(targetPosition, -maxXPosition, maxXPosition);
        hasInput = true;
    }
    public void Jump(Vector3 jumpEndPoint, float jumpPower, float duration)
    {
        // Store current height position
        float currentYPos = transform.position.y;

        // Set Y axis of jump target to current height position
        jumpEndPoint.y = currentYPos;

        // Disable movement
        canMove = false;

        // Use DOTween library to make the character jump and enable movement again when completed
        transform.DOJump(jumpEndPoint, jumpPower, 1, duration).OnComplete(() =>
        {
            canMove = true;

            // Update X and Z positions to jump point
            xPos = jumpEndPoint.x;
            zPos = jumpEndPoint.z;
        });
    }
    private void Accelerate(float deltaTime, float targetSpeed)
    {
        // Accelerate movement speed
        moveSpeed += deltaTime * accelerationSpeed;

        // Limit movement speed to target speed
        moveSpeed = Mathf.Min(moveSpeed, targetSpeed);
    }
    private void Decelerate(float deltaTime, float targetSpeed)
    {
        // Decelerate movement speed
        moveSpeed -= deltaTime * decelerationSpeed;

        // Limit movement speed to target speed
        moveSpeed = Mathf.Max(moveSpeed, targetSpeed);
    }
    public void CancelMovement()
    {
        // Cancel movement input
        hasInput = false;
    }
    public void SetAnimator(bool active)
    {
        // Set animator's activity to specified state
        animator.enabled = false;
    }
    public void SetMaxXPosition(float value)
    {
        // Set maximum X position to specified value
        maxXPosition = value;
    }
    public Vector3 GetPlayerTop()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        // Calculate player's top as capsule collider's center plus half of its height from upward
        return transform.position + col.center + Vector3.up * (col.height * 0.5f);
    }
}