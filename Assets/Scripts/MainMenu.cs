using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _lobbyMenu;
    [SerializeField] private string _gameSceneName;
    [SerializeField] private GameObject _utilPrefab;
    [SerializeField] private Button _playButton;

    private List<int> _gamepadIds = new List<int>();
    private List<Gamepad> _gamepads = new List<Gamepad>();

    public void OpenLobbyMenu()
    {
        _playButton.interactable = false;
        _lobbyMenu.SetActive(true);
    }

    public void BackInMainMenu()
    {
        _lobbyMenu.SetActive(false);

        _gamepadIds.Clear();
        _gamepads.Clear();
    }

    public void StartGame()
    {
        GameObject instance = Instantiate(_utilPrefab);

        DontDestroyOnLoad(instance);

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(_gameSceneName);
        instance.GetComponent<Utils>().SetupGame(asyncOp, _gamepads);
    }

    private void OnAddPlayer()
    {
        if (!_lobbyMenu.activeInHierarchy) return;

        foreach (var gamepad in Gamepad.all.ToList())
        {
            if (_gamepadIds.Contains(gamepad.deviceId)) continue;

            if (gamepad.buttonEast.IsPressed(1f))
            {
                _gamepadIds.Add(gamepad.deviceId);
                _gamepads.Add(gamepad);
            }
        }

        if (_gamepads.Count > 0)
        {
            _playButton.interactable = true;
        }

        Debug.Log(_gamepads.Count);
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
