public enum GameState // Enum to hold game states
{
    Playing,
    Won,
    End
}

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentState = GameState.Playing;
    private bool isGameStarted = false;
    private bool gameWon;
    public bool IsGameStarted => isGameStarted;

    public void SetState(GameState state) // Method to set the game state
    {
        CurrentState = state;

        switch (CurrentState)
        {
            case GameState.Playing:
                gameWon = false;
                break;
            case GameState.Won:
                gameWon = true;
                break;
            case GameState.End:
                if (gameWon) // If the game is won
                {
                    float bonusMultiplier = FindAnyObjectByType<BonusArea>().BonusMultiplier;  // Find the BonusArea and assign the multiplier of collected bonuses
                    CurrencyManager.Instance.MultiplyLevelCurrency(bonusMultiplier); // Multiply level currency with the bonus
                    UIManager.Instance.ShowWinUI(); // Show the win UI
                }
                else // If the game is lost
                {
                    UIManager.Instance.ShowLostUI(); // Show the lost UI
                }
                break;
        }
    }
    public void StartGame()
    {
        isGameStarted = true;
        UIManager.Instance.SetGameStartUI();
    }
}
