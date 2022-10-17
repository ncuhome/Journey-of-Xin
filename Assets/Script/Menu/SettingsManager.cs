using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    #region Properties
    public static SettingsManager Instance {get; private set;}
    //音量滑动条
    private Slider volumeSlider;
    private TMP_Text volumeNumber;

    #endregion

    #region Unity Methods

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        volumeNumber = GameObject.Find("VolumeNumber").GetComponent<TMP_Text>(); 
    }
    
    void Start()
    {
        //设置声音
        if (PlayerPrefs.GetString("IsOnVolume") == "") {
            PlayerPrefs.SetString("IsOnVolume", "true");
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("IsOnVolume") == "true") {
            AudioListener.volume = PlayerPrefs.GetFloat("IsOnVolume");
        }
        if (PlayerPrefs.GetString("IsOnVolume") == "false") {
            AudioListener.volume = 0f;
        }
    }

    void Update()
    {
        volumeNumber.text = ( (int) (volumeSlider.value * 100)).ToString();
    }

    private void OnEnable() 
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeValueChanged);
        string isOnVolume = PlayerPrefs.GetString("IsOnVolume");
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        PlayerPrefs.SetString("IsOnVolume", isOnVolume);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetString("IsOnVolume") == "true") {
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        }
        if (PlayerPrefs.GetString("IsOnVolume") == "false") {
            AudioListener.volume = 0f;
        }
    }

    #endregion

    #region Volume

    private void OnVolumeValueChanged(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        AudioListener.volume = value;
        PlayerPrefs.SetString("IsOnVolume", "true");
        PlayerPrefs.Save();
    }

    #endregion
}
