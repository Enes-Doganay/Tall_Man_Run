using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [Header("Thickness")]
    [SerializeField] private TextMeshProUGUI thicknessLevelText;
    [SerializeField] private TextMeshProUGUI thicknessCostText;
    private ThicknessUpgrade thicknessUpgrader;

    [Header("Height")]
    [SerializeField] private TextMeshProUGUI heightLevelText;
    [SerializeField] private TextMeshProUGUI heightCostText;
    private HeightUpgrade heightUpgrader;

    private void Start()
    {
        TransformChanger transformChanger = PlayerController.Instance.GetComponent<TransformChanger>();
        int thicknessLevel = DataController.Instance.GameData.thicknessLevel;
        int heightLevel = DataController.Instance.GameData.heightLevel;

        thicknessUpgrader = new ThicknessUpgrade(transformChanger, thicknessLevel);
        heightUpgrader = new HeightUpgrade(transformChanger, heightLevel);

        UpdateThicknessUI();
        UpdateHeightUI();
    }
    public void ThicknessUpgrade()
    {
        bool isUpgradeSuccessful = thicknessUpgrader.Upgrade();

        if (isUpgradeSuccessful)
        {
            UpdateThicknessUI();
        }
    }
    public void HeightUpgrade()
    {
        bool isUpgradeSuccessful = heightUpgrader.Upgrade();

        if (isUpgradeSuccessful)
        {
            UpdateHeightUI();
        }
    }

    private void UpdateThicknessUI()
    {
        // Update text for upgrade level and upgrade cost
        thicknessLevelText.text = "Level " + thicknessUpgrader.UpgradeLevel;
        thicknessCostText.text = thicknessUpgrader.CalculateUpgradeCost().ToString();
    }
    private void UpdateHeightUI()
    {
        // Update text for upgrade level and upgrade cost
        heightLevelText.text = "Level " + heightUpgrader.UpgradeLevel;
        heightCostText.text = heightUpgrader.CalculateUpgradeCost().ToString();
    }
}