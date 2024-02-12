using MoonActive.Scripts;
public class InMemoryStorage : StorageType
{
    private GameState gameState;

    public override void SaveGameState(GameState state)
    {
        // Save the provided game state directly to memory
        gameState = state;
    }

    public override GameState LoadGameState()
    {
        // Return the game state stored in memory
        return gameState;
    }
}
