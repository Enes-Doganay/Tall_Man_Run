using UnityEngine;

public class BonusObstacle : Obstacle
{
    private BonusArea bonusArea;
    protected override void Awake()
    {
        base.Awake();
        bonusArea = FindObjectOfType<BonusArea>();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        // Double the bonus earned when colliding with the bonus obstacle
        if (collision.gameObject.CompareTag("Player"))
        {
            bonusArea.UpdateBonusMultiplier();
        }
    }

}
