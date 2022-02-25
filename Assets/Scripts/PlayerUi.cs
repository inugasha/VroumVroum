using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    private int _maxHP;
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;
    [SerializeField] private TMPro.TextMeshProUGUI _roundText;

    public void Setup(Vehicule vehicule)
    {
        vehicule.HPChangedDelegate += UpdateHP;
        vehicule.Data.CurrentRoundChanged += UpdateCurrentRound;

        _maxHP = vehicule.MaxHP;
        _slider.maxValue = _maxHP;
        UpdateHP(vehicule.MaxHP);
        UpdateCurrentRound(1);
    }

    private void UpdateHP(int amount)
    {
        _slider.value = amount;
        _fill.color = _gradient.Evaluate((float)amount/(float)_maxHP);
    }

    private void UpdateCurrentRound(object value)
    {
        _roundText.text = $"{(int)value} / {GameManager.Instance.MaxRound}";
    }
}
