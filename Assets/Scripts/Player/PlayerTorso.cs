using UnityEngine;

public class PlayerTorso : MonoBehaviour
{
    [SerializeField] private TransformChanger transformChanger;
    [SerializeField] private Material bodyMat;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) // if the colliding object has the Obstacle tag
        {
            float value = other.bounds.size.y * 0.5f;  // multiply the colliding object's y size by 0.5
            transformChanger.ChangeHeight(-value); // adjust the height using transformChanger


            // To simulate a fragmentation on the character based on the colliding obstacle size
            // Create a capsule, add necessary components, and apply force
            GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Capsule);

            piece.GetComponent<MeshRenderer>().material = bodyMat;
            piece.GetComponent<Collider>().enabled = false;
            Rigidbody pieceRb = piece.AddComponent<Rigidbody>();

            piece.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
            piece.transform.localScale = new Vector3(transform.lossyScale.x, value, transform.lossyScale.z);

            pieceRb.AddForce(-1, 1, -0.5f, ForceMode.Impulse);
            pieceRb.AddTorque(75, 15, 45);
        }
    }
}