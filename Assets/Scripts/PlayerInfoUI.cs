using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _playerName;

    public void Setup(int playerNumber)
    {
        _playerName.text = $"Player {playerNumber}";
    }

}
