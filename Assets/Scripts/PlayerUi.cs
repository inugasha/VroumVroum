using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    private int _maxHP;
    private Slider _slider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponentInChildren<Slider>();
    }


    public void Setup(Vehicule vehicule)
    {
        vehicule.HPChangedDelegate += UpdateHP;

        _maxHP = vehicule.MaxHP;
        _slider.maxValue = _maxHP;
        UpdateHP(vehicule.MaxHP);
    }

    private void UpdateHP(int amount)
    {
        _slider.value = amount;
        _fill.color = _gradient.Evaluate(amount/_maxHP);
    }
}
