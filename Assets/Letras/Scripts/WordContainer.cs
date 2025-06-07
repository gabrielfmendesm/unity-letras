using System.Collections.Generic;
using UnityEngine;

public class WordContainer : MonoBehaviour
{

    [Header("Elements")]
    private LetterContainer[] letterContainers;

    [Header("Settings")]
    private int currentLetterIndex;

    private void Awake()
    {
        letterContainers = GetComponentsInChildren<LetterContainer>();
        //Initialize();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {
        for (int i = 0; i < letterContainers.Length; i++)
        {
            letterContainers[i].Initialize();
        }
    }

    public void AddLetter(char letter)
    {
        letterContainers[currentLetterIndex].SetLetter(letter);
        currentLetterIndex++;
    }

    public bool RemoveLetter()
    {
        if (currentLetterIndex <= 0)
        {
            return false;
        }

        currentLetterIndex--;
        letterContainers[currentLetterIndex].Initialize();

        return true;
    }

    public string GetWord()
    {
        string word = string.Empty;

        for (int i = 0; i < letterContainers.Length; i++)
        {
            word += letterContainers[i].GetLetter().ToString();
        }

        return word;
    }

    public void Colorize(string secretWord)
    {
        int n = letterContainers.Length;
        var guess = new char[n];
        for (int i = 0; i < n; i++)
            guess[i] = letterContainers[i].GetLetter();

        var counts = new Dictionary<char, int>();
        foreach (char c in secretWord)
            counts[c] = counts.GetValueOrDefault(c, 0) + 1;

        var status = new int[n];

        for (int i = 0; i < n; i++)
        {
            if (guess[i] == secretWord[i])
            {
                status[i] = 1;
                counts[guess[i]]--;
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (status[i] != 0) continue;

            char c = guess[i];
            if (counts.GetValueOrDefault(c, 0) > 0)
            {
                status[i] = 2;
                counts[c]--;
            }
            else
            {
                status[i] = 3;
            }
        }

        for (int i = 0; i < n; i++)
        {
            switch (status[i])
            {
                case 1:
                    letterContainers[i].SetValid();
                    break;
                case 2:
                    letterContainers[i].SetPotential();
                    break;
                default:
                    letterContainers[i].SetInvalid();
                    break;
            }
        }
    }

    public bool IsComplete
    {
        get
        {
            return currentLetterIndex >= letterContainers.Length;
        }
    }
}
