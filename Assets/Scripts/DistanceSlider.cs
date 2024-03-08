using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private Slider distanceSlider;
    private Transform finishLine;
    private void Awake()
    {
        // Get the Slider component
        distanceSlider = GetComponent<Slider>();

        // Find the FinishLine object and get its transform
        finishLine = GameObject.Find("FinishLine").transform;

        Initialize();
    }
    private void Initialize()
    {
        // Get the level from game data and update the level text
        int level = DataController.Instance.GameData.level;
        levelText.text = level.ToString();

        // Set the maximum and minimum values of the slider
        distanceSlider.maxValue = finishLine.position.z - PlayerController.Instance.transform.position.z;
        distanceSlider.minValue = 0;
    }
    private void Update()
    {
        // If the slider object is active and the PlayerController object is not null
        if (distanceSlider.gameObject.activeInHierarchy && PlayerController.Instance != null)
        {
            // Calculate the total distance
            float totalDistance = finishLine.position.z - PlayerController.Instance.transform.position.z;

            // Calculate the travelled distance
            float travelledDistance = distanceSlider.maxValue - totalDistance;

            // Set the value of the slider based on the travelled distance
            distanceSlider.value = travelledDistance;
        }
    }
}