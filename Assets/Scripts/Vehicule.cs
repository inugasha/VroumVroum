using UnityEngine;

public class Vehicule : MonoBehaviour
{
    private VehiculeMotor _motor;


    public int CurrentHP = 100;
    public int MaxHP;

    private void Start()
    {
        _motor = GetComponent<VehiculeMotor>();
        CurrentHP = MaxHP;
    }

    public void TakeDamage(int amount)
    {
        CurrentHP = Mathf.Clamp(CurrentHP - amount, 0, MaxHP);

        if (CurrentHP <= 0)
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

        CurrentHP = MaxHP;
    }
}
