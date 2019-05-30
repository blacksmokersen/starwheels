using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using Bolt;
using TMPro;

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
    [SerializeField] private Button hudMenuButton;
    [SerializeField] private Button keyBindingMenuButton;
    [SerializeField] private Button backMenuButton;

    [Header("Options Menu Panels")]
    [SerializeField] private GameObject audioMenuPanel;
    [SerializeField] private GameObject videoMenuPanel;
    [SerializeField] private GameObject hudMenuPanel;
    [SerializeField] private GameObject keyBindMenuPanel;

    [Header("Audio Panel Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider muteVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Video Panel Settings")]
    [SerializeField] private Slider fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionPickerDropDown;
    [SerializeField] private TMP_Dropdown graphicQualityDropDown;

    [Header("HUD Panel Settings")]
    [SerializeField] private TMP_Dropdown hudPositionDropDown;
    [SerializeField] private Slider hudSizeSlider;

    private bool _isGameFullscreen = true;
    private bool _menuEnabled = false;
    private bool _optionsVisible = false;

    private void Awake()
    {

        _currentMenu = MenuState.NONE;

        quitButton.onClick.AddListener(QuitMatch);
        allToMenu.onClick.AddListener(AllToMenu);
        optionsButton.onClick.AddListener(OpenOptionsMenu);


        backMenuButton.onClick.AddListener(CloseOptionsMenu);
        audioMenuButton.onClick.AddListener(OpenAudioPanel);
        videoMenuButton.onClick.AddListener(OpenVideoPanel);
        hudMenuButton.onClick.AddListener(OpenHudPanel);
        keyBindingMenuButton.onClick.AddListener(OpenKeyBindPanel);


        // Audio Settings
        muteVolumeSlider.onValueChanged.AddListener(MuteSound);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);

        //Video Settings
        fullscreenToggle.onValueChanged.AddListener(SetFullScreen);
        resolutionPickerDropDown.onValueChanged.AddListener(SetScreenResolution);
        graphicQualityDropDown.onValueChanged.AddListener(SetGraphicQuality);

        //HUD Settings


        //Keybind Settings




    }

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Input.EscapeMenu))
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
                CloseOptionsMenu();
            }
        }
    }

    private void QuitMatch()
    {
        Debug.Log("Quit Match");
        if (BoltNetwork.IsServer)
            StartCoroutine(HostDisconectLastSecurity());
        else if (BoltNetwork.IsClient)
        {
            BoltLauncher.Shutdown();
        }
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
        inGameMenuPanel.SetActive(false);
        optionsPanel.SetActive(_optionsVisible);
        _currentMenu = MenuState.OPTIONS;

        //par défaut on affiche l'audio panel dans les options
        audioMenuPanel.SetActive(true);

    }

    private void CloseOptionsMenu()
    {
        Debug.Log("OPTIONS OUT!");
        _optionsVisible = !_optionsVisible;
        optionsPanel.SetActive(false);
        inGameMenuPanel.SetActive(true);
        _currentMenu = MenuState.MAIN;
    }


    // OPTIONS FUNCTIONS

    private void OpenAudioPanel()
    {
        Debug.Log("AUDIO MENU");
        audioMenuPanel.SetActive(true);
        videoMenuPanel.SetActive(false);
        hudMenuPanel.SetActive(false);
        keyBindMenuPanel.SetActive(false);

    }

    private void OpenVideoPanel()
    {
        Debug.Log("VIDEO MENU");
        audioMenuPanel.SetActive(false);
        videoMenuPanel.SetActive(true);
        hudMenuPanel.SetActive(false);
        keyBindMenuPanel.SetActive(false);
    }

    private void OpenHudPanel()
    {
        Debug.Log("HUD MENU");
        audioMenuPanel.SetActive(false);
        videoMenuPanel.SetActive(false);
        hudMenuPanel.SetActive(true);
        keyBindMenuPanel.SetActive(false);
    }

    private void OpenKeyBindPanel()
    {
        Debug.Log("KEYBIND MENU");
        audioMenuPanel.SetActive(false);
        videoMenuPanel.SetActive(false);
        hudMenuPanel.SetActive(false);
        keyBindMenuPanel.SetActive(true);
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

    private void SetFullScreen(float value)
    {
        if(value == 1)
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
}
