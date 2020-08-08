using UnityEngine;

public class AmmoBoxHandler : MonoBehaviour
{
    [SerializeField] Canvas controlsCanvas;

    bool isPlayerNear = false;

    bool isBoxOpen = false;

    void Start() => controlsCanvas.enabled = false;    


    void OnTriggerEnter(Collider other)
    {
        if(!isBoxOpen)
            controlsCanvas.enabled = true;

        isPlayerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        controlsCanvas.enabled = false;

        isPlayerNear = false;
    }

    void Update()
    {
        if (!isBoxOpen && isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isBoxOpen = true;
        }
    }
}