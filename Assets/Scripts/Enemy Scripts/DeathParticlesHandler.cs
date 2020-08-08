using UnityEngine;

public class DeathParticlesHandler : MonoBehaviour
{
    [SerializeField] float replenishAmount = 20f;

    void OnTriggerEnter(Collider other)
    {        
        if(other.tag == "Player")
        {
            PlayerHealthHandler player = other.gameObject.GetComponent<PlayerHealthHandler>();

            player.AddToCurrentHealth(replenishAmount);
            player.ShowPlayerHealthCanvas();

            Destroy(gameObject);
        }
    }
}