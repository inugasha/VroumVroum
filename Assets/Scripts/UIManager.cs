using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMPro.TMP_Text _BigText;
    [SerializeField] private GameObject _playerUIPrefab;
    [SerializeField] private Transform _playerUIParent;

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

    public void Setup(List<Vehicule> vehicules)
    {
        foreach (Vehicule vehicule in vehicules)
        {
            GameObject uiInstance = Instantiate(_playerUIPrefab, _playerUIParent);
            uiInstance.GetComponent<PlayerUi>().Setup(vehicule);
        }
    }

    public void DisplayTextInFront(string value, float time = 0f)
    {
        _BigText.text = value;

        if (time > 0f)
        {
            StopAllCoroutines();
            StartCoroutine(HideText(time, _BigText));
        }
    }

    private IEnumerator HideText(float time, TMPro.TMP_Text txt)
    {
        yield return new WaitForSeconds(time);
        txt.text = string.Empty;
    }
}
