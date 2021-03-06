using UnityEngine;

public class BreakLight : MonoBehaviour
{
	private Light _light;
	[SerializeField] private VehiculeController _vehiculeController;

	void Start()
	{
		_light = GetComponent<Light>();
		_light.enabled = false;
	}

	void LateUpdate()
	{
		if (_vehiculeController.MoveBackward < 0.0f)
		{
			_light.enabled = true;
		}
		else
        {
			_light.enabled = false;
		}
	}
}
