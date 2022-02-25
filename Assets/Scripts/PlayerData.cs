public class PlayerData
{
    public delegate void ValueChanged(object obj);
    public event ValueChanged BestTimeChanged;
    public event ValueChanged LastTimeChanged;
    public event ValueChanged CurrentRoundChanged;

    public PlayerData(int checkpointAmount, int deviceId, int playerIndex)
    {
        DeviceId = deviceId;
        CheckpointPassed = new bool[checkpointAmount];
        PlayerIndex = playerIndex;
    }

    public int DeviceId;
    public bool[] CheckpointPassed;
    public float BestTime;
    public float LastTime;
    public int CurrentRound;
    public int PlayerIndex;

    private float _actualTime;

    public void AddTime(float amount)
    {
        _actualTime += amount;
    }

    public void RoundFinished()
    {
        CurrentRound++;
        CurrentRoundChanged?.Invoke(CurrentRound);
        LastTime = _actualTime;
        LastTimeChanged?.Invoke(LastTime);

        if (LastTime < BestTime)
        {
            BestTime = LastTime;
            BestTimeChanged?.Invoke(BestTime);
        }

        _actualTime = 0f;

        for (int i = 0; i < CheckpointPassed.Length; i++)
        {
            CheckpointPassed[i] = false;
        }
    }

    public bool RaceFinish(int maxRound)
    {
        return CurrentRound == maxRound;
    }

}
