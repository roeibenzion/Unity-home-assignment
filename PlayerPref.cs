using UnityEngine;
using MoonActive.Scripts;

public class PlayerPrefsManager : StorageType
{
    private const string GameStateKey = "GameState";

    public override void SaveGameState(GameState gameState)
    {
        // Convert GameState object to JSON string
        string json = JsonUtility.ToJson(gameState);

        // Save JSON string to PlayerPrefs
        PlayerPrefs.SetString(GameStateKey, json);
    }

    public override GameState LoadGameState()
    {
        // Load JSON string from PlayerPrefs
        string json = PlayerPrefs.GetString(GameStateKey);

        // If no saved data found, return null
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }

        // Convert JSON string back to GameState object
        GameState gameState = JsonUtility.FromJson<GameState>(json);
        
        // Return the loaded GameState object
        return gameState;
    }
}