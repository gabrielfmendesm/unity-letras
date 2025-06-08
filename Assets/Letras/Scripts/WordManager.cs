using UnityEngine;
using UnityEngine.UI;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;

    [Header("Elements")]
    [SerializeField] private string secretWord;
    [SerializeField] private TextAsset wordsText;
    private string words;

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

        words = wordsText.text;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetNewSecretWord();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetSecretWord()
    {
        return secretWord.ToUpper();
    }

    private void SetNewSecretWord()
    {
        int wordCount = (words.Length + 1) / 6;

        int wordIndex = Random.Range(0, wordCount);

        int wordStartIndex = wordIndex * 6;

        secretWord = words.Substring(wordStartIndex, 5).ToUpper();
    }
}
