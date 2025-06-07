using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{
    [Header("Elements")]
    private KeyboardKey[] keys;
    private Dictionary<char, int> keyStatus;
    private Dictionary<char, KeyboardKey> keyLookup;

    private void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
        keyStatus = new Dictionary<char, int>();
        keyLookup = new Dictionary<char, KeyboardKey>();
        foreach (var key in keys)
        {
            char c = key.GetLetter();
            keyStatus[c] = 4;
            keyLookup[c] = key;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Colorize(string secretWord, string wordToCheck)
    {
        int n = wordToCheck.Length;
        var guess = wordToCheck.ToCharArray();
        var counts = new Dictionary<char, int>();
        foreach (char c in secretWord)
            counts[c] = counts.ContainsKey(c) ? counts[c] + 1 : 1;

        var status = new int[n];

        for (int i = 0; i < n; i++)
            if (guess[i] == secretWord[i])
            {
                status[i] = 1;
                counts[guess[i]]--;
            }

        for (int i = 0; i < n; i++)
        {
            if (status[i] != 0) continue;
            char c = guess[i];
            if (counts.ContainsKey(c) && counts[c] > 0)
            {
                status[i] = 2;
                counts[c]--;
            }
            else
            {
                status[i] = 3;
            }
        }

        UpdateKeyboard(guess, status);
    }

    private void UpdateKeyboard(char[] guess, int[] status)
    {
        for (int i = 0; i < guess.Length; i++)
        {
            char c = guess[i];
            int newStat = status[i];
            int oldStat = keyStatus[c];

            if (newStat < oldStat)
            {
                keyStatus[c] = newStat;
                var key = keyLookup[c];
                if (newStat == 1)
                    key.SetValid();
                else if (newStat == 2)
                    key.SetPotential();
                else
                    key.SetInvalid();
            }
        }
    }
}
