using TMPro;
using UnityEngine;

public class SkinShopAndSelection : MonoBehaviour
{
    [SerializeField] private PlayerSkinDatabase skinDB;
    [SerializeField] private Material playerSkinMat;
    [SerializeField] private SkinContainer skinContainer;
    [SerializeField] private Transform skinContentTransform;
    [SerializeField] private TextMeshProUGUI currencyText;

    private int selectedSkinID;

    private void Awake()
    {
        // Loop through each player skin in the skin database
        for (int i = 0; i < skinDB.PlayerSkins.Count; i++)
        {
            // Clone the purchase item and set its binding
            SkinContainer shopItem = Instantiate(skinContainer, skinContentTransform);
            shopItem.SetShopItem(skinDB.PlayerSkins[i], this);
        }
        // Get the ID of the selected skin
        selectedSkinID = DataController.Instance.GameData.selectedSkinID;

        // Determine the color of the selected skin
        Color selectedSkinColor = skinDB.PlayerSkins.Find(skin => skin.SkinID == selectedSkinID).SkinColor;

        // Update the color of the player skin
        playerSkinMat.color = selectedSkinColor;

        UpdateSkinSelectionVisual();
    }

    private void UpdateSkinSelectionVisual()
    {
        // Check all skin items and update their visuals based on whether they are selected or not
        foreach (Transform child in skinContentTransform)
        {
            SkinContainer skinContainer = child.GetComponent<SkinContainer>();
            if (skinContainer != null)
            {
                PlayerSkinData skinData = skinContainer.PlayerSkinData;
                skinContainer.SetSelectionVisual(selectedSkinID == skinData.SkinID);
            }
        }
    }

    public void PurchasePlayerSkin(PlayerSkinData skin)
    {
        // If the skin is not owned and is purchasable
        if (!IsSkinOwned(skin) && IsPurchasable(skin.Cost))
        {
            // Decrease the main currency
            CurrencyManager.Instance.DecreaseMainCurrency(skin.Cost);

            // Update the main currency on the UI
            UIManager.Instance.SetMainCurrencyText();

            // Add the purchased skin and save
            DataController.Instance.GameData.ownedSkins.Add(skin);
            DataController.Instance.Save();
        }
    }
    public void SelectPlayerSkin(PlayerSkinData skin)
    {
        // Update the ID of the selected skin
        selectedSkinID = skin.SkinID;

        // Update the color of the player skin
        playerSkinMat.color = skin.SkinColor;
        UpdateSkinSelectionVisual();

        // Assign the selected skin to the game data and save
        DataController.Instance.GameData.selectedSkinID = skin.SkinID;
        DataController.Instance.Save();
    }
    public bool IsSkinOwned(PlayerSkinData skin)
    {
        // Return true if the skin is found in the owned skins list
        return DataController.Instance.GameData.ownedSkins.Contains(skin);
    }
    public bool IsPurchasable(int cost)
    {
        // Check if the main currency is greater than or equal to the cost
        return CurrencyManager.Instance.MainCurrency >= cost;
    }
}