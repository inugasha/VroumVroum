using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Checkpoint[] _checkpoints;
    [SerializeField] private GameObject _vehiculePrefab;
    [SerializeField] private Transform _lineStart;

    private float _bestTime;
    private float _actualTime;
    private float _lastTime;
    private bool[] _checkpointPass;
    private bool _gameStart;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of GameManager already exist");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        _checkpointPass = new bool[_checkpoints.Length];
        _checkpoints = _checkpoints.OrderBy(x => x.CheckpointNumber).ToArray();
    }

    void Update()
    {
        if (!_gameStart) return;
        _actualTime += Time.deltaTime;
    }

    public void CheckpointPass(int number)
    {
        bool lastCheckpointPass = true;

        for (int i = 0; i < _checkpoints.Length; i++)
        {
            if (_checkpoints[i].CheckpointNumber < number)
            {
                if (!_checkpointPass[i])
                {
                    lastCheckpointPass = false;
                }
            }
        }

        if (!lastCheckpointPass) return;

        _checkpointPass[number] = true;
        CheckRoundIsFinish(number);
    }

    private void CheckRoundIsFinish(int number)
    {
        bool isLastCheckpoint = false;

        if (number == _checkpointPass.Length)
        {
            if (_checkpointPass.Count(x => !x) <= 0)
            {
                isLastCheckpoint = true;
            }
        }

        if (isLastCheckpoint)
        {
            SetTimer();

            for (int i = 0; i < _checkpointPass.Length; i++)
            {
                _checkpointPass[i] = false;
            }
        }

    }

    private void SetTimer()
    {
        _lastTime = _actualTime;
        _actualTime = 0f;

        if (_lastTime < _bestTime)
        {
            _bestTime = _lastTime;
        }
    }

    public void StartGame(List<Gamepad> gamepads)
    {
        foreach (var gamepad in gamepads)
        {
            GameObject instance = Instantiate(_vehiculePrefab);
            instance.GetComponent<VehiculeController>().SetDeviceId(gamepad.deviceId);
        }

        _gameStart = true;
    }

    public Transform GetLastCheckpointPosition()
    {
        int index = -1;

        for (int i = 0; i < _checkpointPass.Length; i++)
        {
            if (_checkpointPass[i])
            {
                index = i;
            }
        }

        if (index < 0)
        {
            return _lineStart.transform;
        }

        return _checkpoints[index].gameObject.transform;
    }

}
