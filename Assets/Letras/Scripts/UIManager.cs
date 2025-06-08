using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Elements")]
    [SerializeField] private CanvasGroup menuCG;
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;
    [SerializeField] private CanvasGroup gameOverCG;
    [SerializeField] private CanvasGroup settingsCG;
    [SerializeField] private CanvasGroup adPopup;
    [SerializeField] private CanvasGroup adLock;
    [SerializeField] private CanvasGroup noCoins;

    [Header("Menu Elements")]
    [SerializeField] private TextMeshProUGUI menuCoins;
    [SerializeField] private TextMeshProUGUI menuBestScore;

    [Header("Level Complete Elements")]
    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteSecretWord;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteBestScore;

    [Header("GameOver Elements")]
    [SerializeField] private TextMeshProUGUI gameOverCoins;
    [SerializeField] private TextMeshProUGUI gameOverSecretWord;
    [SerializeField] private TextMeshProUGUI gameOverBestScore;

    [Header("Game Elements")]
    [SerializeField] private TextMeshProUGUI gameCoins;
    [SerializeField] private TextMeshProUGUI gameScore;

    public static float adElapsedTime = 0;
    public static float noCoinsElapsedTime = 0;
    
    
    
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
        ShowMenu();
        HideGame();
        HideLevelComplete();
        HideGameOver();

        GameManager.onGameStateChanged += GameStateChangedCallback;
        DataManager.onCoinsUpdated += UpdateCoinsTexts;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
        DataManager.onCoinsUpdated -= UpdateCoinsTexts;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                HideGame();
                HideLevelComplete();
                HideGameOver();
                ShowMenu();
                HideAdPopup();
                HideAdLock();
                HideNoCoins();

                break;
            
            case GameState.AdLock:
                HideGame();
                HideLevelComplete();
                HideGameOver();
                HideMenu();
                ShowAdLock();
                HideAdPopup();
                HideNoCoins();

                break;
            
            case GameState.NoCoins:
                HideLevelComplete();
                HideGameOver();
                HideMenu();
                ShowGame();
                ShowNoCoins();
                HideAdLock();
                HideAdPopup();
                break;
            
            case GameState.AdPopup:
                HideGame();
                HideLevelComplete();
                HideGameOver();
                ShowMenu();
                ShowAdPopup();
                HideAdLock();
                HideNoCoins();
                break;
                
            case GameState.Game:
                HideMenu();
                HideLevelComplete();
                HideGameOver();
                ShowGame();
                HideAdPopup();
                HideAdLock();
                HideNoCoins();

                break;

            case GameState.LevelComplete:
                HideMenu();
                HideGame();
                HideGameOver();
                ShowLevelComplete();
                HideAdPopup();
                HideAdLock();
                HideNoCoins();

                break;

            case GameState.GameOver:
                HideMenu();
                HideGame();
                HideLevelComplete();
                ShowGameOver();
                HideAdPopup();
                HideAdLock();
                HideNoCoins();

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (adElapsedTime > 0)
        {   
            adElapsedTime -= Time.deltaTime;
            if (adElapsedTime <= 0)
            {
                DataManager.instance.AddCoins(30);
                adElapsedTime = 0;
                GameStateChangedCallback(GameState.Menu);
            } 
        }
        
        if (noCoinsElapsedTime > 0)
        {   
            noCoinsElapsedTime -= Time.deltaTime;
            if (noCoinsElapsedTime <= 0)
            {
                noCoinsElapsedTime = 0;
                GameStateChangedCallback(GameState.Game);
            } 
        }
    }

    private void UpdateCoinsTexts()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        gameCoins.text = DataManager.instance.GetCoins().ToString();
        levelCompleteCoins.text = DataManager.instance.GetCoins().ToString();
        gameOverCoins.text = DataManager.instance.GetCoins().ToString();
    }
    
    private void ShowMenu()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        menuBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(menuCG);
    }

    private void HideMenu()
    {
        HideCG(menuCG);
    }
    
    private void HideAdPopup()
    {
        HideCG(adPopup);
    }
    
    private void ShowAdPopup()
    {
        ShowCG(adPopup);
    }
    
    private void HideAdLock()
    {
        HideCG(adLock);
    }
    
    private void ShowAdLock()
    {
        adElapsedTime = 30;
        ShowCG(adLock);
    }
    
    private void ShowNoCoins()
    {
        noCoinsElapsedTime = 3;
        ShowCG(noCoins);
    }
    
    private void HideNoCoins()
    {
        HideCG(noCoins);
    }

    private void ShowGame()
    {
        gameCoins.text = DataManager.instance.GetCoins().ToString();
        gameScore.text = DataManager.instance.GetScore().ToString();

        ShowCG(gameCG);
    }

    private void HideGame()
    {
        HideCG(gameCG);
    }

    private void ShowLevelComplete()
    {   
        levelCompleteCoins.text = DataManager.instance.GetCoins().ToString();
        levelCompleteSecretWord.text = WordManager.instance.GetSecretWord();
        levelCompleteScore.text = DataManager.instance.GetScore().ToString();
        levelCompleteBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(levelCompleteCG);
    }

    private void HideLevelComplete()
    {
        HideCG(levelCompleteCG);
    }

    private void ShowGameOver()
    {   
        gameOverCoins.text = DataManager.instance.GetCoins().ToString();
        gameOverSecretWord.text = WordManager.instance.GetSecretWord();
        gameOverBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(gameOverCG);
    }

    private void HideGameOver()
    {
        HideCG(gameOverCG);
    }

    public void ShowSettings()
    {
        ShowCG(settingsCG);
    }

    public void HideSettings()
    {
        HideCG(settingsCG);
    }

    private void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    
    private void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
