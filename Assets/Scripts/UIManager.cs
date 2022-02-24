using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMPro.TMP_Text _HPText;
    [SerializeField] private TMPro.TMP_Text _BigText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of UIManager already exist");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

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


    public void DisplayTextInFront(string value,float time = 0f)
    {
        _BigText.text = value;

        if(time > 0f)
        {
            StartCoroutine(HideText(time, _BigText));
        }
    }

    private IEnumerator HideText(float time, TMPro.TMP_Text txt)
    {
        yield return new WaitForSeconds(time);
        txt.text = string.Empty;
    }
}
