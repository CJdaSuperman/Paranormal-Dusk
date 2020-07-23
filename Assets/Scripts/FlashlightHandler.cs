using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightHandler : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minAngle = 40f;

    Light light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
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

    void DecreaseLightIntensity()
    {
        light.intensity -= lightDecay * Time.deltaTime;
    }

    public void RestoreLightAngle(float restoreAngle)
    {
        light.spotAngle = 70f;
    }

    public void RestoreLightIntensity(float intensityAmount)
    {
        light.intensity = intensityAmount;
    }
}
