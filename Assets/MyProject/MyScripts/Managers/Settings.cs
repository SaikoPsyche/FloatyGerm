using UnityEngine;
using TMPro;

// Updated 2026-09-14 by Claude (Anthropic AI assistant):
// - Fixed volume percentage text: BGMusicValue/UISoundValue are 0-1
//   floats, but ToString("000") formats whole numbers, so the display was
//   effectively stuck at "000"/"001". Now multiplies by 100 first, which
//   is what the commented-out code above it was already trying to do.
// - BGVolumeUp()/UISoundVolumeUp() now un-mute, matching the *Down()
//   methods that mute - previously, once muted, sound never came back.
// - BGMusicValue/UISoundValue are now clamped to 0 at the bottom (they
//   could previously go negative, which also fed into the broken text
//   above).
// - DisplayVolumeValue() is no longer called every frame in Update(); it
//   now runs once on Start() and after each volume button press instead.
// - uiSounds[0]/[1]/[2] hardcoded indexing replaced with a foreach loop
//   over the array.
public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private TextMeshProUGUI BGMusicValueText;
    [SerializeField] private TextMeshProUGUI UISoundValueText;

    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource[] uiSounds;

    [SerializeField] private float BGMusicValue = 1;
    [SerializeField] private float UISoundValue = 1;

    [SerializeField] private float increment = 0.1f;

    private void Start()
    {
        DisplayVolumeValue();
    }

    public void ShowSettingsScreen()
    {
        settingsScreen.SetActive(true);
    }

    public void DisplayVolumeValue()
    {
        bgMusic.volume = BGMusicValue;
        foreach (AudioSource uiSound in uiSounds)
        {
            uiSound.volume = UISoundValue;
        }

        const float toWholeNumber = 100f;
        BGMusicValueText.text = (BGMusicValue * toWholeNumber).ToString("000");
        UISoundValueText.text = (UISoundValue * toWholeNumber).ToString("000");
    }

    public void BGVolumeUp()
    {
        BGMusicValue += increment;

        if (BGMusicValue >= 1)
        {
            BGMusicValue = 1;
        }

        bgMusic.mute = false;
        DisplayVolumeValue();
    }

    public void BGVolumeDown()
    {
        BGMusicValue -= increment;

        if (BGMusicValue <= 0)
        {
            BGMusicValue = 0;
            bgMusic.mute = true;
        }

        DisplayVolumeValue();
    }

    public void UISoundVolumeUp()
    {
        UISoundValue += increment;

        if (UISoundValue >= 1)
        {
            UISoundValue = 1;
        }

        foreach (AudioSource uiSound in uiSounds)
        {
            uiSound.mute = false;
        }

        DisplayVolumeValue();
    }

    public void UISoundVolumeDown()
    {
        UISoundValue -= increment;

        if (UISoundValue <= 0)
        {
            UISoundValue = 0;
            foreach (AudioSource uiSound in uiSounds)
            {
                uiSound.mute = true;
            }
        }

        DisplayVolumeValue();
    }
}
