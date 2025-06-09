using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Image soundsImage;
    [SerializeField] private Image hapticsImage;

    private bool soundsState;
    private bool hapticsState;

    void Start()
    {
        LoadStates();
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
            SoundsManager.instance.EnableSounds();
            soundsImage.color = Color.white;
        }
        else
        {
            SoundsManager.instance.DisableSounds();
            soundsImage.color = Color.gray;
        }
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
            EnableHaptics();
        else
            DisableHaptics();
    }

    private void EnableHaptics()
    {
        HapticsManager.instance.EnableHaptics();
        HapticsManager.Vibrate();
        hapticsImage.color = Color.white;
    }

    private void DisableHaptics()
    {
        HapticsManager.instance.DisableHaptics();
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
