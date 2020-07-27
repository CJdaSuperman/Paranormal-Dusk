using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityStandardAssets.Characters.FirstPerson;

public class GunZoomHandler : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [Tooltip("Field of View in Aim Down Sight")] [SerializeField] float adsFOV = 38f;
    [SerializeField] float aimSpeed = 40f;
    [SerializeField] float adsMouseSensitivity = .6f;
    [Tooltip("Default Field of View")] [SerializeField] float normalFOV = 60f;
    [SerializeField] float normalMouseSensitivity = 1.3f;

    RigidbodyFirstPersonController fpsController;

    bool isADS = false;

    Animator animator;

    void Awake()
    {
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
        animator = GetComponent<Animator>();
    }

    //Allows the gun to go back to idle if it was switched
    void OnEnable()
    {
        animator.SetBool("Aim Down", false);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(!isADS)
            {
                AimDownSight();
            }
            else if(isADS)
            {
                NormalView();
            }
        }

        if (GetComponent<GunHandler>().IsReloading())
            NormalView();
    }

    void AimDownSight()
    {
        isADS = true;

        animator.SetBool("Aim Down", true);

        fpsCamera.fieldOfView = adsFOV;

        fpsController.mouseLook.XSensitivity = adsMouseSensitivity;
        fpsController.mouseLook.YSensitivity = adsMouseSensitivity;
    }

    void NormalView()
    {
        isADS = false;

        animator.SetBool("Aim Down", false);

        fpsCamera.fieldOfView = normalFOV;        

        fpsController.mouseLook.XSensitivity = normalMouseSensitivity;
        fpsController.mouseLook.YSensitivity = normalMouseSensitivity;
    }    

    void OnDisable()
    {
        NormalView();
    }
}
