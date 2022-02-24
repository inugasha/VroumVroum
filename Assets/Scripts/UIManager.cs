using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _HPText;

    private void Start()
    {
        //TODO
        //FindObjectOfType<Vehicule>().HPChangedDelegate += HPChanged;
    }

    public void Setup()
    {

    }

    private void HPChanged(int amount)
    {
        _HPText.text = $"{amount} HP";
    }
}
