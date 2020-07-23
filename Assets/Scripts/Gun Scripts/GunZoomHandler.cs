using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GunZoomHandler : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [Tooltip("Field of View in Aim Down Sight")] [SerializeField] float adsFOV = 38f;
    [SerializeField] float adsMouseSensitivity = .6f;
    [Tooltip("Default Field of View")] [SerializeField] float normalFOV = 60f;
    [SerializeField] float normalMouseSensitivity = 1.3f;

    RigidbodyFirstPersonController fpsController;

    bool isADS = false;

    void Start()
    {
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(!isADS)
            {
                AimDownSight();
            }
            else
            {
                NormalView();
            }
        }
    }

    void NormalView()
    {
        isADS = false;

        fpsCamera.fieldOfView = normalFOV;

        fpsController.mouseLook.XSensitivity = normalMouseSensitivity;
        fpsController.mouseLook.YSensitivity = normalMouseSensitivity;
    }

    void AimDownSight()
    {
        isADS = true;

        fpsCamera.fieldOfView = adsFOV;

        fpsController.mouseLook.XSensitivity = adsMouseSensitivity;
        fpsController.mouseLook.YSensitivity = adsMouseSensitivity;
    }

    void OnDisable()
    {
        NormalView();
    }
}
