using UnityEngine;

public class Diamond : Collectable
{
    [SerializeField] private GameObject diamondParticle;

    public override void Collect()
    {
        base.Collect();
        // Add to LevelCurrency
        CurrencyManager.Instance.AddLevelCurrency(1);

        // Create Diamond effect
        Instantiate(diamondParticle, transform.position, Quaternion.identity);

        // Deactivate the Diamond
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collect when Player collides with the diamond
        if (other.gameObject.CompareTag("Player"))
            Collect();
    }
}