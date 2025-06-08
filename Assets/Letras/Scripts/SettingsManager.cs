using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image soundsImage;
    [SerializeField] private Image hapticsImage;

    [Header("Settings")]
    private bool soundsState;
    private bool hapticsState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadStates();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SoundsButtonCallback()
    {
        soundsState = !soundsState;
        UpdateSoundsState();
        SaveStates();
    }

    private void UpdateSoundsState()
    {
        if (soundsState)
        {
            EnableSounds();
        }
        else
        {
            DisableSounds();
        }
    }

    private void EnableSounds()
    {
        soundsImage.color = Color.white;
    }

    private void DisableSounds()
    {
        soundsImage.color = Color.gray;
    }

    public void HapticsButtonCallback()
    {
        hapticsState = !hapticsState;
        UpdateHapticsState();
        SaveStates();
    }

    private void UpdateHapticsState()
    {
        if (hapticsState)
        {
            EnableHaptics();
        }
        else
        {
            DisableHaptics();
        }
    }

    private void EnableHaptics()
    {
        hapticsImage.color = Color.white;
    }

    private void DisableHaptics()
    {
        hapticsImage.color = Color.gray;
    }

    private void LoadStates()
    {
        soundsState = PlayerPrefs.GetInt("Sounds", 1) == 1;
        hapticsState = PlayerPrefs.GetInt("Haptics", 1) == 1;

        UpdateSoundsState();
        UpdateHapticsState();
    }
    
    private void SaveStates()
    {
        PlayerPrefs.SetInt("Sounds", soundsState ? 1 : 0);
        PlayerPrefs.SetInt("Haptics", hapticsState ? 1 : 0);
        PlayerPrefs.Save();
    }
}
