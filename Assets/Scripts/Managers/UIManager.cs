using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private TextMeshProUGUI gameCurrencyText;
    [SerializeField] private GameObject lostPanel;

    [Header("Starting Panel Elements")]
    [SerializeField] private GameObject startingPanel;
    [SerializeField] private TextMeshProUGUI totalCurrencyText;

    [Header("Win Panel Elements")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button getButton;

    protected override void Awake()
    {
        base.Awake();
        SetMainCurrencyText();
        winPanel.transform.localScale = Vector3.zero; // Set the size of the win panel to zero initially
        lostPanel.transform.localScale = Vector3.zero; // Set the size of the lost panel to zero initially
        getButton.onClick.AddListener(() => LevelManager.Instance.NextLevel()); // Add event to move to the next level when the button is clicked
        lostPanel.GetComponent<Button>().onClick.AddListener(() => LevelManager.Instance.RestartLevel()); // Add event to restart the level when the button is clicked
    }
    public void OpenButton(GameObject gameObject)
    {
        gameObject.SetActive(true); // Activate the object
    }
    public void CloseButton(GameObject gameObject)
    {
        gameObject.SetActive(false); // Deactivate the object
    }
    public void SetGameStartUI()
    {
        startingPanel.SetActive(false); // Deactivate the start panel
        hudPanel.SetActive(true); // Activate the HUD panel
    }
    public void ShowWinUI()
    {
        rewardText.text = CurrencyManager.Instance.LevelCurrency.ToString(); // Update the reward text
        winPanel.SetActive(true); // Activate the win panel
        winPanel.transform.DOScale(1, 1); // Scale the panel from zero to one over one second using the DoTween package function
    }
    public void ShowLostUI()
    {
        lostPanel.SetActive(true); // Activate the lost panel
        lostPanel.transform.DOScale(1, 1); // Scale the panel from zero to one over one second using the DoTween package function
    }
    public void UpdateCurrencyText(int currency)
    {
        gameCurrencyText.text = currency.ToString(); // Method to update the currency text as currency is earned in-game
    }
    public void SetMainCurrencyText()
    {
        totalCurrencyText.text = CurrencyManager.Instance.MainCurrency.ToString(); // Set the text showing the total in-game currency
    }
}