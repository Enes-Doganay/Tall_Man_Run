using UnityEngine;

public class HeightUpgrade : IUpgrade
{
    private TransformChanger transformChanger;
    private int upgradeLevel;
    private int defaultUpgradeCost = 50;
    private int maxUpgradeLevel = 50;
    private int costMultiplier = 10;
    private float heightValue = 3;
    public int UpgradeLevel => upgradeLevel;
    public HeightUpgrade(TransformChanger transformChanger, int upgradeLevel)
    {
        this.transformChanger = transformChanger;
        this.upgradeLevel = upgradeLevel;
    }

    public int CalculateUpgradeCost()
    {
        // Calculate the upgrade cost
        return defaultUpgradeCost + (upgradeLevel * costMultiplier);
    }

    public bool IsUpgradeable(int upgradeCost)
    {
        // Return true if the upgrade cost is less than the main currency and the upgrade level is less than the maximum upgrade level
        return upgradeCost < CurrencyManager.Instance.MainCurrency && upgradeLevel < maxUpgradeLevel;
    }

    public bool Upgrade()
    {
        // Calculate the upgrade cost
        int upgradeCost = CalculateUpgradeCost();

        // If the upgrade is possible
        if (IsUpgradeable(upgradeCost))
        {
            // Increase the height in the transform changer
            transformChanger.ChangeHeight(heightValue * transformChanger.DefaultHeightMultiplier);

            // Increase the upgrade level by one
            upgradeLevel += 1;

            // Decrease the main currency
            CurrencyManager.Instance.DecreaseMainCurrency(upgradeCost);

            // Update the main currency text
            UIManager.Instance.SetMainCurrencyText();

            // Increase the height level in the game data and save the data
            DataController.Instance.GameData.heightLevel += 1;
            DataController.Instance.Save();
            return true;
        }
        return false;
    }
}