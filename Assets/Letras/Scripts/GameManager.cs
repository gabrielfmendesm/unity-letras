using UnityEngine;
using System;

public enum GameState { Menu, Game, LevelComplete, GameOver, Idle, AdPopup, AdLock, NoCoins }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Settings")]
    private GameState gameState;

    [Header("Events")]
    public static Action<GameState> onGameStateChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextButtonCallback()
    {
        SetGameState(GameState.Game);
    }

    public void PlayButtonCallback()
    {
        SetGameState(GameState.Game);
    }
    
    public void ShowAdPopup()
    {
        SetGameState(GameState.AdPopup);
    }
    
    public void ShowMenu()
    {
        
        SetGameState(GameState.Menu);
    }
    
    public void ShowNoCoins()
    {
        
        SetGameState(GameState.NoCoins);
    }

    
    public void ShowAdLock()
    {
        SetGameState(GameState.AdLock);
    }

    
    public void BackButtonCallback()
    {
        SetGameState(GameState.Menu);
    }
}
