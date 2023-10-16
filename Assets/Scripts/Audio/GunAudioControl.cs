using Guns;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// The audio control for Guns
    /// </summary>
    public class GunAudioControl : MonoBehaviour
    {
        [SerializeField]
        private AudioSource m_audioSource;

        [SerializeField]
        private AudioClip m_shotClip;

        [SerializeField]
        private AudioClip m_reloadClip;

        [SerializeField]
        private Gun m_gun;

        private AudioControl m_audioControl;

        private void Awake()
        {
            if (!m_audioSource)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its AudioSource component.");

            if (!m_gun)
            {
                Debug.LogError($"{gameObject.name} doesn't have a reference to its Gun.");
            }
            else
            {
                m_gun.OnFired  += OnFired;
                m_gun.OnReload += OnReload;
            }

            m_audioControl = new AudioControl(m_audioSource);
        }

        private void OnDestroy()
        {
            m_gun.OnFired  -= OnFired;
            m_gun.OnReload -= OnReload;
        }

        private void OnFired() => m_audioControl.Play(m_shotClip);
        private void OnReload() => m_audioControl.Play(m_reloadClip);
    }
}
