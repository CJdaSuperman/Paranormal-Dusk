using UnityEngine;

public class BatteryPickupHandler : MonoBehaviour
{
    [SerializeField] float lightAngle = 70f;
    [SerializeField] float lightIntensity = 10f;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<FlashlightHandler>().RestoreLightAngle(lightAngle);
            FindObjectOfType<FlashlightHandler>().RestoreLightIntensity(lightIntensity);

            Destroy(gameObject);
        }
    }
}