using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Elements")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;

    [Header("Settings")]
    private int currentWordContainerIndex;
    private bool canAddLetter = true;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();

        KeyboardKey.onKeyPressed += KeyPressedCallback;
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        KeyboardKey.onKeyPressed -= KeyPressedCallback;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                Initialize();
                break;

            case GameState.LevelComplete:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Initialize()
    {
        currentWordContainerIndex = 0;
        canAddLetter = true;
        DisableTryButton();

        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
        }
    }

    private void KeyPressedCallback(char letter)
    {
        if (!canAddLetter || currentWordContainerIndex >= wordContainers.Length)
        {
            return;
        }

        wordContainers[currentWordContainerIndex].AddLetter(letter);

        if (wordContainers[currentWordContainerIndex].IsComplete)
        {
            canAddLetter = false;
            EnableTryButton();
        }
    }

    public void CheckWord()
    {
        string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.instance.GetSecretWord();

        wordContainers[currentWordContainerIndex].Colorize(secretWord);
        keyboardColorizer.Colorize(secretWord, wordToCheck);

        if (wordToCheck.Equals(secretWord))
        {
            SetLevelComplete();
        }
        else
        {
            // Debug.Log("Incorrect word: " + wordToCheck);
            currentWordContainerIndex++;
            DisableTryButton();

            if (currentWordContainerIndex >= wordContainers.Length)
            {
                // Debug.Log("GameOver");
                DataManager.instance.ResetScore();
                GameManager.instance.SetGameState(GameState.GameOver);
            }
            else
            {
                canAddLetter = true;
                DisableTryButton();
            }
        }
    }

    private void SetLevelComplete()
    {
        UpdateData();

        GameManager.instance.SetGameState(GameState.LevelComplete);
    }

    private void UpdateData()
    {
        int scoreToAdd = 6 - currentWordContainerIndex;

        DataManager.instance.IncreaseScore(scoreToAdd);
        DataManager.instance.AddCoins(scoreToAdd * 3);
    }

    public void BackspacePressedCallback()
    {
        bool removedLetter = wordContainers[currentWordContainerIndex].RemoveLetter();

        if (removedLetter)
        {
            DisableTryButton();
        }

        canAddLetter = true;
    }

    private void EnableTryButton()
    {
        tryButton.interactable = true;
    }

    private void DisableTryButton()
    {
        tryButton.interactable = false;
    }
    
    public WordContainer GetCurrentWordContainer()
    {
        return wordContainers[currentWordContainerIndex];
    }
}
