using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinContainer : MonoBehaviour
{
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI costText;
    private SkinShopAndSelection skinController;
    private PlayerSkinData playerSkinData;
    public PlayerSkinData PlayerSkinData => playerSkinData;
    public void SetShopItem(PlayerSkinData playerSkinData, SkinShopAndSelection controller)
    {
        // Set the player skin data and the controller
        this.playerSkinData = playerSkinData;
        skinController = controller;

        if (skinController.IsSkinOwned(playerSkinData)) // If the player owns this skin, make it selectable
            selectButton.interactable = true;

        // Set the color of the skin image to the player's skin color
        itemImage.color = playerSkinData.SkinColor;
        costText.text = playerSkinData.Cost.ToString();

        // Update the visibility of the buy button
        UpdateBuyButtonVisibility();

        // Add button listeners
        AddButtonListeners();
    }

    private void UpdateBuyButtonVisibility()
    {
        // Update the visibility of the buy button based on whether the player owns the skin or not
        buyButton.gameObject.SetActive(!skinController.IsSkinOwned(playerSkinData));
    }

    private void AddButtonListeners()
    {
        // Add click listeners to the buy and select buttons
        buyButton.onClick.AddListener(() => OnBuyButtonClicked());
        selectButton.onClick.AddListener(() => OnSelectButtonClicked());
    }
    public void OnBuyButtonClicked()
    {
        // Purchase the player skin and update the shop item
        skinController.PurchasePlayerSkin(playerSkinData);
        SetShopItem(playerSkinData, skinController);
    }
    public void OnSelectButtonClicked()
    {
        // Select the player skin
        skinController.SelectPlayerSkin(playerSkinData);
    }
    public void SetSelectionVisual(bool isSelected)
    {
        // Update the color of the selection button based on whether it is selected or not
        selectButton.GetComponent<Image>().color = isSelected ? selectedColor : normalColor;
    }
}