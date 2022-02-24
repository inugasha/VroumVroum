using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public PlayerData(int checkpointAmount, int deviceId)
    {
        DeviceId = deviceId;
        CheckpointPassed = new bool[checkpointAmount];
    }

    public int DeviceId;
    public bool[] CheckpointPassed;
    public float BestTime;
    public float LastTime;
    public int CurrentRound;
    private float _actualTime;


    public void AddTime(float amount)
    {
        _actualTime += amount;
    }

    public void RoundFinished()
    {
        CurrentRound++;
        LastTime = _actualTime;

        if(LastTime < BestTime)
        {
            BestTime = LastTime;
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
