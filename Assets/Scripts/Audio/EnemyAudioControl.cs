using Enemy;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// The audio control for enemies
    /// </summary>
    public class EnemyAudioControl : MonoBehaviour
    {
        [SerializeField]
        private AudioSource m_audioSource;

        [SerializeField]
        private AudioClip m_attackClip;

        [SerializeField]
        private EnemyController m_enemy;

        private AudioControl m_audioControl;

        private void Awake()
        {
            if (!m_audioSource)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its AudioSource component.");

            if (!m_enemy)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the {nameof(EnemyController)}.");
            else
                m_enemy.OnAttack += PlayAttackClip;

            m_audioControl = new AudioControl(m_audioSource);
        }

        private void OnDisable() => m_enemy.OnAttack -= PlayAttackClip;

        private void PlayAttackClip() => m_audioControl.Play(m_attackClip, oneShot:false);
    }
}
