using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;

    [Header("Sounds")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource letterAddedSound;
    [SerializeField] private AudioSource letterRemovedSound;
    [SerializeField] private AudioSource levelCompletedSound;
    [SerializeField] private AudioSource gameOverSound;

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
        InputManager.onLetterAdded += PlayLetterAddedSound;
        InputManager.onLetterRemoved += PlayLetterRemovedSound;

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        InputManager.onLetterAdded -= PlayLetterAddedSound;
        InputManager.onLetterRemoved -= PlayLetterRemovedSound;

        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.LevelComplete:
                levelCompletedSound.Play();
                break;

            case GameState.GameOver:
                gameOverSound.Play();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButtonSound()
    {
        buttonSound.Play();
    }

    private void PlayLetterAddedSound()
    {
        letterAddedSound.Play();
    }

    private void PlayLetterRemovedSound()
    {
        letterRemovedSound.Play();
    }

    public void EnableSounds()
    {
        buttonSound.volume = 1;
        letterAddedSound.volume = 1;
        letterRemovedSound.volume = 1;
        levelCompletedSound.volume = 1;
        gameOverSound.volume = 1;
    }

    public void DisableSounds()
    {
        buttonSound.volume = 0;
        letterAddedSound.volume = 0;
        letterRemovedSound.volume = 0;
        levelCompletedSound.volume = 0;
        gameOverSound.volume = 0;
    }
}
