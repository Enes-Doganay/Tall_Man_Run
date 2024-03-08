using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    [SerializeField] private Transform jumpEndPoint;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpDuration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // If the colliding object has the "Player" tag
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.Jump(jumpEndPoint.position, jumpPower, jumpDuration); // Make the player jump to the jumpEndPoint position
            }
        }
    }
}