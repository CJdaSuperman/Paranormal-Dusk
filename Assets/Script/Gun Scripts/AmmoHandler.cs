using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHandler : MonoBehaviour
{
    [SerializeField] int ammoAmount = 12;

    public int GetAmmoAmount() { return ammoAmount; }

    public void ReduceCurrentAmmo() => ammoAmount--;
}
