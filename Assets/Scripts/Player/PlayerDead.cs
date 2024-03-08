using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField] private GameObject head;
    public void Death()
    {
        GameObject headClone = Instantiate(head, head.transform.position, Quaternion.identity); // Create a copy of the character's head
        headClone.AddComponent<SphereCollider>(); // Add a Sphere Collider component
        Rigidbody headCloneRb = headClone.AddComponent<Rigidbody>(); // Add a Rigidbody component
        headCloneRb.AddForce(0, 2, 5, ForceMode.Impulse); // Apply a force

        GameManager.Instance.SetState(GameState.End); // Set the game state to end
        gameObject.SetActive(false); // Deactivate the object
    }
}