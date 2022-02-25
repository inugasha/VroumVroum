using UnityEngine;

public class Vehicule : MonoBehaviour
{
    private VehiculeMotor _motor;
    private VehiculeController _controller;
    private int _currentHP;

    public delegate void HPChanged(int amount);
    public HPChanged HPChangedDelegate;
    public PlayerData Data;

    [SerializeField] private int _maxHP = 100;

    public int MaxHP
    {
        get { return _maxHP; }
        private set { _maxHP = value; }
    }

    private void Start()
    {
        _motor = GetComponent<VehiculeMotor>();
        _controller = GetComponent<VehiculeController>();
        _currentHP = _maxHP;
        HPChangedDelegate?.Invoke(_currentHP);
    }

    public void TakeDamage(int amount)
    {
        _currentHP = Mathf.Clamp(_currentHP - amount, 0, _maxHP);

        HPChangedDelegate?.Invoke(_currentHP);

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Respawn();
    }

    public void Respawn()
    {
        _motor.InstantStopVehicule();

        Transform checkpointTransform = GameManager.Instance.GetLastCheckpointPosition(_controller.DeviceId);

        transform.position = checkpointTransform.position;
        transform.rotation = checkpointTransform.rotation;

        _currentHP = _maxHP;
        HPChangedDelegate?.Invoke(_currentHP);
    }
}
