using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float fuel = 3000f;
    [SerializeField] private float _maxFuel = 3000f;
    [SerializeField] private float _fuelConsumption = 1f;
    private ShipMovement shipMovement;

    void Start()
    {
        shipMovement = GetComponent<ShipMovement>();
    }
    void Update()
    {
        if(shipMovement.moveValue.magnitude != 0)
        {
            fuel -= _fuelConsumption * (shipMovement.boosting ? shipMovement.boostValue : 1) * Time.deltaTime;
        }
    }
}
