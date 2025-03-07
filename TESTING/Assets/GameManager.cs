using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action<GAME_STATE> onGameStateChanged;
    public Action<PLAYER_STATE> onPlayerStateChanged;

    [Header("Game State and player")]
    public GAME_STATE currentGameState;
    public PLAYER_STATE currentPlayerState;

    public int strokesAmount;

    public void ChangeGameState(GAME_STATE _newGameState)
    {
        if (currentGameState == _newGameState) return;

        currentGameState = _newGameState;
        Debug.Log("Game state changed to: " + currentGameState);

        if (onGameStateChanged != null)
        {
            onGameStateChanged.Invoke(currentGameState);
        }
    }


    public void ChangePlayerState(PLAYER_STATE _newPlayerState)
    {
        if (currentPlayerState == _newPlayerState) return;

        currentPlayerState = _newPlayerState;
        Debug.Log("Game state changed to: " + currentPlayerState);

        if (onPlayerStateChanged != null)
        {
            onPlayerStateChanged.Invoke(currentPlayerState);
        }
    }
}

public enum GAME_STATE
{
    PLAY,
    PAUSE,
    GAME_OVER
}

public enum PLAYER_STATE
{
    NORMAL,
    FIRE
}
