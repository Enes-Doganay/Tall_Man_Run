using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem confetti;

    [SerializeField]
    private SoundID confettiSound = SoundID.None;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetState(GameState.Won); // Set the game state to won
            AudioManager.Instance.PlayEffect(confettiSound);
            confetti.Play(); // Play the confetti effect
        }
    }
}