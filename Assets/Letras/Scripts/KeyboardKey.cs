using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeyboardKey : MonoBehaviour
{   
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI letterText;

    [Header("Events")]
    public static Action<char> onKeyPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SendKeyPressedEvent()
    {
        onKeyPressed?.Invoke(letterText.text[0]);
    }
}
