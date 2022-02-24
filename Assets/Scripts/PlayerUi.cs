using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vehicule t = FindObjectOfType<Vehicule>();
        t.HPChangedDelegate += UpdateHP;
    }

    private void UpdateHP(int amount)
    {
        //slider.value = amount;
    }
}
