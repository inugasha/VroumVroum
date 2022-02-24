using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool GameStart;
    public int MaxRound;

    [SerializeField] private Checkpoint[] _checkpoints;
    [SerializeField] private GameObject _vehiculePrefab;
    [SerializeField] private Transform _lineStart;
    [SerializeField] private Transform[] _spawnPos;

    private List<PlayerData> _playerDatas;

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
        _checkpoints = _checkpoints.OrderBy(x => x.CheckpointNumber).ToArray();
    }

    void Update()
    {
        if (!GameStart) return;
        _playerDatas.ForEach(x => x.AddTime(Time.deltaTime));
    }

    public void CheckpointPass(int number, int deviceId)
    {
        bool lastCheckpointPass = true;

        PlayerData data = GetPlayerData(deviceId);

        for (int i = 0; i < data.CheckpointPassed.Length; i++)
        {
            if (_checkpoints[i].CheckpointNumber < number)
            {
                if (!data.CheckpointPassed[i])
                {
                    lastCheckpointPass = false;
                }
            }
        }

        if (!lastCheckpointPass) return;

        data.CheckpointPassed[number] = true;
        CheckRoundIsFinish(number, deviceId);
    }

    private void CheckRoundIsFinish(int number, int deviceId)
    {
        bool isLastCheckpoint = false;

        PlayerData data = GetPlayerData(deviceId);

        if (number == data.CheckpointPassed.Length)
        {
            if (data.CheckpointPassed.Count(x => !x) <= 0)
            {
                isLastCheckpoint = true;
            }
        }

        if (isLastCheckpoint)
        {
            RoundFinished(data);
        }

    }

    private void RoundFinished(PlayerData data)
    {
        data.RoundFinished();
        if (data.RaceFinish(MaxRound))
        {
            Debug.Log($"Player {data.DeviceId} as finish");
        }
    }

    public void StartGame(List<Gamepad> gamepads, int maxRound)
    {
        int index = 0;
        MaxRound = maxRound;
        _playerDatas = new List<PlayerData>();

        List<Vehicule> vehicules = new List<Vehicule>();

        foreach (var gamepad in gamepads)
        {
            GameObject instance = Instantiate(_vehiculePrefab, _spawnPos[index].transform.position, Quaternion.identity);
            instance.GetComponent<VehiculeController>().SetDeviceId(gamepad.deviceId);
            vehicules.Add(instance.GetComponent<Vehicule>());

            PlayerData data = new PlayerData(_checkpoints.Length, gamepad.deviceId);
            _playerDatas.Add(data);
            index++;
        }

        UIManager.Instance.Setup(vehicules);

        StartCoroutine(WaitingTimeBeforeStart());
    }

    private IEnumerator WaitingTimeBeforeStart()
    {
        int remainingTime = 3;

        while (remainingTime > 0)
        {
            UIManager.Instance.DisplayTextInFront(remainingTime.ToString());
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        UIManager.Instance.DisplayTextInFront("START!", 1f);
        GameStart = true;
    }
    private PlayerData GetPlayerData(int deviceId)
    {
        return _playerDatas.FirstOrDefault(x => x.DeviceId == deviceId);
    }

    public Transform GetLastCheckpointPosition(int deviceId)
    {
        int index = -1;
        PlayerData data = GetPlayerData(deviceId);

        for (int i = 0; i < data.CheckpointPassed.Length; i++)
        {
            if (data.CheckpointPassed[i])
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
