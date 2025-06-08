using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("Data")]
    private int coins;
    private int score;
    private int bestScore;

    [Header("Events")]
    public static Action onCoinsUpdated;

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

        LoadData();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCoins(int amount)
    {
        coins += amount;
        SaveData();

        onCoinsUpdated?.Invoke();
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        if (coins < 0)
        {
            coins = 0;
        }
        SaveData();

        onCoinsUpdated?.Invoke();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        if (score > bestScore)
        {
            bestScore = score;
        }
        SaveData();
    }

    public void ResetScore()
    {
        score = 0;
        SaveData();
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void LoadData()
    {
        coins = PlayerPrefs.GetInt("Coins", 150);
        score = PlayerPrefs.GetInt("Score");
        bestScore = PlayerPrefs.GetInt("BestScore");
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("BestScore", bestScore);
    }
}
