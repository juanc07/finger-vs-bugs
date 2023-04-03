using UnityEngine;
using System.Collections;

public class DataController : MonoBehaviour
{

    // this class is used for debuging

    //player data - you can change player data here and it will used in the game
    public PlayerData playerData;

    // you will see the game state it will be updated every time  game state change
    public GameState gameState;

    // you will see the game mode it will be updated every time  game state change
    public GameMode gameMode;

    public bool isRandomBackground;

    private GameDataManager gameDataManager;

    // Use this for initialization
    void Awake()
    {
        gameDataManager = GameDataManager.GetInstance();
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public void SetPlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public void SetGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
    }

    public void ResetSaveData()
    {        
        // resets the score and best score
        gameDataManager.SetScore(0);
        gameDataManager.SetBestScore(0);
        gameDataManager.SetKidsBestScore(0);
        gameDataManager.SetCombo(0);
        gameDataManager.SetGameCount(0);
        gameDataManager.SetTotalGameCount(0);

        ISave saveDataService = ServiceLocator.GetPlayerPrefDataService();
        // resets all save data on player pref
        saveDataService.DeleteAll();
    }
}
