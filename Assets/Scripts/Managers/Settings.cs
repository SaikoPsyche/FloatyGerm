using UnityEngine;
using TMPro;

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

    private void Update()
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
        uiSounds[0].volume = UISoundValue;
        uiSounds[1].volume = UISoundValue;
        uiSounds[2].volume = UISoundValue;
        
        //float toWholeNumber = 100;

        // float BGFullValue = BGMusicValue * toWholeNumber;
        BGMusicValueText.text = BGMusicValue.ToString("000");

        // float UISoundFullValue = UISoundValue * toWholeNumber;
        UISoundValueText.text = UISoundValue.ToString("000");
    }

    public void BGVolumeUp()
    {
        BGMusicValue += increment;

        if (BGMusicValue >= 1)
        {
            BGMusicValue = 1;
        }
        
    }

    public void BGVolumeDown()
    {
        BGMusicValue -= increment;

        if (BGMusicValue <= 0)
        {
            bgMusic.mute = true;
        }
    }

    public void UISoundVolumeUp()
    {
        UISoundValue += increment;
        
        if (UISoundValue >= 1)
        {
            UISoundValue = 1;
        }
    }

    public void UISoundVolumeDown()
    {
        UISoundValue -= increment;

        if (UISoundValue <= 0)
        {
            uiSounds[0].mute = true;
            uiSounds[1].mute = true;
            uiSounds[2].mute = true;
        }
    }
}
