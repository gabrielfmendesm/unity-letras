using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

enum Validity { None, Valid, Potential, Invalid }

public class KeyboardKey : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image keyImage;
    [SerializeField] private TextMeshProUGUI letterText;

    [Header("Settings")]
    private Validity validity;

    [Header("Events")]
    public static Action<char> onKeyPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SendKeyPressedEvent()
    {
        onKeyPressed?.Invoke(letterText.text[0]);
    }

    public char GetLetter()
    {
        return letterText.text[0];
    }

    public void Initialize()
    {
        keyImage.color = Color.white;
        validity = Validity.None;
    }

    public void SetValid()
    {
        keyImage.color = Color.green;
        validity = Validity.Valid;
    }

    public void SetPotential()
    {
        if (validity == Validity.Valid)
        {
            return;
        }

        keyImage.color = Color.yellow;
        validity = Validity.Potential;
    }

    public void SetInvalid()
    {
        if (validity == Validity.Valid || validity == Validity.Potential)
        {
            return;
        }

        keyImage.color = Color.gray;
        validity = Validity.Invalid;
    }
    
    public bool IsUntouched()
    {
        return validity == Validity.None;
    }
}
