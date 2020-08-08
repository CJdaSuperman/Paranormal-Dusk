using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] float damage = 40f;
    [SerializeField] AudioClip attackSound;

    PlayerHealthHandler target;
    
    // Start is called before the first frame update
    void Start() => target = FindObjectOfType<PlayerHealthHandler>();

    void AttackAnimationEvent()
    {
        if(target == null) { return; }

        GetComponent<AudioSource>().PlayOneShot(attackSound);

        target.DecreaseHealth(damage);
    }
}