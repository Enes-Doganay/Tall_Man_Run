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
        xPos = transform.position.x;
        zPos = transform.position.z;
        ResetSpeed();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        //Update Speed
        if (!hasInput)
        {
            Decelerate(deltaTime, 0.0f);
        }
        else if(targetMoveSpeed < moveSpeed)
        {
            Decelerate(deltaTime, targetMoveSpeed);
        }
        else if(targetMoveSpeed > moveSpeed)
        {
            Accelerate(deltaTime, targetMoveSpeed);
        }
        float speed = moveSpeed * deltaTime;

        //Update Position

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

        if(animator != null && deltaTime > 0.0f)
        {
            float distanceTravelledSinceLastFrame = (transform.position - lastPosition).magnitude;
            float distancePerSecond = distanceTravelledSinceLastFrame / deltaTime;

            animator.SetFloat("Speed", distancePerSecond);
        }

        if(transform.position != lastPosition)
        {
            transform.forward = Vector3.Lerp(transform.forward, (transform.position - lastPosition).normalized, speed);
        }
        lastPosition = transform.position;
    }

    public void AdjustMoveSpeed(float speed)
    {
        targetMoveSpeed += speed;
        targetMoveSpeed = Mathf.Max(0, targetMoveSpeed);
    }
    public void ResetSpeed()
    {
        targetMoveSpeed = defaultMoveSpeed;
    }
    public void SetDeltaPosition(float normalizedDeltaPosition)
    {
        float fullWidth = maxXPosition * 2;
        targetPosition = targetPosition + fullWidth * normalizedDeltaPosition;
        targetPosition = Mathf.Clamp(targetPosition, -maxXPosition, maxXPosition);
        hasInput = true;
        Debug.Log("setdelta");
    }
    private void Accelerate(float deltaTime, float targetSpeed)
    {
        moveSpeed += deltaTime * accelerationSpeed;
        moveSpeed = Mathf.Min(moveSpeed, targetSpeed);
    }
    private void Decelerate(float deltaTime, float targetSpeed)
    {
        moveSpeed -= deltaTime * decelerationSpeed;
        moveSpeed = Mathf.Max(moveSpeed, targetSpeed);
    }
    public void CancelMovement()
    {
        hasInput = false;
    }
    public void SetAnimator(bool active)
    {
        animator.enabled = false;
    }
    public Vector3 GetPlayerTop()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        return transform.position + col.center + Vector3.up * (col.height * 0.5f);
    }
}