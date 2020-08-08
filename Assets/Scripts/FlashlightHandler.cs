using UnityEngine;

public class FlashlightHandler : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minAngle = 40f;

    Light light;

    void Start() => light = GetComponent<Light>();

    void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }    

    void DecreaseLightAngle()
    {
        if(light.spotAngle <= minAngle) { return; }

        light.spotAngle -= angleDecay * Time.deltaTime;
    }

    void DecreaseLightIntensity() => light.intensity -= lightDecay * Time.deltaTime;

    public void RestoreLightAngle(float restoreAngle) => light.spotAngle = restoreAngle;

    public void RestoreLightIntensity(float intensityAmount) => light.intensity = intensityAmount;
}