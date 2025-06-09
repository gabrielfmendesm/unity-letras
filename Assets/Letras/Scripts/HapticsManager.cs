using UnityEngine;

public class HapticsManager : MonoBehaviour
{
    public static HapticsManager instance;
    private bool haptics = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Taptic.tapticOn = haptics;
    }

    public void EnableHaptics()
    {
        haptics = true;
        Taptic.tapticOn = true;
    }

    public void DisableHaptics()
    {
        haptics = false;
        Taptic.tapticOn = false;
    }

    public bool HapticsEnabled() => haptics;

    public static void Vibrate()
    {
        if (instance.HapticsEnabled())
            Taptic.Vibrate();
    }
}
