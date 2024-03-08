using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputManager : Singleton<InputManager>
{
    private Vector3 inputPosition;
    private Vector3 previousInputPosition;
    private bool hasInput;

    [SerializeField]
    private float inputSensitivity;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable(); // Enable touch support when the script is enabled
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable(); // Disable touch support when the script is disabled
    }
    private void Update()
    {
        if (PlayerController.Instance == null)
        {
            return;
        }

        if (!GameManager.Instance.IsGameStarted)
        {
            return;
        }

#if UNITY_EDITOR
        inputPosition = Mouse.current.position.ReadValue(); // Read the current position of the mouse

        if (Mouse.current.leftButton.isPressed) // If left mouse button is pressed
        {
            if (!hasInput) // If there is no input
            {
                previousInputPosition = inputPosition; // Assign the current mouse position to the previous input position
            }
            hasInput = true;
        }
        else
        {
            hasInput = false;
        }
#else
        if(Touch.activeTouches.Count > 0) // If the count of active touches is greater than 0
        {
            inputPosition = Touch.activeTouches[0].screenPosition; // Get the screen position of the first active touch

            if (!hasInput)
            {
                previousInputPosition = inputPosition; // Assign the current input position to the previous input position
            }
            hasInput= true;
        }
#endif
        if (hasInput) // If there is input
        {
            float normalizedDeltaPosition = (inputPosition.x - previousInputPosition.x) / Screen.width * inputSensitivity; // Calculate the difference between the current input position and the previous input position, divide it by screen width and multiply by input sensitivity to normalize
            PlayerController.Instance.SetDeltaPosition(normalizedDeltaPosition); // Set player's position as normalizedDeltaPosition
        }
        else
        {
            PlayerController.Instance.CancelMovement(); // If there is no input, cancel player movement
        }
        previousInputPosition = inputPosition;
    }
}