using UnityEngine;
using System.Collections;

public interface IMediator
{
    // game events
    void StartGame();

    void PausedGame();

    void ResumeGame();

    void QuitGame();

    void RestartGame();

    void GameOver();

    void QuitApplication();

    void StartResumeCountdown();

    void SetGameState(GameState gameState);

    GameState GetGameState();
}
