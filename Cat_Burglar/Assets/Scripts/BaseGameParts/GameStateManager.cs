/* Author: Parker DeVenney
 * File Name: GameStateManager.cs
 * Date: 1/25/23
 * Description: Sets different game states, like paused and unpaused. 
 */
public class GameStateManager
{
    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameStateManager();
            }
            return _instance;
        }
    }
    public GameState CurrentGameState 
    {
        get;
        private set;
    }
    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler OnGameStateChanged;

    private GameStateManager()
    {

    }

    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
        {
            return;
        }

        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }
}