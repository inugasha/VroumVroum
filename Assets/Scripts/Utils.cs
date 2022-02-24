using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Utils : MonoBehaviour
{
    private AsyncOperation _asyncOp;
    private List<Gamepad> _gamepads;

    public void SetupGame(AsyncOperation asyncOp, List<Gamepad> gamepads)
    {
        _asyncOp = asyncOp;
        _gamepads = gamepads;

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
            GameManager.Instance.StartGame(_gamepads);
            Destroy(gameObject);
        }
    }
}
