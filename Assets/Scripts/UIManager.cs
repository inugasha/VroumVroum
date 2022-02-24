using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _HPText;

    private void Start()
    {
        //FindObjectOfType<Vehicule>().HPChangedDelegate += HPChanged;
    }

    private void HPChanged(int amount)
    {
        _HPText.text = $"{amount} HP";
    }
}
