using System.Collections;
using UnityEngine;

public class DamageIndicatorController : MonoBehaviour
{
    [SerializeField] float duration = 0.3f;

    Canvas damageIndicatorCanvas;

    void Start()
    {
        damageIndicatorCanvas = GetComponent<Canvas>();
        damageIndicatorCanvas.enabled = false;
    }

    public void IndicateDamage() => StartCoroutine(DisplayIndicator());

    IEnumerator DisplayIndicator()
    {
        damageIndicatorCanvas.enabled = true;
        
        yield return new WaitForSeconds(duration);

        damageIndicatorCanvas.enabled = false;
    }
}