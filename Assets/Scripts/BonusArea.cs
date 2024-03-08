using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> bonusModelContainers;
    [SerializeField] private float bonusIncreaseValue = 0.3f;
    private float bonusMultiplier = 1.0f;
    public float BonusMultiplier => bonusMultiplier;

    private void Awake()
    {
        float bonusValue = bonusMultiplier;

        // Assign bonus value to the models displaying bonuses
        foreach (var container in bonusModelContainers)
        {
            bonusValue += bonusIncreaseValue;
            container.transform.GetChild(0).GetComponentInChildren<TextMeshPro>().text = bonusValue.ToString();
            container.transform.GetChild(1).GetComponentInChildren<TextMeshPro>().text = bonusValue.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // After the player enters the area
            PlayerController.Instance.SetMaxXPosition(0); // Set the maximum position the player can go to as 0 to prevent movement
    }

    public void UpdateBonusMultiplier()
    {
        // Increase the bonus multiplier based on the obstacles surpassed
        bonusMultiplier += bonusIncreaseValue;
        Debug.Log("bonus multiplier " + bonusMultiplier);
    }
}