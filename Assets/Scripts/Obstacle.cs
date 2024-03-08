using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private SoundID sound = SoundID.None;

    [Header("Configuration")]
    [SerializeField] protected float heightChangeThreshold = 0.5f;
    [SerializeField] protected float heightChangeAmount = -0.15f;
    [SerializeField] protected float thicknessChangeAmount = -0.1f;

    [Header("Random Force")]
    [SerializeField] protected float minForceX = -10f;
    [SerializeField] protected float maxForceX = 10f;
    [SerializeField] protected float minForceY = 8f;
    [SerializeField] protected float maxForceY = 12f;
    [SerializeField] protected float minForceZ = 8f;
    [SerializeField] protected float maxForceZ = 12f;


    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // If the colliding object has the tag "Player"
        {
            HandlePlayerCollision(collision.gameObject); // Perform collision handling with the player
            SetColliderAsTrigger(); // Set the collider's isTrigger property to true
            ApplyRandomForce(); // Apply random force
            AudioManager.Instance.PlayEffect(sound); // Play collision sound
        }
    }

    protected virtual void HandlePlayerCollision(GameObject player)
    {
        TransformChanger transformChanger = player.GetComponent<TransformChanger>(); // Get the TransformChanger component of the player
        Transform playerTorso = player.transform.GetChild(0).GetChild(1).transform; // Get the player's torso

        float torsoHeight = playerTorso.localScale.y;  // Get the torso height

        if (torsoHeight > heightChangeThreshold)  // If the torso height is greater than a certain threshold
        {
            transformChanger.ChangeHeight(heightChangeAmount); // Change the player's height using TransformChanger
        }
        else
        {
            transformChanger.ChangeThickness(thicknessChangeAmount); // Change the player's thickness using TransformChanger
        }
    }

    protected void SetColliderAsTrigger()
    {
        GetComponent<Collider>().isTrigger = true; // Set the collider's isTrigger property to true
    }

    protected void ApplyRandomForce()
    {
        // Generate random force values
        float forceX = Random.Range(minForceX, maxForceX);
        float forceY = Random.Range(minForceY, maxForceY);
        float forceZ = Random.Range(minForceZ, maxForceZ);

        // Apply a random impulse force to the Rigidbody
        rb.AddForce(forceX, forceY, forceZ, ForceMode.Impulse);
    }
}