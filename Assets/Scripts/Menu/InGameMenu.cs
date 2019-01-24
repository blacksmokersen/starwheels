using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using Bolt;

public class InGameMenu : GlobalEventListener
{

    private enum MenuState
    {
        NONE,
        MAIN,
        OPTIONS,
    }

    private MenuState _currentMenu;

    [SerializeField] private Button quitButton;
    [SerializeField] private Button allToMenu;
    [SerializeField] private GameObject inGameMenuPanel;
    [SerializeField] private Button optionsButton;
    [SerializeField] private GameObject optionsPanel;

    [Header("Options Menu Buttons")]
    [SerializeField] private Button audioMenuButton;
    [SerializeField] private Button videoMenuButton;

    [Header("Options Menu Panels")]
    [SerializeField] private GameObject audioMenuPanel;
    [SerializeField] private GameObject videoMenuPanel;

    [Header("Audio Panel Settings")]

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider muteVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Video Panel Settings")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Dropdown resolutionPickerDropDown;
    [SerializeField] private Dropdown graphicQualityDropDown;


    private bool _isGameFullscreen = true;
    private bool _menuEnabled = false;
    private bool _optionsVisible = false;

    private void Awake()
    {
        _currentMenu = MenuState.NONE;

        quitButton.onClick.AddListener(QuitMatch);
        allToMenu.onClick.AddListener(AllToMenu);
        optionsButton.onClick.AddListener(OpenOptionsMenu);

        audioMenuButton.onClick.AddListener(OpenAudioPanel);
        videoMenuButton.onClick.AddListener(OpenVideoPanel);


        // Audio Sliders
        muteVolumeSlider.onValueChanged.AddListener(MuteSound);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);

        //Video Settings
        fullscreenToggle.onValueChanged.AddListener(SetFullScreen);
        resolutionPickerDropDown.onValueChanged.AddListener(SetScreenResolution);
        graphicQualityDropDown.onValueChanged.AddListener(SetGraphicQuality);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_currentMenu == MenuState.NONE )
            {
                Debug.Log("MAIN IN!");
                _menuEnabled = !_menuEnabled;
                inGameMenuPanel.SetActive(true);
                _currentMenu = MenuState.MAIN;
            }
            else if( _currentMenu == MenuState.MAIN)
            {
                Debug.Log("MAIN OUT!");
                _menuEnabled = !_menuEnabled;
                inGameMenuPanel.SetActive(false);
                _currentMenu = MenuState.NONE;
            }
            else if(_currentMenu == MenuState.OPTIONS)
            {
                Debug.Log("OPTIONS OUT!");
                _optionsVisible = !_optionsVisible;
                optionsPanel.SetActive(false);
                inGameMenuPanel.SetActive(true);
                _currentMenu = MenuState.MAIN;
            }
        }
    }

    private void QuitMatch()
    {
        Debug.Log("Quit Match");
        if (BoltNetwork.IsServer)
            StartCoroutine(HostDisconectLastSecurity());
        else if (BoltNetwork.IsClient)
            BoltLauncher.Shutdown();
    }

    private void AllToMenu()
    {
        Debug.Log("AllToMenu");
        var DisconnectAllPlayers = AllPlayersToMenu.Create();
        DisconnectAllPlayers.Send();
    }

    private void OpenOptionsMenu()
    {
        Debug.Log("Options Menu");
        _optionsVisible = !_optionsVisible;
        optionsPanel.SetActive(_optionsVisible);
        inGameMenuPanel.SetActive(false);
        _currentMenu = MenuState.OPTIONS;

        //par défaut on affiche l'audio panel dans les options
        audioMenuPanel.SetActive(true);

    }

    // OPTIONS FUNCTIONS

    private void OpenAudioPanel()
    {
        Debug.Log("AUDIO MENU");
        audioMenuPanel.SetActive(true);
        videoMenuPanel.SetActive(false);
    }

    private void OpenVideoPanel()
    {
        Debug.Log("VIDEO MENU");
        audioMenuPanel.SetActive(false);
        videoMenuPanel.SetActive(true);
    }

    private void SetSfxVolume(float value)
    {
        audioMixer.SetFloat("MasterSFX", value);
    }

    private void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MasterMusic", value);
    }

    private void MuteSound(float value)
    {
        Debug.Log("MUTE SOUND : " + value);

        if (value == 0)
        {
            audioMixer.SetFloat("Master", -80);
        }
        else
        {
            audioMixer.SetFloat("Master", -4);
        }
        
    }

    //


    //VIDEO OPTIONS

    private void SetFullScreen(bool value)
    {
        if(value)
        {
            Screen.fullScreen = true;
            _isGameFullscreen = true;
        }
        else
        {
            Screen.fullScreen = false;
            _isGameFullscreen = false;
        }
    }

    private void SetScreenResolution(int value)
    {
        switch (value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, _isGameFullscreen);
                break;
            case 1:
                Screen.SetResolution(1366, 768, _isGameFullscreen);
                break;
            case 2:
                Screen.SetResolution(1280, 720, _isGameFullscreen);
                break;
            default:
                break;
        }
    }

    private void SetGraphicQuality(int value)
    {
        switch (value)
        {
            case 0:
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                QualitySettings.SetQualityLevel(1);
                break;
            case 2:
                QualitySettings.SetQualityLevel(2);
                break;
            default:
                break;
        }
    }

    //

    public override void OnEvent(AllPlayersToMenu DisconnectAllPlayers)
    {
        QuitMatch();
    }

    public override void BoltShutdownBegin(AddCallback registerDoneCallback)
    {
        BoltLog.Warn("Bolt is shutting down");

        SceneManager.LoadScene("Menu");

        registerDoneCallback(() =>
        {
            BoltLog.Warn("Bolt is down");
        });
    }


    IEnumerator HostDisconectLastSecurity()
    {
        yield return new WaitForSeconds(1.5f);
        BoltLauncher.Shutdown();
    }

    /*
    public override void BoltShutdownBegin(AddCallback registerDoneCallback)
    {

        if (BoltNetwork.IsServer)
        {
            Debug.Log("1");
            SceneManager.LoadScene("Menu");
        }
        else if (BoltNetwork.IsClient)
        {
            Debug.Log("2");
            SceneManager.LoadScene("Menu");
        }

        registerDoneCallback(() =>
        {
           Debug.Log("Shutdown Done");
        });
    }
    */
}
