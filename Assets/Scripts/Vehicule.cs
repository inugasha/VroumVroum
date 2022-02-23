using UnityEngine;

public class Vehicule : MonoBehaviour
{
    private VehiculeMotor _motor;
    private int _currentHP;

    public delegate void HPChanged(int amount);
    public HPChanged HPChangedDelegate;

    [SerializeField] private int _maxHP = 100;

    private void Start()
    {
        _motor = GetComponent<VehiculeMotor>();
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
        //TODO il faut avoir un checkpoint par défaut sur la ligne de départ pour ne pas avoir d'erreur

        Transform checkpointTransform = GameManager.Instance.GetLastCheckpointPosition();

        transform.position = checkpointTransform.position;
        transform.rotation = checkpointTransform.rotation;

        _currentHP = _maxHP;
        HPChangedDelegate?.Invoke(_currentHP);
    }
}
