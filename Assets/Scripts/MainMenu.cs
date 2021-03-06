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
    [SerializeField] private GameObject _controlMenu;
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _playerInfoUIPrefab;
    [SerializeField] private GameObject _playersUI;
    [SerializeField] private TMPro.TextMeshProUGUI _roundText;

    private List<int> _gamepadIds = new List<int>();
    private List<Gamepad> _gamepads = new List<Gamepad>();
    private List<GameObject> _playerUis = new List<GameObject>();
    private int _currentRound;

    [SerializeField] private int _minRound;
    [SerializeField] private int _maxRound;

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

        _playerUis.ForEach(x => Destroy(x));
    }

    public void HideShowControlMenu()
    {
        _controlMenu.SetActive(!_controlMenu.activeInHierarchy);
    }

    public void StartGame()
    {
        GameObject instance = Instantiate(_utilPrefab);

        DontDestroyOnLoad(instance);

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(_gameSceneName);
        instance.GetComponent<Utils>().SetupGame(asyncOp, _gamepads, _currentRound);
    }

    public void UpdateMaxRound(int value)
    {
        _currentRound = Mathf.Clamp(_currentRound += value, _minRound, _maxRound);

        _roundText.text = $"Round : {_currentRound}";
    }



    private void OnAddPlayer()
    {
        if (!_lobbyMenu.activeInHierarchy) return;

        foreach (Gamepad gamepad in Gamepad.all.ToList())
        {
            if (_gamepadIds.Contains(gamepad.deviceId)) continue;

            if (gamepad.buttonEast.IsPressed(1f))
            {
                AddPlayer(gamepad);
            }
        }

        if (_gamepads.Count > 0)
        {
            _playButton.interactable = true;
        }
    }

    private void AddPlayer(Gamepad gamepad)
    {
        _gamepadIds.Add(gamepad.deviceId);
        _gamepads.Add(gamepad);

        GameObject instanceUI = Instantiate(_playerInfoUIPrefab, _playersUI.transform);
        instanceUI.GetComponent<PlayerInfoUI>().Setup(_gamepads.Count);

        _playerUis.Add(instanceUI);
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
