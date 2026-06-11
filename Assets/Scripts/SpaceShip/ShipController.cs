using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public float fuel = 3000f;
    [SerializeField] private float _maxFuel = 3000f;
    [SerializeField] private float _fuelConsumption = 1f;
    private ShipMovement shipMovement;
    private Slider slider;

    void Start()
    {
        shipMovement = GetComponent<ShipMovement>();
        slider = GameObject.Find("FuelSlider").GetComponent<Slider>();
    }
    void Update()
    {
        if (shipMovement.moveValue.magnitude != 0)
        {
            fuel -= _fuelConsumption * (shipMovement.boosting ? shipMovement.boostValue : 1) * Time.deltaTime;
        }
        slider.value = fuel / _maxFuel;
    }
}
