using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    private const string LOBBY_LIST_SCENE = "LobbyList";

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _lobbyListButton;
    [SerializeField] private Button _settingsButton;

    public void OnClick_PlayButton()
    {
        
    }

    public void OnClick_LobbyListButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClick_SettingsButton()
    {
            
    }
}
