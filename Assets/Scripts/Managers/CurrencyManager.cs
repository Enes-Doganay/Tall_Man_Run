using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private int levelCurrency = 0;
    private int mainCurrency;
    public int LevelCurrency => levelCurrency;
    public int MainCurrency => mainCurrency;
    protected override void Awake()
    {
        base.Awake();
        mainCurrency = DataController.Instance.GameData.currency;   // Assign the data saved to the mainCurrency variable at the beginning
    }
    public void AddLevelCurrency(int amount)
    {
        levelCurrency += amount;    // Increase levelCurrency by the amount
        UIManager.Instance.UpdateCurrencyText(levelCurrency); // and update its text
    }
    public void AddMainCurrency(int amount)
    {
        DataController.Instance.GameData.currency += amount;    // Increase the currency in GameData by the amount
        DataController.Instance.Save(); // and save
    }

    public void DecreaseMainCurrency(int amount)
    {
        mainCurrency -= amount;     // Decrease the variable holding the main currency by the amount
        DataController.Instance.GameData.currency -= amount; // Decrease from the saved data and save
        DataController.Instance.Save();
    }
    public void MultiplyLevelCurrency(float multiplierValue)
    {
        levelCurrency = Mathf.FloorToInt(levelCurrency * multiplierValue); // Multiply levelCurrency and round down to the nearest integer
    }
    public void TransferToMainCurrency()
    {
        AddMainCurrency(levelCurrency); // Add levelCurrency to mainCurrency and save
        levelCurrency = 0;
    }
}