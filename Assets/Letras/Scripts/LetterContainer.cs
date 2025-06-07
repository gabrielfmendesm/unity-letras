using TMPro;
using UnityEngine;

public class LetterContainer : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private SpriteRenderer letterCointainer;
    [SerializeField] private TextMeshPro letter;

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
        letter.text = string.Empty;
        letterCointainer.color = Color.white;
    }

    public void SetLetter(char letter)
    {
        this.letter.text = letter.ToString();
    }

    public void SetValid()
    {
        letterCointainer.color = Color.green;
    }

    public void SetPotential()
    {
        letterCointainer.color = Color.yellow;
    }

    public void SetInvalid()
    {
        letterCointainer.color = Color.gray;
    }

    public char GetLetter()
    {
        return letter.text[0];
    }
}
