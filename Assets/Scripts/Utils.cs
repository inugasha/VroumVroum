using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Utils : MonoBehaviour
{
    private AsyncOperation _asyncOp;
    private List<Gamepad> _gamepads;
    private int _maxRound;

    public void SetupGame(AsyncOperation asyncOp, List<Gamepad> gamepads,int maxRound)
    {
        _asyncOp = asyncOp;
        _gamepads = gamepads;
        _maxRound = maxRound;

        StartCoroutine(SetupGameRoutine());
    }

    private IEnumerator SetupGameRoutine()
    {
        while (!_asyncOp.isDone)
        {
            yield return new WaitForSeconds(.1f);
        }

        if (_asyncOp.isDone)
        {
            GameManager.Instance.StartGame(_gamepads, _maxRound);
            Destroy(gameObject);
        }
    }
}
