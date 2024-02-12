using MoonActive.Scripts;
public abstract class StorageType
{
    public abstract void SaveGameState(GameState gameState);
    public abstract GameState LoadGameState();
}