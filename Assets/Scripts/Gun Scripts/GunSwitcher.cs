using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    int currentWeapon = 0;

    void Start()
    {
        SetWeaponActive();
    }

    void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach(Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);

            weaponIndex++;
        }
    }

   void Update()
    {
        int previousWeapon = currentWeapon;

        ScrollWheelSwitching();
        KeyInputSwitching();

        //without this, the gun you switch to will not be set active
        if (previousWeapon != currentWeapon)
            SetWeaponActive();
    }

    void ScrollWheelSwitching()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentWeapon >= transform.childCount - 1)
                currentWeapon = 0;
            else
                currentWeapon++;
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (currentWeapon <= 0)
                currentWeapon = transform.childCount - 1;
            else
                currentWeapon--;
        }
    }

    void KeyInputSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentWeapon = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            currentWeapon = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            currentWeapon = 2;

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
            currentWeapon = 3;
    }

    
}
