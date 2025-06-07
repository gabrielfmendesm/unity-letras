using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{
    [Header("Elements")]
    private KeyboardKey[] keys;

    private void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
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
        for (int i = 0; i < keys.Length; i++)
        {
            char keyLetter = keys[i].GetLetter();

            for (int j = 0; j < wordToCheck.Length; j++)
            {
                if (keyLetter != wordToCheck[j])
                {
                    continue;
                }
                if (keyLetter == secretWord[j])
                {
                    keys[i].SetValid();
                }
                else if (secretWord.Contains(keyLetter))
                {
                    keys[i].SetPotential();
                }
                else
                {
                    keys[i].SetInvalid();
                }
            }
        }
    }
}
